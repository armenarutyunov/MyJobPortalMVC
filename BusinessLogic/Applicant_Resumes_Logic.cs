using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLogic
{
    public class Applicant_Resumes_Logic : BaseLogic<Applicant_Resumes>
    {
        IDataRepository<Applicant_Resumes> repo = new EFGenericRepository<Applicant_Resumes>();
        public Applicant_Resumes_Logic(IDataRepository<Applicant_Resumes> repository) : base(repository)
        {
        }
        public override List<Applicant_Resumes> GetAll()
        {

            List<Applicant_Resumes> list = repo.GetAll(new Expression<Func<Applicant_Resumes, object>>[]
            { a => a.Applicant_Profiles }).ToList();
            return list;
        }
        public override void Add(Applicant_Resumes[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Applicant_Resumes[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Applicant_Resumes[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Applicant_Resumes poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Resume))
                {
                    exceptions.Add(new ValidationException(113, $"Resume cannot be empty - {poco.Id}"));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
