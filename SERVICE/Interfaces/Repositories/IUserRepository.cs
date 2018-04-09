using System.Collections.Generic;
using SignalrCore.Models;

namespace SignalrCore.Interfaces.Repositories
{
    public interface IUserRepository
    {
        #region Methods

        /// <summary>
        /// Get users in repository.
        /// </summary>
        IList<User> Get();

        /// <summary>
        /// Add an user to repository.
        /// </summary>
        /// <param name="user"></param>
        void Add(User user);

        #endregion
    }
}