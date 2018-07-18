

using SMSInterfaces.Enums;

namespace SMSInterfaces.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthorizationResult
    {
        AuthorizationStatus Status { get; set; }
    }
}
