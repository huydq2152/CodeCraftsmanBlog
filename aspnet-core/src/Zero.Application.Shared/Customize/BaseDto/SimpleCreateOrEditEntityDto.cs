﻿using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Zero.Customize.BaseDto
{
	public class SimpleCreateOrEditEntityDto : EntityDto<int?>
	{
		[Required]
		[StringLength(ZeroConst.MaxCodeLength, MinimumLength = ZeroConst.MinCodeLength)]
		public string Code { get; set; }
		
		[Required]
		[StringLength(ZeroConst.MaxNameLength, MinimumLength = ZeroConst.MinNameLength)]
		public string Name { get; set; }

		public string Note { get; set; }
		
		public int Order { get; set; }
		
		public bool IsDefault { get; set; }
		
		public bool IsActive { get; set; }
	}
}