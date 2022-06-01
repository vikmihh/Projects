using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using Base.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppUser = App.Domain.Identity.AppUser;


namespace WebApplication.ApiControllers.Identity;

/// <summary>
/// Account controller class. Has methods for creating new account and login with the one, which already exists.
/// Generates tokens and refresh tokens if the old token is expired.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Random _rnd = new Random();
    private readonly AppDbContext _context;


    /// <summary>
    /// Account controller constructor
    /// </summary>
    /// <param name="signInManager"> Provides the APIs for user sign in</param>
    /// <param name="userManager"> Provides the APIs for managing user in a persistence store</param>
    /// <param name="logger"> Logs out warnings</param>
    /// <param name="configuration"> Represents a set of key/value application configuration properties</param>
    /// <param name="context"> Gives access to database</param>
    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
        ILogger<AccountController> logger, IConfiguration configuration, AppDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _configuration = configuration;
        _context = context;
    }

    /// <summary>
    /// Login into the rest backend - generates JWT to be included in
    /// Authorize: Bearer xyz
    /// </summary>
    /// <param name="loginData">Supply email and password</param>
    /// <returns>JWT and refresh token</returns>
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> LogIn([FromBody] Login loginData)
    {
        //verify username
        var appUser = await _userManager.FindByNameAsync(loginData.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        //verify username and password
        var res = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!res.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password problem for user{}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        //get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        appUser.RefreshTokens = await _context
            .Entry(appUser)
            .Collection(a => a.RefreshTokens!)
            .Query()
            .Where(t => t.AppUserId == appUser.Id)
            .ToListAsync();

        foreach (var userRefreshToken in appUser.RefreshTokens)
        {
            if (userRefreshToken.TokenExpirationDateTime < DateTime.UtcNow &&
                userRefreshToken.PreviousTokenExpirationDateTime < DateTime.UtcNow)
            {
                _context.RefreshTokens.Remove(userRefreshToken);
            }
        }

        var refreshToken = new RefreshToken();
        refreshToken.AppUserId = appUser.Id;
        _context.RefreshTokens.Add(refreshToken);


        //generate JWT
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:issuer"],
            _configuration["JWT:issuer"],
            DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
        );
        var u = await _userManager.FindByEmailAsync(appUser.Email)!;
        var roles = await _userManager.GetRolesAsync(u)!;

        var result = new JWTResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Email = appUser.Email,
            Role = roles.ElementAt(0)
        };
        return Ok(result);
    }

    /// <summary>
    /// Create new account into the rest backend - creates JWT to be included in
    /// Authorize: Bearer xyz
    /// </summary>
    /// <param name="registrationData">Supply email, password, firstName and lastName</param>
    /// <returns>JWT and refresh token</returns>
    [HttpPost]
    public async Task<ActionResult<JWTResponse>> Register(Register registrationData)
    {
        //verify user
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registrationData.Email);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            errorResponse.Errors["email"] = new List<string>()
            {
                "Email already registered"
            };
            return BadRequest(errorResponse);
        }

        var refreshToken = new RefreshToken();
        appUser = new AppUser()
        {
            FirstName = registrationData.FirstName,
            LastName = registrationData.LastName,
            Email = registrationData.Email,
            UserName = registrationData.Email,
            RefreshTokens = new List<RefreshToken>()
            {
                refreshToken
            }
        };


        //create user (system will do it)
        var result = await _userManager.CreateAsync(appUser, registrationData.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        result = await _userManager.AddClaimAsync(appUser, new Claim("aspnet.firstname", appUser.FirstName));
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        result = await _userManager.AddClaimAsync(appUser, new Claim("aspnet.lastname", appUser.LastName));
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        //get full user from system fixed data
        appUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (appUser == null)
        {
            _logger.LogWarning("User with email {} is not found after registered", registrationData.Email);
            return BadRequest($"User with email {registrationData.Email} is not found after registered");
        }

        //get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", registrationData.Email);
            return NotFound("User/Password problem");
        }

        await _userManager.AddToRoleAsync(appUser, "user");
        //generate JWT
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:issuer"],
            _configuration["JWT:issuer"],
            DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
        );
  
        var roles = await _userManager.GetRolesAsync(appUser)!;
        var res = new JWTResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Role = roles.ElementAt(0)
        };
        return Ok(res);
    }

    /// <summary>
    /// Generate new refresh token , obsolete old ones
    /// Authorize: Bearer xyz
    /// </summary>
    /// <param name="refreshTokenModel">JWT old token</param>
    /// <returns>New refresh token</returns>
    [HttpPost]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel)
    {
        //get info from JWT
        JwtSecurityToken jwtToken;
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
            if (jwtToken == null)
            {
                return BadRequest("No token");
            }
        }
        catch (Exception e)
        {
            return BadRequest($"Can not parse the token, {e.Message}");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        //validate token signature TODO:


        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest("No email in JWT");
        }

        //get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return NotFound($"User with email {userEmail} not found");
        }

        //load and compare refresh tokens

        await _context.Entry(appUser).Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x =>
                (x.Token == refreshTokenModel.RefreshToken && x.TokenExpirationDateTime > DateTime.UtcNow) ||
                (x.PreviousToken == refreshTokenModel.RefreshToken &&
                 x.PreviousTokenExpirationDateTime > DateTime.UtcNow)
            )
            .ToListAsync();

        if (appUser.RefreshTokens == null)
        {
            return Problem("RefreshTokens collection is null");
        }

        if (appUser.RefreshTokens.Count == 0)
        {
            return Problem("RefreshTokens collection is empty, no valid refresh tokens found");
        }

        if (appUser.RefreshTokens.Count != 1)
        {
            return Problem("More than one valid refresh token found");
        }


        //generate new JWT

        //get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", userEmail);
            return NotFound("User/Password problem");
        }

        //generate JWT
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:issuer"],
            _configuration["JWT:issuer"],
            DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:ExpiresInMinutes"))
        );

        //make new refresh token, obsolete old ones

        var refreshToken = appUser.RefreshTokens.First();
        if (refreshToken.Token == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
        }

        var res = new JWTResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName
        };
        return Ok(res);
    }
}