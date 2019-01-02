using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlindDate.Common.Models
{
    public class RegistrationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayUsername { get; set; }
        public string DisplayUsernameId { get; set; }
        public string MSISDN { get; set; }
        public string Email { get; set; }
    }
}
