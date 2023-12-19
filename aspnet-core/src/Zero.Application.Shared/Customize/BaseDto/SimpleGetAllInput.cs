using Abp.Application.Services.Dto;

namespace Zero.Customize.BaseDto
{
	public class SimpleGetAllInput : PagedAndSortedResultRequestDto
	{
		public string Filter { get; set; }
		public bool? IsActive { get; set; }
	}
}