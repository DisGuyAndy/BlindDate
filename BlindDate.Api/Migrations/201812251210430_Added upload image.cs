namespace BlindDate.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeduploadimage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.String(),
                        SequenceId = c.Int(nullable: false),
                        S3ImageLocation = c.String(),
                        IsMainProfileImage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProfileImages");
        }
    }
}
