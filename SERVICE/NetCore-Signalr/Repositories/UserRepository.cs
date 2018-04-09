using System.Collections.Generic;
using SignalrCore.Interfaces.Repositories;
using SignalrCore.Models;

namespace SignalrCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Properties

        /// <summary>
        /// List of users.
        /// </summary>
        private readonly IList<User> _users;

        #endregion

        #region Methods

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IList<User> Get()
        {
            return _users;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            _users.Add(user);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize repository.
        /// </summary>
        public UserRepository()
        {
            _users = new List<User>();
            _users.Add(new User("a1@gmail.com"));
            _users.Add(new User("a2@gmail.com"));
            _users.Add(new User("a3@gmail.com"));
        }

        #endregion
    }
}