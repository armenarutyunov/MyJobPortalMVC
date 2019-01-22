using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace MVC_CORE.Models
{
    public class CompanyVM
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyWebsite { get; set; }
        public string ContactPhone { get; set; }
        public string ContactName { get; set; }
        public string LanguageId { get; set; }
        public DateTime RegistrationDate { get; set; }
        //public Dictionary<string, string> Location { get; set; }//= new Dictionary<string, string>();
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Postal_Code { get; set; }
        public CompanyVM()
        {
            CompanyName = ""; CompanyDescription = ""; LanguageId = "EN"; CompanyWebsite = "xxx@yyy.com";
            ContactPhone = "(416)-555-5555"; ContactName = "";
            Country= "Not Provided"; Province = "Not Provided"; City = "Not Provided";
            Postal_Code = "Not Provided"; Street = "Not Provided";
        }
    }
}
