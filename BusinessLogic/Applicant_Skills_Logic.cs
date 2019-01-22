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
    public class Applicant_Skills_Logic : BaseLogic<Applicant_Skills>
    {
        IDataRepository<Applicant_Skills> repo = new EFGenericRepository<Applicant_Skills>();
        public Applicant_Skills_Logic(IDataRepository<Applicant_Skills> repository) : base(repository)
        {
        }
        public override List<Applicant_Skills> GetAll()
        {

            List<Applicant_Skills> list = repo.GetAll(new Expression<Func<Applicant_Skills, object>>[]
            { a => a.Applicant_Profiles}).ToList();
            return list;
        }
        public override void Add(Applicant_Skills[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Applicant_Skills[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Applicant_Skills[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Applicant_Skills poco in pocos)
            {

                if ((int)poco.Start_Month > 12)
                {
                    exceptions.Add(new ValidationException(101, $"StartMonth Cannot be greater than 12 - {poco.Id}"));
                }
                if ((int)poco.End_Month > 12)
                {
                    exceptions.Add(new ValidationException(102, $"EndMonth Cannot be greater than 12 - {poco.Id}"));
                }
                if (poco.Start_Year < 1900)
                {
                    exceptions.Add(new ValidationException(103, $"StartYear Cannot be less then 1900 - {poco.Id}"));

                }
                if (poco.End_Year < poco.Start_Year)
                {
                    exceptions.Add(new ValidationException(104, $"EndYear Cannot be less then StartYear - {poco.Id}"));

                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
