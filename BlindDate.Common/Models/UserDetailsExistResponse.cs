using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlindDate.Common.Models
{
    public class UserDetailsExistResponse: ResponseBase
    {
        public bool IdNumberExists { get; set; }
        public bool MSISDNExists { get; set; }
        public bool EmailExists { get; set; }
    }
}
