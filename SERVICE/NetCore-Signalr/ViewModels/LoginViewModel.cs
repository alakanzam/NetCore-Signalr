using System.ComponentModel.DataAnnotations;

namespace SignalrCore.ViewModels
{
    public class LoginViewModel
    {
        #region Properties

        /// <summary>
        /// Email address.
        /// </summary>
        [Required]
        public string Email { get; set; }

        #endregion
    }
}