using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlindDate.Common.Models
{
    public class UploadImageRequest
    {
        public string Base64 { get; set; }
        public string ProfileId { get; set; }
        public int ImageSequenceId { get; set; }
        public bool IsMainProfileImage { get; set; }
    }
}
