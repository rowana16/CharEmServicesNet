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
        //public ServiceOperationViewModel()
        //{
        //    providers = new ICollection<SelectListItem>();
        //    recipients = new ICollection<SelectListItem>();
        //    ServiceType = new ICollection<SelectListItem>();
        //}

        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDetails { get; set; }
        
        public int SelectedServiceTypeId { get; set; }
        public int SelectedProviderId { get; set; }
        public int SelectedRecipientId { get; set; }

        public ServiceType ServiceTypeReturn { get; set; }
        
        public ICollection<SelectListItem> ServiceType { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public ICollection<SelectListItem> Recipients { get; set; }
    }
    
}