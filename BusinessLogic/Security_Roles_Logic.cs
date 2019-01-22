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
    public class Security_Roles_Logic : BaseLogic<Security_Roles>
    {
        IDataRepository<Security_Roles> repo = new EFGenericRepository<Security_Roles>();
        public Security_Roles_Logic(IDataRepository<Security_Roles> repository) : base(repository)
        {
        }
        public override List<Security_Roles> GetAll()
        {

            List<Security_Roles> list = repo.GetAll().ToList();
            return list;
        }
        public override void Add(Security_Roles[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Security_Roles[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Security_Roles[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Security_Roles poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Role))
                {
                    exceptions.Add(new ValidationException(800, $"Role Cannot be empty - {poco.Id}"));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
