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
    public class Security_Logins_Roles_Logic : BaseLogic<Security_Logins_Roles>
    {
        IDataRepository<Security_Logins_Roles> repo = new EFGenericRepository<Security_Logins_Roles>();
        public Security_Logins_Roles_Logic(IDataRepository<Security_Logins_Roles> repository) : base(repository)
        {
        }
        public override List<Security_Logins_Roles> GetAll()
        {

            List<Security_Logins_Roles> list = repo.GetAll(new Expression<Func<Security_Logins_Roles, object>>[]
            { a => a.Security_Logins, b => b.Security_Roles }).ToList();
            return list;
        }
        public override void Add(Security_Logins_Roles[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Security_Logins_Roles[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Security_Logins_Roles[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Security_Logins_Roles poco in pocos)
            {
                if (poco.Login == Guid.Empty)
                {
                    exceptions.Add(new ValidationException(1200, $" Login Cannot be empty  - {poco.Id}"));

                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
