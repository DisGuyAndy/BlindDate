using BlindDate.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlindDate.Api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<ApplicationUserProfile> ApplicationUserProfiles { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }

        

        public ApplicationDbContext() : base(ConfigurationManager.AppSettings["data:sqlserver:connectionstring"])
        {

        }
    }
}