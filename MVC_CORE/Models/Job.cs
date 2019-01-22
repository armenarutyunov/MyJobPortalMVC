using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_CORE.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string Major { get; set; }
        public  string  MajorImp { get; set; }
        public string Skill { get; set; }
        public string  SkillLevel { get; set; }
        public string SkillImp { get; set; }
        public DateTime ProfileCreated { get; set; }
        
        public Job()
        {
            JobName = ""; JobDescription = ""; Major = ""; MajorImp = "0"; Skill = ""; SkillLevel = "0"; SkillImp = "0";
        }
    }
}