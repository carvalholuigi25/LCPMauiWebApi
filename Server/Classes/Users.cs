﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPMauiWebApi.Server.Classes
{
    public class MyUsers
    {
#nullable enable

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [DisplayName("MyUsersId")]
        public int? MyUsersId { get; set; }

        [DisplayName("Username")]
        public string? Username { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Password")]
        public string? Password { get; set; }

        [DisplayName("Pin")]
        public string? Pin { get; set; }

        [DisplayName("Role")]
        public string? Role { get; set; }

        [DisplayName("About")]
        public string? About { get; set; }

        [DisplayName("Image")]
        public string? Image { get; set; }

        [DisplayName("Cover")]
        public string? Cover { get; set; }

        [DisplayName("Status")]
        public string? Status { get; set; }

        [DisplayName("Privacy")]
        public string? Privacy { get; set; }

        [DisplayName("Date Created")]
        public DateTime? DateCreated { get; set; }
    }
}
