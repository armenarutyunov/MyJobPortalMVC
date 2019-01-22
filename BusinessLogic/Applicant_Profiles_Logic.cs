using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Applicant_Profiles_Logic : BaseLogic<Applicant_Profiles>
    {
        IDataRepository<Applicant_Profiles> repo = new EFGenericRepository<Applicant_Profiles>();
        public Applicant_Profiles_Logic(IDataRepository<Applicant_Profiles> repository) : base(repository)
        {
        }
        public override List<Applicant_Profiles> GetAll()
        {

            List<Applicant_Profiles> list = repo.GetAll(new Expression<Func<Applicant_Profiles, object>>[]
            { a => a.Security_Logins, b => b.System_Country_Codes }).ToList();
            return list;
        }
        public override void Add(Applicant_Profiles[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Applicant_Profiles[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Applicant_Profiles[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Applicant_Profiles poco in pocos)
            {

                if (poco.Current_Salary < 0)
                {
                    exceptions.Add(new ValidationException(111, $"CurrentSalary cannot be negative - {poco.Id}"));
                }
                if (poco.Current_Rate < 0)
                {
                    exceptions.Add(new ValidationException(112, $"CurrentRate cannot be negative - {poco.Id}"));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
