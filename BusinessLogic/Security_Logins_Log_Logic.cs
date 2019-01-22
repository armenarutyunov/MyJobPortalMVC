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
    public class Security_Logins_Log_Logic : BaseLogic<Security_Logins_Log>
    {
        IDataRepository<Security_Logins_Log> repo = new EFGenericRepository<Security_Logins_Log>();
        public Security_Logins_Log_Logic(IDataRepository<Security_Logins_Log> repository) : base(repository)
        {
        }
        public override List<Security_Logins_Log> GetAll()
        {

            List<Security_Logins_Log> list = repo.GetAll(new Expression<Func<Security_Logins_Log, object>>[]
            { a => a.Security_Logins}).ToList();
            return list;
        }
        public override void Add(Security_Logins_Log[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Security_Logins_Log[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Security_Logins_Log[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Security_Logins_Log poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Source_IP))
                {
                    exceptions.Add(new ValidationException(1100, $" SourceIP Cannot be empty  - {poco.Id}"));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
