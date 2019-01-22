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
    public class Applicant_Work_History_Logic : BaseLogic<Applicant_Work_History>
    {
        IDataRepository<Applicant_Work_History> repo = new EFGenericRepository<Applicant_Work_History>();
        public Applicant_Work_History_Logic(IDataRepository<Applicant_Work_History> repository) : base(repository)
        {
        }
        public override List<Applicant_Work_History> GetAll()
        {

            List<Applicant_Work_History> list = repo.GetAll(new Expression<Func<Applicant_Work_History, object>>[]
            { a => a.Applicant_Profiles, b => b.System_Country_Codes }).ToList();
            return list;
        }
        public override void Add(Applicant_Work_History[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Applicant_Work_History[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Applicant_Work_History[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Applicant_Work_History poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Company_Name))
                {
                    exceptions.Add(new ValidationException(105, $"Cannot be empty and Must be greater then 2 characters - {poco.Id}"));

                }
                else
                if (poco.Company_Name.Length < 3)
                {
                    exceptions.Add(new ValidationException(105, $"Cannot be empty and Must be greater then 2 characters - {poco.Id}"));
                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
