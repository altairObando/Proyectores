namespace Proyectores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docente_logical : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Docentes", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Docentes", "Activo");
        }
    }
}
