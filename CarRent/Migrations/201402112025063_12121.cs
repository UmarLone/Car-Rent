namespace FinanceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12121 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        BookingId = c.Long(nullable: false, identity: true),
                        CarId = c.Long(nullable: false),
                        RentalPlanId = c.Long(nullable: false),
                        Name = c.String(),
                        SurName = c.String(),
                        Email = c.String(),
                        HomePhone = c.String(),
                        Mobile = c.String(),
                        HouseNumber = c.String(),
                        Street = c.String(),
                        Town = c.String(),
                        PostCode = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        StartTime = c.String(),
                        StartDeopt = c.Int(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        ReturnTime = c.String(),
                        ReturnDeopt = c.String(),
                        Status = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        EntryBy = c.Long(nullable: false),
                        UpdateBy = c.Long(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        Type = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Car", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.RentalPlan", t => t.RentalPlanId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.RentalPlanId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Booking", new[] { "RentalPlanId" });
            DropIndex("dbo.Booking", new[] { "CarId" });
            DropForeignKey("dbo.Booking", "RentalPlanId", "dbo.RentalPlan");
            DropForeignKey("dbo.Booking", "CarId", "dbo.Car");
            DropTable("dbo.Booking");
        }
    }
}
