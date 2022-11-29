﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LCPMauiWebApi.Client.Classes
{
    public class UsersAuth
    {
#nullable enable

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [DisplayName("UsersAuthId")]
        public int? UsersAuthId { get; set; }

        [DisplayName("Username")]
        public string? Username { get; set; }

        [DisplayName("Password")]
        public string? Password { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        [DisplayName("Pin")]
        public string? Pin { get; set; }

        [DisplayName("Role")]
        public string? Role { get; set; }

        [DisplayName("Token")]
        public string? Token { get; set; }

        [DisplayName("Date Created")]
        public DateTime? DateCreated { get; set; }

        [DisplayName("Date Expiration of Login")]
        public DateTime? DateExpLogin { get; set; }
    }
}
