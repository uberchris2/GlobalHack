namespace GlobalHack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "NoShow", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "NoShow");
        }
    }
}
