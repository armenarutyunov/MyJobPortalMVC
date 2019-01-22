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
    public class Company_Locations_Logic : BaseLogic<Company_Locations>
    {
        IDataRepository<Company_Locations> repo = new EFGenericRepository<Company_Locations>();
        public Company_Locations_Logic(IDataRepository<Company_Locations> repository) : base(repository)
        {
        }
        public override List<Company_Locations> GetAll()
        {

            List<Company_Locations> list = repo.GetAll(new Expression<Func<Company_Locations, object>>[]
            { a => a.Company_Profiles}).ToList();
            return list;
        }
        public override void Add(Company_Locations[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(Company_Locations[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(Company_Locations[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (Company_Locations poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Country_Code))
                {
                    exceptions.Add(new ValidationException(500, $"CountryCode cannot be empty - {poco.Id}"));

                }
                if (string.IsNullOrEmpty(poco.State_Province_Code))
                {
                    exceptions.Add(new ValidationException(501, $"Province cannot be empty - {poco.Id}"));

                }
                if (string.IsNullOrEmpty(poco.City_Town))
                {
                    exceptions.Add(new ValidationException(503, $"City cannot be empty - {poco.Id}"));

                }
                if (string.IsNullOrEmpty(poco.Street_Address))
                {
                    exceptions.Add(new ValidationException(502, $"Street cannot be empty - {poco.Id}"));

                }
                if (string.IsNullOrEmpty(poco.Zip_Postal_Code))
                {
                    exceptions.Add(new ValidationException(504, $"PostalCode cannot be empty - {poco.Id}"));

                }


                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
