using Hospital.Models;
using Hospital.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public  class ApplicationUserViewModel
    {
        public List<ApplicationUser> Doctors { get; set; } = new List<ApplicationUser>();
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public string Specialist { get; set; }
        public bool IsDoctor { get; set; }

        public ApplicationUserViewModel()
        {
            
        }

        public ApplicationUserViewModel(ApplicationUser user)
        {
            Name       = user.Name;
            City       = user.City;
            Gender     = user.Gender;
            Specialist = user.Specialist;
            IsDoctor   = user.IsDoctor;
            Email      = user.Email;
            UserName   = user.UserName;
        }

        public ApplicationUser ConvertViewModelToModel(ApplicationUserViewModel user)
        {
            return new ApplicationUser
            {
                Name       = user.Name,
                City       = user.City,
                Gender     = user.Gender,
                Specialist = user.Specialist,
                IsDoctor   = user.IsDoctor,
                Email      = user.Email,
                UserName   = user.UserName,
            };
        }
    }
}
