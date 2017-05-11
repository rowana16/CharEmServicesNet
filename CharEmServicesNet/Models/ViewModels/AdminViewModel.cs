using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CharEmServicesNet.UserHelpers;

namespace CharEmServicesNet.Models.ViewModels
{
    public class AdminViewModel
    {

    }

    public class AdminIndexViewModel
    {
        public AdminIndexViewModel()
        {
            this.Users = new List<ApplicationUserView>();
        }
        public List<ApplicationUserView> Users { get; set; }

    }

    public class ApplicationUserView
    {       
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }



    }
}