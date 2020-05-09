﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobSolution.Domain.Entities
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleFromRegister { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        
    }
}
