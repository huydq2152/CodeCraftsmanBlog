using Abp.Application.Services.Dto;

namespace Zero.Notifications.Dto
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}