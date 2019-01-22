using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class Applicant_Educations_Logic : BaseLogic<Applicant_Educations>
    {
        IDataRepository<Applicant_Educations> repo = new EFGenericRepository<Applicant_Educations>();
        public Applicant_Educations_Logic(IDataRepository<Applicant_Educations> repository) : base(repository)
        {
        }
        public override List<Applicant_Educations> GetAll()
        {

            List<Applicant_Educations> list = repo.GetAll(a => a.Applicant_Profiles).ToList();
            return list;
        }
        public override void Add(Applicant_Educations[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Applicant_Educations[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Applicant_Educations[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Applicant_Educations poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(107, $"Cannot be empty or less than 3 characters - {poco.Id}"));

                }
                else
                if (poco.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"Cannot be empty or less than 3 characters - {poco.Id}"));
                }
                if (poco.Start_Date > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108, $"Cannot be greater than today - {poco.Id}"));

                }
                if (poco.Completion_Date < poco.Start_Date)
                {
                    exceptions.Add(new ValidationException(109, $"CompletionDate cannot be earlier than StartDate - {poco.Id}"));

                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
