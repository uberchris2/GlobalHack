namespace GlobalHack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BirthYear = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        NumChildren = c.Int(nullable: false),
                        Pregnant = c.Boolean(nullable: false),
                        Transgender = c.Boolean(nullable: false),
                        SexOffender = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Referrals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.String(),
                        Title = c.String(),
                        Notes = c.String(),
                        ShelterId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        ShelterId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Confirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shelters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Location = c.String(),
                        Beds = c.Int(nullable: false),
                        MinAge = c.Int(nullable: false),
                        MaxAge = c.Int(nullable: false),
                        GenderRestriction = c.Int(nullable: false),
                        PregnantOnly = c.Boolean(nullable: false),
                        SexOffenderRestriction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shelters");
            DropTable("dbo.Reservations");
            DropTable("dbo.Referrals");
            DropTable("dbo.People");
        }
    }
}
