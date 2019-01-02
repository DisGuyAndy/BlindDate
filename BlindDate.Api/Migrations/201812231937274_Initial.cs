namespace BlindDate.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7),
                        DateRegistered = c.DateTimeOffset(precision: 7),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DisplayUsername = c.String(),
                        MSISDN = c.String(),
                        Email = c.String(),
                        IdNumber = c.String(),
                        Region = c.String(maxLength: 200),
                        ProfileImage = c.String(),
                        Avatar = c.Int(nullable: false),
                        PolicyHolder = c.Boolean(nullable: false),
                        OneTimePin = c.String(maxLength: 10),
                        ContactNumberValidated = c.Boolean(nullable: false),
                        LastKnownLocation = c.Geography(),
                        UserType = c.Int(nullable: false),
                        LastActivityDate = c.DateTimeOffset(precision: 7),
                        UninstallDate = c.DateTimeOffset(precision: 7),
                        RegistrationStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationUserProfiles");
        }
    }
}
