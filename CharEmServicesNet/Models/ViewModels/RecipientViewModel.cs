using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CharEmServicesNet.Models.ViewModels
{
    public class IndexRecipientViewModel
    {
        public IndexRecipientViewModel()
        {
            ServiceRecipients = new List<IndexRecipientItem>();
        }

        public List<IndexRecipientItem> ServiceRecipients { get; set; }
    }

    public class IndexRecipientItem
    {
        public int RecipientId { get; set; }
        public string OrganizationName { get; set; }
        public string City { get; set; }
    }

    public class CreateRecipientViewModel
    {
        [Display(Name = "Provider name")]
        public string OrganizationName { get; set; }
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Description { get; set; }

        public int AddressId { get; set; }
        public int OrganizationTypeId { get; set; }
        public string UserId { get; set; }
        public int? RecipientId { get; set; }
    }
}