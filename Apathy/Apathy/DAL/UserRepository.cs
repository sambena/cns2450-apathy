using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface IUserRepository
    {
        User GetByUserName(string userName);
        User Add(string userName);
    }

    public class UserRepository : IUserRepository
    {
        private BudgetContext context;

        public UserRepository(BudgetContext context)
        {
            this.context = context;
        }

        public User GetByUserName(string userName)
        {
            return context.Users.Single(u => u.UserName == userName);
        }

        public User Add(string userName)
        {
            User user = context.Users.Add(new User { UserName = userName });
            return user;
        }
    }
}