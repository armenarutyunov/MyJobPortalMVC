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
    public class System_Country_Codes_Logic

    {
        public IDataRepository<System_Country_Codes> _repository;
        public System_Country_Codes_Logic(IDataRepository<System_Country_Codes> repository)
        {
            _repository = repository;
        }
        public void Add(System_Country_Codes[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }
        public void Update(System_Country_Codes[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos); _repository.GetAll();
        }
        public List<System_Country_Codes> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public System_Country_Codes Get(string id)
        {
            return _repository.GetSingle(c => c.Code == id);
        }
        public void Delete(System_Country_Codes[] pocos)
        {
            _repository.Remove(pocos);
        }

        protected void Verify(System_Country_Codes[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (System_Country_Codes poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, $" Code Cannot be empty or Null "));

                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, $" Name Cannot be empty or Null "));

                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
