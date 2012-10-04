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
            User user = uow.UserRepository.Add(userName);
            Budget budget = uow.BudgetRepository.Create(userName);
            uow.Save();
        }
    }
}