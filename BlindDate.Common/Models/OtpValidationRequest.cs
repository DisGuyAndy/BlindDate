using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlindDate.Common.Models
{
    public class OtpValidationRequest
    {
        public string Otp { get; set; }
        public string ProfileId { get; set; }
    }
}
