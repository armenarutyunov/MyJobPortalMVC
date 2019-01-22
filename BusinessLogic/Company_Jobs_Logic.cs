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
    public class Company_Jobs_Logic : BaseLogic<Company_Jobs>
    {
        IDataRepository<Company_Jobs> repo = new EFGenericRepository<Company_Jobs>();
        public Company_Jobs_Logic(IDataRepository<Company_Jobs> repository) : base(repository)
        {
        }
        public override List<Company_Jobs> GetAll()
        {

            List<Company_Jobs> list = repo.GetAll(new Expression<Func<Company_Jobs, object>>[]
            { a => a.Company_Profiles}).ToList();
            return list;
        }
        public override void Add(Company_Jobs[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Company_Jobs[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Company_Jobs[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Company_Jobs poco in pocos)
            {

                if (poco.Profile_Created> DateTime.Now)
                {
                    exceptions.Add(new ValidationException(800, $"Cannot be greater than today - {poco.Id}"));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
