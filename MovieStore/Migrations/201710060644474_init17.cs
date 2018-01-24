namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "image");
        }
    }
}
