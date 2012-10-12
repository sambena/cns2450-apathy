using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface IUserService
    {
        void CreateUser(string userName);
    }

    public class UserService : IUserService
    {
        private UnitOfWork uow;

        public UserService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateUser(string userName)
        {
            uow.UserRepository.Insert(new User { UserName = userName });
            uow.BudgetRepository.Insert(new Budget { Owner = userName });
            uow.Save();
        }
    }
}