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
    public class Company_Job_Educations_Logic : BaseLogic<Company_Job_Educations>
    {
        IDataRepository<Company_Job_Educations> repo = new EFGenericRepository<Company_Job_Educations>();
        public Company_Job_Educations_Logic(IDataRepository<Company_Job_Educations> repository) : base(repository)
        {
        }
        public override List<Company_Job_Educations> GetAll()
        {

            List<Company_Job_Educations> list = repo.GetAll(new Expression<Func<Company_Job_Educations, object>>[]
            { a => a.Company_Jobs}).ToList();
            return list;
        }
        public override void Add(Company_Job_Educations[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Company_Job_Educations[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Company_Job_Educations[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Company_Job_Educations poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(200, $"Major must be at least 2 characters - {poco.Id}"));

                }
                else
                if (poco.Major.Length < 2)
                {
                    exceptions.Add(new ValidationException(200, $"Major must be at least 2 characters - {poco.Id}"));
                }
                if (poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(201, $"Importance cannot be less than 0 - {poco.Id}"));
                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
