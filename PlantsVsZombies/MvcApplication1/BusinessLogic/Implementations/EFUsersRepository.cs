using BusinessLogic.Interfaces;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace BusinessLogic.Implementations
{
    public class EFUsersRepository : IUsersRepository
    {
        private EFDbContext context;

        public EFUsersRepository(EFDbContext context)
        {
            this.context = context;
        }

        public MembershipUser GetMembershipUserByName(string userName)
        {
 	        User user = context.Users.FirstOrDefault(x => x.UserName == userName);
            if (user != null)
            {
                return new MembershipUser(
                    "CustomMembershipProvider",
                    user.UserName,
                    user.Id,
                    user.Email,
                    "",
                    null,
                    true,
                    false,
                    user.CreatedDate,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now
                    );            
            }
            return null;
        }

        public string GetUserNameByEmail(string email)
        {
 	        User user = context.Users.FirstOrDefault(x => x.Email == email);
            return user != null ? user.UserName : "";
        }

        public void CreateUser(string username, string password, string email, string firstName, string lastName, string middleName)
        {
            User user = new User
            {
                UserName = username,
                Password = password,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                CreatedDate = DateTime.Now
            };
            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool ValidateUser(string userName, string password)
        {
            User user = context.Users.FirstOrDefault(x => x.UserName == userName);
            return user != null && user.Password == password;
        }

        public IEnumerable<Int32> SearchUsers(String firstName, String lastName, String middleName)
        {
            if (String.IsNullOrEmpty(firstName) && String.IsNullOrEmpty(lastName) && String.IsNullOrEmpty(middleName))
                return (from u in context.Users select u.Id).ToList();
            
            return (from u in context.Users
                    where u.FirstName.ToLower().StartsWith(firstName.ToLower())
                          && u.LastName.ToLower().StartsWith(lastName.ToLower())
                          && u.MiddleName.ToLower().StartsWith(middleName.ToLower())
                    select u.Id).ToList();
        }

        public String GetFirstNameById(Int32 id)
        {
            return (from u in context.Users
                   where u.Id == id
                   select u.FirstName).FirstOrDefault();    
        }

        public String GetLastNameById(Int32 id)
        {
            return (from u in context.Users
                    where u.Id == id
                    select u.LastName).FirstOrDefault();
        }

        public String GetMiddleNameById(Int32 id)
        {
            return (from u in context.Users
                    where u.Id == id
                    select u.MiddleName).FirstOrDefault();
        }
    }
}
