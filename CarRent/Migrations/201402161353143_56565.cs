namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _56565 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactsId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        EmailId = c.String(),
                        Suggestion = c.String(),
                        EntryDate = c.DateTime(nullable: false),
                        EntryBy = c.Long(nullable: false),
                        UpdateDate = c.DateTime(),
                        UpdateBy = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Subject = c.String(),
                        Phone = c.String(),
                        Location = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
