namespace BlindDate.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProfileforotpwork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUserProfiles", "DisplayUsernameId", c => c.String());
            AddColumn("dbo.ApplicationUserProfiles", "Country", c => c.String(maxLength: 200));
            AddColumn("dbo.ApplicationUserProfiles", "Province", c => c.String());
            AddColumn("dbo.ApplicationUserProfiles", "City", c => c.String());
            AddColumn("dbo.ApplicationUserProfiles", "EmailOneTimePin", c => c.String(maxLength: 10));
            AddColumn("dbo.ApplicationUserProfiles", "MSISDNOneTimePin", c => c.String(maxLength: 10));
            AddColumn("dbo.ApplicationUserProfiles", "EmailValidated", c => c.Boolean(nullable: false));
            DropColumn("dbo.ApplicationUserProfiles", "IdNumber");
            DropColumn("dbo.ApplicationUserProfiles", "Region");
            DropColumn("dbo.ApplicationUserProfiles", "OneTimePin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUserProfiles", "OneTimePin", c => c.String(maxLength: 10));
            AddColumn("dbo.ApplicationUserProfiles", "Region", c => c.String(maxLength: 200));
            AddColumn("dbo.ApplicationUserProfiles", "IdNumber", c => c.String());
            DropColumn("dbo.ApplicationUserProfiles", "EmailValidated");
            DropColumn("dbo.ApplicationUserProfiles", "MSISDNOneTimePin");
            DropColumn("dbo.ApplicationUserProfiles", "EmailOneTimePin");
            DropColumn("dbo.ApplicationUserProfiles", "City");
            DropColumn("dbo.ApplicationUserProfiles", "Province");
            DropColumn("dbo.ApplicationUserProfiles", "Country");
            DropColumn("dbo.ApplicationUserProfiles", "DisplayUsernameId");
        }
    }
}
