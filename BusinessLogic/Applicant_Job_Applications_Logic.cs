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
    public class Applicant_Job_Applications_Logic : BaseLogic<Applicant_Job_Applications>
    {
        IDataRepository<Applicant_Job_Applications> repo = new EFGenericRepository<Applicant_Job_Applications>();
        public Applicant_Job_Applications_Logic(IDataRepository<Applicant_Job_Applications> repository) : base(repository)
        {
        }
        public override List<Applicant_Job_Applications> GetAll()
        {

            List<Applicant_Job_Applications> list = repo.GetAll(new Expression<Func<Applicant_Job_Applications, object>>[] 
            { a => a.Applicant_Profiles, b => b.Company_Jobs }).ToList();
            return list;
        }
        public override void Add(Applicant_Job_Applications[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Applicant_Job_Applications[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Applicant_Job_Applications[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Applicant_Job_Applications poco in pocos)
            {

                if (poco.Application_Date > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(110, $"ApplicationDate cannot be greater than today - {poco.Id}"));

                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
