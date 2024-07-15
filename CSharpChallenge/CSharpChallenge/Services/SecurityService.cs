using CSharpChallenge.Models;

namespace CSharpChallenge.Services
{
    public class SecurityService
    {
        UsersDAO usersDAO = new UsersDAO();

        public bool IsValidLogin(UserModel user)
        {
            return usersDAO.FindUserByUserNameAndPassword(user);
        }
        
        public bool DoesUserNameExist(UserModel user)
        {
            return usersDAO.FindUserByUserName(user);
        }
        public bool AreValidRegistrationDetails(UserModel user)
        {
            return !usersDAO.FindUserByUserNameOrEmail(user);
        }
    }
}
