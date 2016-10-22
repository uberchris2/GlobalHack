namespace GlobalHack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Referrals", "Person_Id", "dbo.People");
            DropIndex("dbo.Referrals", new[] { "Person_Id" });
            DropColumn("dbo.Referrals", "PersonId");
            RenameColumn(table: "dbo.Referrals", name: "Person_Id", newName: "PersonId");
            AlterColumn("dbo.Referrals", "PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.Referrals", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Referrals", "PersonId");
            AddForeignKey("dbo.Referrals", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Referrals", "PersonId", "dbo.People");
            DropIndex("dbo.Referrals", new[] { "PersonId" });
            AlterColumn("dbo.Referrals", "PersonId", c => c.Int());
            AlterColumn("dbo.Referrals", "PersonId", c => c.String());
            RenameColumn(table: "dbo.Referrals", name: "PersonId", newName: "Person_Id");
            AddColumn("dbo.Referrals", "PersonId", c => c.String());
            CreateIndex("dbo.Referrals", "Person_Id");
            AddForeignKey("dbo.Referrals", "Person_Id", "dbo.People", "Id");
        }
    }
}
