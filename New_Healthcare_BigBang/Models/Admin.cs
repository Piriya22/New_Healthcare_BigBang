﻿using System.ComponentModel.DataAnnotations;

namespace New_Healthcare_BigBang.Models
{
    public class Admin
    {
        [Key]

        public int Admin_Id { get; set; }

        public string? Admin_Name { get; set; }

        public string? Admin_Password { get; set; }
    }
}
