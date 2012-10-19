using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Apathy.Models;

namespace Apathy.DAL
{
    public interface IUserService
    {
        User GetUser(string username);
        IEnumerable<User> GetBudgetUsers(string username);
        bool CreateUser(RegisterModel model, string owner, out string errorDescription);
        bool DeleteUser(string username);
    }

    public class UserService : IUserService
    {
        private UnitOfWork uow;

        public UserService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public User GetUser(string username)
        {
            return uow.UserRepository.GetByPK(username);
        }

        public IEnumerable<User> GetBudgetUsers(string username)
        {
            var users = uow.UserRepository.GetByPK(username)
                .Budget
                .Users
                .Where(u => !u.UserName.Equals(username))
                .ToList();

            return users;
        }

        public bool DeleteUser(string username)
        {
            bool success;
            success = Membership.DeleteUser(username);

            if (success)
            {
                User user = uow.UserRepository.GetByPK(username);
                foreach (Transaction transaction in user.Transactions)
                {
                    transaction.UserName = null;
                }
                uow.UserRepository.Delete(user);
                uow.Save();
            }

            return success;
        }

        public bool CreateUser(RegisterModel model, string owner, out string errorDescription)
        {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
            {
                Budget budget;

                if (string.IsNullOrEmpty(owner))
                    budget = new Budget();
                else
                    budget = uow.UserRepository.GetByPK(owner).Budget;

                uow.UserRepository.Insert(new User { UserName = model.UserName, Budget = budget });
                uow.Save();
            }
            else
            {
                errorDescription = ErrorCodeToString(createStatus);
                return false;
            }

            errorDescription = String.Empty;
            return true;
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}