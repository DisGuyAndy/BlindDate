using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlindDate.Api.Data.Models
{
    public class ProfileImage
    {
        [Key]
        public int Id { get; set; }
        public string ProfileId { get; set; }
        public int SequenceId { get; set; }
        public string S3ImageLocation { get; set; }
        public bool IsMainProfileImage { get; set; }
    }
}