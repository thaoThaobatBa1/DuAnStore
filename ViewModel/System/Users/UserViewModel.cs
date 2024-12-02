﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.System.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PassWord { get; set; }
        public DateTime Dob { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
        public IList<string> Roles { get; set; }
    }
}
