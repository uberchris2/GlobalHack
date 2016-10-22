namespace GlobalHack.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Username", c => c.String());
            AddColumn("dbo.People", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Password");
            DropColumn("dbo.People", "Username");
        }
    }
}
