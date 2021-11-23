using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project_2___CMPG323.Models
{
	public class UserLogin
	{
		
		[Display(Name = "Email")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Email required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
		[DataType(DataType.Password)]
		[MinLength(8, ErrorMessage = "Minimum 8 characters required")]
		public string Password { get; set; }

		[Display(Name ="Remember me")]
		public bool RememberMe { get; set; }
	}
}