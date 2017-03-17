using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CharEmServicesNet.Models.ViewModels
{
    public class ServiceIndexViewModel
    {
        public ServiceIndexViewModel()
        {
            services = new List<Service>();
        }

        public List<Service> services { get; set; }
    }

    public class ServiceOperationViewModel
    {
        public ServiceOperationViewModel()
        {
            providers = new List<SelectListItem>());
            recipients = new List<SelectListItem>());
        }

        public List<SelectListItem> providers { get; set; }
        public List<SelectListItem> recipients { get; set; }
    }
    
}