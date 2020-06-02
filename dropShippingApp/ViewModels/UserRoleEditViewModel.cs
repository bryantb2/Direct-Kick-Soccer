using dropShippingApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.ViewModels
{
	public class UserRoleEditViewModel
	{
		public IdentityRole Role { get; set; }
		public IEnumerable<AppUser> Members { get; set; }
		public IEnumerable<AppUser> NonMembers { get; set; }
	}
	public class RoleModificationViewModel
	{
		[Required]
		public string RoleName { get; set; }
		public string RoleId { get; set; }
		public string[] IdsToAdd { get; set; }
		public string[] IdsToDelete { get; set; }
	}
}
