﻿using System.ComponentModel.DataAnnotations;

namespace areas_api.Models.DTOs
{
	public class RegisterRequestDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public required string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
		public required string[] Roles { get; set; }
	}
}

