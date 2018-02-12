namespace Proyectores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201802101901524_marcas_edit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prestamoes", "id_responsable", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prestamoes", "id_responsable", c => c.Int(nullable: false));
        }
    }
}
