using BlindDate.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlindDate.Api.Models
{
    public class UploadToS3Response: ResponseBase
    {
        public string S3FilePath { get; set; }
    }
}