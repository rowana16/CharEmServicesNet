using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class ServiceBaseViewModel
    {
        public ServiceBaseViewModel()
        {
            Providers = new List<SelectListItem>();
            Recipients = new List<SelectListItem>();
            Locations = new List<SelectListItem>();
            SelectedLocations = new List<string>();      
        }

        public int Id { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual string ServiceDetails { get; set; }
        public virtual string SelectedProviderId { get; set; }
        public virtual int SelectedRecipientId { get; set; }   
        public virtual List<string> SelectedLocations { get; set; }     

        
        public ICollection<SelectListItem> Locations { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public ICollection<SelectListItem> Recipients { get; set; }
    }

    public class ServiceOperationViewModel : ServiceBaseViewModel
    {

        [Required(ErrorMessage = "Name is Required")]
        public override string ServiceName { get; set; }

        [Required(ErrorMessage = "Provider is Required")]        
        public override string SelectedProviderId { get; set; }        
    }

    public class ServiceEditViewModel: ServiceBaseViewModel
    {
        public ServiceEditViewModel()
        {
            this.SelectedProviders = new List<string>();
            this.AddLocations = new List<string>();
            this.RemoveLocations = new List<string>();    
        }

        [Required(ErrorMessage = "Name is Required")]
        public override string ServiceName { get; set; }

        public List<string> SelectedProviders { get; set; }
        public ServiceProvider CurrentProvider { get; set; }

        public List<string> AddLocations { get; set; }
        public List<string> RemoveLocations { get; set; }

        public ICollection<SelectListItem> MissingLocations { get; set; }
    }

    public class ServiceDetailViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDetails { get; set; }
        public ServiceProvider CurrentProvider { get; set; }
    }
    
}