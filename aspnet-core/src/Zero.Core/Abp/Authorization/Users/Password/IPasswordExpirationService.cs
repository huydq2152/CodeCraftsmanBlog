using Abp.Domain.Services;

namespace Zero.Authorization.Users.Password
{
    public interface IPasswordExpirationService : IDomainService
    {
        void ForcePasswordExpiredUsersToChangeTheirPassword();
    }
}
