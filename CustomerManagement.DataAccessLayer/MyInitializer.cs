using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CustomerManagement.Entities;

namespace CustomerManagement.DataAccessLayer
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            #region CMUser
            CMUser admin = new CMUser
            {
                Name = "Admin",
                Surname = "User",
                Email = "adminuser@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "recep"
            };
            CMUser user1 = new CMUser
            {
                Name = "user",
                Surname = "1",
                Email = "user1@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "recep"
            };
            CMUser user2 = new CMUser
            {
                Name = "user",
                Surname = "2",
                Email = "user2@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = false,
                IsAdmin = false,
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "recep"
            };
            context.CMUser.Add(admin);
            context.CMUser.Add(user1);
            context.CMUser.Add(user2);
            #endregion

            #region Customer
            for (int i = 0; i < FakeData.NumberData.GetNumber(7, 19); i++)
            {
                Customer customer1 = new Customer
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    Password = "customer" + i,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUserName = "recep",
                    MobilePhone = "09878987678",
                    ActivateGuid = Guid.NewGuid()
                };
                context.Customer.Add(customer1);
            }
            #endregion

            #region Product
            for (int i = 0; i < FakeData.NumberData.GetNumber(10, 23); i++)
            {
                Product prd = new Product
                {
                    Name = FakeData.PlaceData.GetCity(),
                    Code = FakeData.NumberData.GetNumber(1, 100).ToString(),
                    Type = FakeData.PlaceData.GetPostCode(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUserName = "recep"
                };
                context.Product.Add(prd);
            }
            #endregion
            #region Category
            for (int i = 0; i < FakeData.NumberData.GetNumber(8, 20); i++)
            {
                Category prd = new Category
                {
                    Name = FakeData.PlaceData.GetCountry(),
                    Description = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(3, 7)),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUserName = "recep"
                };
                context.Category.Add(prd);
            }
            #endregion

            context.SaveChanges();

            #region Incidents
            List<CMUser> cmUserList = context.CMUser.ToList();
            List<Customer> customerList = context.Customer.ToList();
            List<Product> productList = context.Product.ToList();
            List<Category> CategoryList = context.Category.ToList();

            for (int i = 0; i < FakeData.NumberData.GetNumber(50, 100); i++)
            {
                CMUser owner = cmUserList[FakeData.NumberData.GetNumber(0, cmUserList.Count - 1)];
                Customer customer = customerList[FakeData.NumberData.GetNumber(0, cmUserList.Count - 1)];
                Product product = productList[FakeData.NumberData.GetNumber(0, cmUserList.Count - 1)];
                Category category = CategoryList[FakeData.NumberData.GetNumber(0, cmUserList.Count - 1)];

                Incident inc = new Incident
                {
                    Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 19)),
                    Description = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(2, 5)),
                    StartDate = FakeData.DateTimeData.GetDatetime(),
                    EndDate = FakeData.DateTimeData.GetDatetime(),
                    StateCode = FakeData.NumberData.GetNumber(0, 2),
                    Owner = owner,
                    Customer = customer,
                    Product = product,
                    Category = category,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now.AddMinutes(5),
                    ModifiedUserName = "recep"
                };
                context.Incident.Add(inc);
            }
            context.SaveChanges();

            #endregion
        }
    }
}
