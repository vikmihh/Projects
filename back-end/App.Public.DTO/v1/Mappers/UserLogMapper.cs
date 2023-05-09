using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserLogMapper : BaseMapper<Public.DTO.v1.UserLog, App.BLL.DTO.UserLog>
{
    public UserLogMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.UserLog? MapToBll(Public.DTO.v1.UserLog? userLog)
    {
        if (userLog == null) return null;
        var res = new App.BLL.DTO.UserLog()
        {
            Id = userLog.Id,
            From = userLog.From,
            Until = userLog.Until,
            AppUserId = userLog.AppUserId,
            TicketInOrderId = userLog.TicketInOrderId
        };
        return res;
    }
    
    public static Public.DTO.v1.UserLog? MapToPublic(App.BLL.DTO.UserLog? userLog)
    {
        if (userLog == null) return null;
        var res = new Public.DTO.v1.UserLog()
        {
            Id = userLog.Id,
            FromStr = userLog.From.ToLocalTime().ToString("MM/dd/yyyy HH:mm"),
            UntilStr = userLog.Until.ToLocalTime().ToString("MM/dd/yyyy HH:mm"),
            AppUserId = userLog.AppUserId,
            TicketInOrderId = userLog.TicketInOrderId
        };
        return res;
    }
}