namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init25 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            AddColumn("dbo.Orders", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Orders", "CustomerId", c => c.String());
            CreateIndex("dbo.Orders", "Customer_Id");
            AddForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            AlterColumn("dbo.Orders", "CustomerId", c => c.Int());
            DropColumn("dbo.Orders", "Customer_Id");
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id");
        }
    }
}
