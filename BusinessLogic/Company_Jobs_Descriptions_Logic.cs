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
    public class Company_Jobs_Descriptions_Logic : BaseLogic<Company_Jobs_Descriptions>
    {
        IDataRepository<Company_Jobs_Descriptions> repo = new EFGenericRepository<Company_Jobs_Descriptions>();
        public Company_Jobs_Descriptions_Logic(IDataRepository<Company_Jobs_Descriptions> repository) : base(repository)
        {
        }
        public override List<Company_Jobs_Descriptions> GetAll()
        {

            List<Company_Jobs_Descriptions> list = repo.GetAll(new Expression<Func<Company_Jobs_Descriptions, object>>[]
            { a => a.Company_Jobs }).ToList();
            return list;
        }
        public override void Add(Company_Jobs_Descriptions[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Company_Jobs_Descriptions[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Company_Jobs_Descriptions[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Company_Jobs_Descriptions poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Job_Name))
                {
                    exceptions.Add(new ValidationException(300, $"JobName cannot be Null or Empty - {poco.Id}"));

                }
                if (string.IsNullOrEmpty(poco.Job_Descriptions))
                {
                    exceptions.Add(new ValidationException(301, $"JobDescriptions cannot be Null or Empty - {poco.Id}"));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
