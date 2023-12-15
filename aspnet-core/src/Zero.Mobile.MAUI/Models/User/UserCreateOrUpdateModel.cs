using Abp.AutoMapper;
using Zero.Authorization.Users.Dto;

namespace Zero.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(CreateOrUpdateUserInput))]
    public class UserCreateOrUpdateModel : CreateOrUpdateUserInput
    {

    }
}
