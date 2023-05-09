using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers.Identity;

public class AppUserMapper : BaseMapper<Public.DTO.v1.Identity.AppUser, App.BLL.DTO.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.Identity.AppUser MapToBll(Public.DTO.v1.Identity.AppUser user)
    {
        // maybe we need custom logic here
        // return Mapper.Map<BLL.App.DTO.Person>(personAdd);
        var res = new App.BLL.DTO.Identity.AppUser()
        {
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        /*
        res.Contacts = 
            personAdd.Contacts.Select(x => ContactMapper.MapToBll(x)).ToList();
        */
        return res;
    }
}