using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CORE.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public Company_Descriptions EnName { get; set; }
        public Company_Descriptions DeName { get; set; }
        public Company_Descriptions FrName { get; set; }
        public Company_Profiles Profile { get; set; }
        public Company_Locations Location { get; set; }
        public List<Company_Locations> Locations { get; set; }

        //Company_Descriptions _EnName;
        //Company_Descriptions _DeName;
        //Company_Descriptions _FrName;
        //Company_Profiles _Profile;
        //Company_Locations _Location;
        public Company()
        {
            Id = Guid.NewGuid();
            EnName = new Company_Descriptions() { Company_Name = "Was not specified", Company_Description = "Was not specified" };
            DeName = new Company_Descriptions() { Company_Name = "Was not specified", Company_Description = "Was not specified" };
            FrName = new Company_Descriptions() { Company_Name = "Was not specified", Company_Description = "Was not specified" };

            Profile = new Company_Profiles() { Company_Website = "xxx@xxx.com",
                Contact_Phone = "xxx-xxx-xxxx", Contact_Name = "John Smith", Registration_Date = DateTime.Now };


            Location = new Company_Locations() { Id = Guid.NewGuid(),  Country_Code = "Was not specified", State_Province_Code = "Was not specified",
                City_Town = "Was not specified", Zip_Postal_Code = "Was not specified", Street_Address = "Was not specified"
            };
            Locations = new List<Company_Locations>() {Location};
           
        }
    }
}
