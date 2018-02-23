namespace Proyectores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dpto_validaciones : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departamentoes", "mision", c => c.String());
            AlterColumn("dbo.Departamentoes", "vision", c => c.String());
            AlterColumn("dbo.Departamentoes", "historia", c => c.String());
            AlterColumn("dbo.Departamentoes", "ubicacion", c => c.String());
            AlterColumn("dbo.Departamentoes", "organizacion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departamentoes", "organizacion", c => c.String(nullable: false));
            AlterColumn("dbo.Departamentoes", "ubicacion", c => c.String(nullable: false));
            AlterColumn("dbo.Departamentoes", "historia", c => c.String(nullable: false));
            AlterColumn("dbo.Departamentoes", "vision", c => c.String(nullable: false));
            AlterColumn("dbo.Departamentoes", "mision", c => c.String(nullable: false));
        }
    }
}
