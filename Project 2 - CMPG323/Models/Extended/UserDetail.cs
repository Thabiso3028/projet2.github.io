using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project_2___CMPG323.Models
{
	[MetadataType(typeof(UserMetaData))]
	public partial class UserDetail
	{
		public string ConfirmPassword { get; set; }  // As it not on the database table (Addition model)
	}
	public class UserMetaData
	{

		/// <summary>
		/// 
		/// This is out validation!
		/// 
		/// </summary>


		[Display(Name ="First Name")]
		[Required(AllowEmptyStrings =false, ErrorMessage ="First name required")]
		public string UserFName { get; set; }

		[Display(Name = "Last Name")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
		public string UserLName { get; set; }

		[Display(Name = "Email")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Email required")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
		[DataType(DataType.Password)]
		[MinLength(8, ErrorMessage ="Minimum 8 characters required")]
		public string Password { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage ="Passwords do not match")]
		public string ConfirmPassword { get; set; }
	}

}