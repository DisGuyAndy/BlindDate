using BlindDate.Api.Models;
using BlindDate.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace BlindDate.Api.Data.Models
{
    public class ApplicationUserProfile
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateRegistered { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayUsername { get; set; }
        public string DisplayUsernameId { get; set; }
        public string MSISDN { get; set; }
        public string Email { get; set; }
        [MaxLength(200)]
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string ProfileImage { get; set; }
        public int Avatar { get; set; }
        public bool PolicyHolder { get; set; }
        [MaxLength(10)]
        public string EmailOneTimePin { get; set; }
        [MaxLength(10)]
        public string MSISDNOneTimePin { get; set; }
        public bool ContactNumberValidated { get; set; }
        public bool EmailValidated { get; set; }
        public DbGeography LastKnownLocation { get; set; }
        public ProfileType UserType { get; set; }
        public DateTimeOffset? LastActivityDate { get; set; }
        public DateTimeOffset? UninstallDate { get; set; }
        public RegistrationStatus RegistrationStatus { get; set; }
    }
}