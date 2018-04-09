using System;

namespace SignalrCore.Models
{
    public class User
    {
        #region Properties

        public Guid Id { get; set; }

        public string Email { get; set; }

        #endregion

        #region Constructor

        public User(string email)
        {
            Id = Guid.NewGuid();
            Email = email;
        }

        #endregion
    }
}