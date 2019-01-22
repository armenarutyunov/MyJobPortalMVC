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
    public class System_Language_Codes_Logic

    {
        protected IDataRepository<System_Language_Codes> _repository;
        public System_Language_Codes_Logic(IDataRepository<System_Language_Codes> repository)
        {
            _repository = repository;
        }
        public void Add(System_Language_Codes[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }
        public void Update(System_Language_Codes[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }
        public List<System_Language_Codes> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public System_Language_Codes Get(string id)
        {
            return _repository.GetSingle(c => c.LanguageID == id);
        }
        public void Delete(System_Language_Codes[] pocos)
        {
            _repository.Remove(pocos);
        }

        protected void Verify(System_Language_Codes[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (System_Language_Codes poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000, $" LanguageCodePoco  Cannot be empty or Null "));

                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(1001, $" Name Cannot be empty or Null "));

                }
                if (string.IsNullOrEmpty(poco.Native_Name))
                {
                    exceptions.Add(new ValidationException(1002, $" NativeName Cannot be empty or Null "));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
