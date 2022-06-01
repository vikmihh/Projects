using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using App.Domain;
using App.Public.DTO.v1.Identity;
using Base.Contracts.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;


namespace Testing.WebApp.ApiControllers;

public class IntegrationOrderController : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;


    public IntegrationOrderController(CustomWebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            }
        );
    }

    [Fact]
    public async Task Get_Register_API_Happy_Flow()
    {
        var registerDto = new App.Public.DTO.v1.Identity.Register()
        {
            Email = "test@test.test",
            Password = "Test1.test",
            FirstName = "Tester",
            LastName = "Testerfield"
        };
        var jsonStr = System.Text.Json.JsonSerializer.Serialize(registerDto);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/v1/identity/Account/Register/", data);

        response.EnsureSuccessStatusCode();
        var requestContent = await response.Content.ReadAsStringAsync();

        var resultJwt = System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.Identity.JWTResponse>(
            requestContent,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}
        );

        await InitConfiguration(resultJwt!);
    }


    private async Task InitConfiguration(App.Public.DTO.v1.Identity.JWTResponse resultJwt)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", resultJwt!.Token);
        // Act 


        var apiResponseCard = new App.Public.DTO.v1.Card()
        {
            FirstName = "User",
            LastName = "Surname",
            CardNumber = 1234123412341234,
            SecurityCode = 123,
            ExpiryMonth = 12,
            ExpiryYear = 1234
        };
        var jsonStr = System.Text.Json.JsonSerializer.Serialize(apiResponseCard);
        var data = new StringContent(jsonStr, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/v1/Cards", data);

        // // Assert
        response.EnsureSuccessStatusCode();
        var contentCardJson = await response.Content.ReadAsStringAsync();
        var resultDataCard = System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.Card>(contentCardJson,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataCard);


        var apiResponseCoordinateLocation = new App.Public.DTO.v1.CoordinateLocation()
        {
            Location = "Tallinn"
        };
        var jsonStrCoordinateLocation = System.Text.Json.JsonSerializer.Serialize(apiResponseCoordinateLocation);
        var dataCoordinateLocation = new StringContent(jsonStrCoordinateLocation, Encoding.UTF8, "application/json");
        var responseCoordinateLocation = await _client.PostAsync("/api/v1/CoordinatesLocation", dataCoordinateLocation);


        //Assert
        responseCoordinateLocation.EnsureSuccessStatusCode();
        var contentCoordinateLocationJson = await responseCoordinateLocation.Content.ReadAsStringAsync();
        var resultDataCoordinateLocation =
            System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.CoordinateLocation>(
                contentCoordinateLocationJson,
                new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataCoordinateLocation);



        var apiResponseCoordinate = new App.Public.DTO.v1.Coordinate()
        {
            Index = "A1",
            CoordinateLocationId = resultDataCoordinateLocation!.Id,
            CoordinateLocationName = "test"
        };

        var jsonStrCoordinate = System.Text.Json.JsonSerializer.Serialize(apiResponseCoordinate);
        var dataCoordinate = new StringContent(jsonStrCoordinate, Encoding.UTF8, "application/json");
        var responseCoordinate = await _client.PostAsync("/api/v1/Coordinates", dataCoordinate);
        // Assert
        responseCoordinate.EnsureSuccessStatusCode();
        var contentCoordinateJson = await responseCoordinate.Content.ReadAsStringAsync();
        var resultDataCoordinate =
            System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.Coordinate>(contentCoordinateJson,
                new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataCoordinate);


        var apiResponseItemCategory = new App.Public.DTO.v1.ItemCategory()
        {
            Name = "Drink"
        };
        var jsonStrItemCategory = System.Text.Json.JsonSerializer.Serialize(apiResponseItemCategory);
        var dataItemCategory = new StringContent(jsonStrItemCategory, Encoding.UTF8, "application/json");
        var responseItemCategory = await _client.PostAsync("/api/v1/ItemCategories", dataItemCategory);
        // Assert
        responseItemCategory.EnsureSuccessStatusCode();
        var contentItemCategoryJson = await responseItemCategory.Content.ReadAsStringAsync();
        var resultDataItemCategory =
            System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.ItemCategory>(contentItemCategoryJson,
                new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataItemCategory);


        var apiResponseMenuItem = new App.Public.DTO.v1.MenuItem()
        {
            ItemName = "Coffee",
            Description = "asdfas",
            ItemCategoryName = "test",
            Price = 5,
            ItemCategoryId = resultDataItemCategory!.Id
        };
        var jsonStrMenuItem = System.Text.Json.JsonSerializer.Serialize(apiResponseMenuItem);
        var dataMenuItem = new StringContent(jsonStrMenuItem, Encoding.UTF8, "application/json");
        var responseMenuItem = await _client.PostAsync("/api/v1/MenuItems", dataMenuItem);
        // Assert
        responseMenuItem.EnsureSuccessStatusCode();
        var contentMenuItemJson = await responseMenuItem.Content.ReadAsStringAsync();
        var resultDataMenuItem =
            System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.MenuItem>(contentMenuItemJson,
                new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataMenuItem);


        var apiResponseTicket = new App.Public.DTO.v1.Ticket()
        {
            Name = "Full day",
            DayRange = 1,
            Price = 5
        };
        var jsonStrTicket = System.Text.Json.JsonSerializer.Serialize(apiResponseTicket);
        var dataTicket = new StringContent(jsonStrTicket, Encoding.UTF8, "application/json");
        var responseTicket = await _client.PostAsync("/api/v1/Tickets", dataTicket);
        // Assert
        responseTicket.EnsureSuccessStatusCode();
        var contentTicketJson = await responseTicket.Content.ReadAsStringAsync();
        var resultDataTicket = System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.Ticket>(contentTicketJson,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataTicket);
        
        
       
        var ticketAdding = await _client.PostAsync($"/api/v1/TicketsInOrder/add/{resultDataTicket!.Id}", dataTicket);
        // Assert
        ticketAdding.EnsureSuccessStatusCode();
        var contentTicketAddJson = await responseTicket.Content.ReadAsStringAsync();
        var resultDataTicketAdd = System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.TicketInOrder>(contentTicketAddJson,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataTicketAdd);
        
        var itemAdding = await _client.PostAsync($"/api/v1/ItemsInOrder/add/{resultDataMenuItem!.Id}/amount/5", dataTicket);
        // Assert
        itemAdding.EnsureSuccessStatusCode();
        var contentItemAddJson = await itemAdding.Content.ReadAsStringAsync();
        var resultDataItemAdd = System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.ItemInOrder>(contentItemAddJson,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataItemAdd);

        
        var currentOrder = await _client.GetAsync($"/api/v1/Order/current/");
        currentOrder.EnsureSuccessStatusCode();
        var contentCurrentOrderJson = await currentOrder.Content.ReadAsStringAsync();
        var resultDataCurrentOrder = System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.Order>(contentCurrentOrderJson,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataCurrentOrder);
        resultDataCurrentOrder!.CardId = resultDataCard!.Id;
        resultDataCurrentOrder!.CoordinateId = resultDataCoordinate!.Id;
        
        
        
        var jsonStrUpdatedOrder = System.Text.Json.JsonSerializer.Serialize(resultDataCurrentOrder);

        var dataUpdatedOrder = new StringContent(jsonStrUpdatedOrder, Encoding.UTF8, "application/json");
        
        var proceededOrder = await _client.PostAsync($"/api/v1/Order/proceedOrder/",dataUpdatedOrder);
        proceededOrder.EnsureSuccessStatusCode();
        var contentProceededOrderJson = await proceededOrder.Content.ReadAsStringAsync();
        var resultDataProceededOrder = System.Text.Json.JsonSerializer.Deserialize<App.Public.DTO.v1.Order>(contentProceededOrderJson,
            new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        Assert.NotNull(resultDataProceededOrder);
        Assert.NotEqual(Guid.Empty, resultDataProceededOrder!.Id);
        Assert.False(resultDataProceededOrder.InProcess);
        Assert.Equal(30, resultDataProceededOrder.FinalPrice);
        Assert.Equal(0, resultDataProceededOrder.Discount);
        Assert.Equal(30, resultDataProceededOrder.Price);
        Assert.Equal(1, resultDataProceededOrder.OrderNr);
    }
}