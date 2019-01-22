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
    public class Company_Job_Skills_Logic : BaseLogic<Company_Job_Skills>
    {
        IDataRepository<Company_Job_Skills> repo = new EFGenericRepository<Company_Job_Skills>();
        public Company_Job_Skills_Logic(IDataRepository<Company_Job_Skills> repository) : base(repository)
        {
        }
        public override List<Company_Job_Skills> GetAll()
        {

            List<Company_Job_Skills> list = repo.GetAll(new Expression<Func<Company_Job_Skills, object>>[]
            { a => a.Company_Jobs}).ToList();
            return list;
        }
        public override void Add(Company_Job_Skills[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Company_Job_Skills[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Company_Job_Skills[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Company_Job_Skills poco in pocos)
            {

                if (poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(400, $"Importance cannot be less than 0 - {poco.Id}"));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
