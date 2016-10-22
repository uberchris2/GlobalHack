namespace GlobalHack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstablishRelationships : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Referrals", "Person_Id", c => c.Int());
            CreateIndex("dbo.Referrals", "ShelterId");
            CreateIndex("dbo.Referrals", "Person_Id");
            CreateIndex("dbo.Reservations", "PersonId");
            CreateIndex("dbo.Reservations", "ShelterId");
            AddForeignKey("dbo.Referrals", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.Reservations", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Referrals", "ShelterId", "dbo.Shelters", "Id");
            AddForeignKey("dbo.Reservations", "ShelterId", "dbo.Shelters", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ShelterId", "dbo.Shelters");
            DropForeignKey("dbo.Referrals", "ShelterId", "dbo.Shelters");
            DropForeignKey("dbo.Reservations", "PersonId", "dbo.People");
            DropForeignKey("dbo.Referrals", "Person_Id", "dbo.People");
            DropIndex("dbo.Reservations", new[] { "ShelterId" });
            DropIndex("dbo.Reservations", new[] { "PersonId" });
            DropIndex("dbo.Referrals", new[] { "Person_Id" });
            DropIndex("dbo.Referrals", new[] { "ShelterId" });
            DropColumn("dbo.Referrals", "Person_Id");
        }
    }
}
