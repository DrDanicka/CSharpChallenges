using CSharpChallenge.Models;

namespace CSharpChallenge.Services
{
    /// <summary>
    /// Service class responsible for user authentication and registration validation.
    /// </summary>
    public class SecurityService
    {
        private readonly UsersDAO _usersDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityService"/> class.
        /// </summary>
        /// <param name="usersDAO">Data access object for user-related operations.</param>
        public SecurityService(UsersDAO usersDAO)
        {
            _usersDAO = usersDAO;
        }

        /// <summary>
        /// Checks if the login credentials provided are valid.
        /// </summary>
        /// <param name="user">The user model containing username and password.</param>
        /// <returns>True if the login credentials are valid, false otherwise.</returns>
        public bool IsValidLogin(UserModel user)
        {
            return _usersDAO.FindUserByUserNameAndPassword(user);
        }

        /// <summary>
        /// Checks if a username already exists in the database.
        /// </summary>
        /// <param name="user">The user model containing the username to check.</param>
        /// <returns>True if the username exists, false otherwise.</returns>
        public bool DoesUserNameExist(UserModel user)
        {
            return _usersDAO.FindUserByUserName(user);
        }

        /// <summary>
        /// Checks if the registration details provided are valid (username and email are unique).
        /// </summary>
        /// <param name="user">The user model containing username and email.</param>
        /// <returns>True if the registration details are valid, false otherwise.</returns>
        public bool AreValidRegistrationDetails(UserModel user)
        {
            return !_usersDAO.FindUserByUserNameOrEmail(user);
        }
    }
}
