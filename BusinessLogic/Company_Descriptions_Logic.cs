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
    public class Company_Descriptions_Logic : BaseLogic<Company_Descriptions>
    {
        IDataRepository<Company_Descriptions> repo = new EFGenericRepository<Company_Descriptions>();
        public Company_Descriptions_Logic(IDataRepository<Company_Descriptions> repository) : base(repository)
        {
        }
        public override List<Company_Descriptions> GetAll()
        {

            List<Company_Descriptions> list = repo.GetAll(new Expression<Func<Company_Descriptions, object>>[]
            { a => a.Company_Profiles, a => a.System_Language_Codes }).ToList();
            return list;
        }
        public override void Add(Company_Descriptions[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Company_Descriptions[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Company_Descriptions[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Company_Descriptions poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Company_Description))
                {
                    exceptions.Add(new ValidationException(107, $"CompanyDescription Cannot be empty and Must be greater than 2 characters - {poco.Id}"));

                }
                else
                if (poco.Company_Description.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"CompanyDescription Cannot be empty and Must be greater than 2 characters - {poco.Id}"));
                }
                if (string.IsNullOrEmpty(poco.Company_Name))
                {
                    exceptions.Add(new ValidationException(106, $"CompanyName Cannot be empty and Must be greater than 2 characters - {poco.Id}"));
                }
                else
               if (poco.Company_Name.Length < 3)
                {
                    exceptions.Add(new ValidationException(106, $"CompanyName Cannot be empty and Must be greater than 2 characters - {poco.Id}"));
                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
