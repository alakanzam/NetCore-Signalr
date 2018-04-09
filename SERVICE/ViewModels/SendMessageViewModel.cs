using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SignalrCore.ViewModels
{
    public class SendMessageViewModel
    {
        #region Properties

        /// <summary>
        /// Client indexes.
        /// </summary>
        public IList<string> ClientIds { get; set; }

        [Required]
        public string EventName { get; set; }

        public IDictionary Item { get; set; }

        #endregion
    }
}