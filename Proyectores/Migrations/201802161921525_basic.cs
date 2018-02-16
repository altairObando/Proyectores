namespace Proyectores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departamentoes",
                c => new
                    {
                        id_departamento = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 100),
                        mision = c.String(),
                        vision = c.String(),
                        historia = c.String(),
                        ubicacion = c.String(),
                        organizacion = c.String(),
                    })
                .PrimaryKey(t => t.id_departamento);
            
            CreateTable(
                "dbo.Devolucions",
                c => new
                    {
                        id_devolucion = c.Int(nullable: false, identity: true),
                        id_prestamo = c.Int(nullable: false),
                        hora = c.Time(nullable: false, precision: 7),
                        anulada = c.Boolean(),
                    })
                .PrimaryKey(t => t.id_devolucion)
                .ForeignKey("dbo.Prestamoes", t => t.id_prestamo, cascadeDelete: true)
                .Index(t => t.id_prestamo);
            
            CreateTable(
                "dbo.Prestamoes",
                c => new
                    {
                        id_prestamo = c.Int(nullable: false, identity: true),
                        id_proyector = c.Int(nullable: false),
                        id_docente = c.Int(nullable: false),
                        id_responsable = c.String(),
                        fecha = c.DateTime(nullable: false),
                        prestamo_anulado = c.Boolean(),
                        finalizado = c.Boolean(),
                        activo = c.Boolean(nullable: false),
                        Docente_DocenteId = c.Int(),
                        Responsable_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id_prestamo)
                .ForeignKey("dbo.Docentes", t => t.Docente_DocenteId)
                .ForeignKey("dbo.Proyectors", t => t.id_proyector, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Responsable_Id)
                .Index(t => t.id_proyector)
                .Index(t => t.Docente_DocenteId)
                .Index(t => t.Responsable_Id);
            
            CreateTable(
                "dbo.Docentes",
                c => new
                    {
                        DocenteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        id_especialidad = c.Int(nullable: false),
                        id_departamento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocenteId)
                .ForeignKey("dbo.Departamentoes", t => t.id_departamento, cascadeDelete: true)
                .ForeignKey("dbo.Especialidads", t => t.id_especialidad, cascadeDelete: true)
                .Index(t => t.id_especialidad)
                .Index(t => t.id_departamento);
            
            CreateTable(
                "dbo.Especialidads",
                c => new
                    {
                        id_especialidad = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.id_especialidad);
            
            CreateTable(
                "dbo.Proyectors",
                c => new
                    {
                        id_proyector = c.Int(nullable: false, identity: true),
                        id_marca = c.Int(nullable: false),
                        id_estado = c.Int(nullable: false),
                        nombre = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_proyector)
                .ForeignKey("dbo.Estadoes", t => t.id_estado, cascadeDelete: true)
                .ForeignKey("dbo.Marcas", t => t.id_marca, cascadeDelete: true)
                .Index(t => t.id_marca)
                .Index(t => t.id_estado);
            
            CreateTable(
                "dbo.Estadoes",
                c => new
                    {
                        id_estado = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.id_estado);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        id_marca = c.Int(nullable: false, identity: true),
                        marca = c.String(),
                    })
                .PrimaryKey(t => t.id_marca);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        Foto_Perfil = c.Binary(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Prestamoes", "Responsable_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Prestamoes", "id_proyector", "dbo.Proyectors");
            DropForeignKey("dbo.Proyectors", "id_marca", "dbo.Marcas");
            DropForeignKey("dbo.Proyectors", "id_estado", "dbo.Estadoes");
            DropForeignKey("dbo.Prestamoes", "Docente_DocenteId", "dbo.Docentes");
            DropForeignKey("dbo.Docentes", "id_especialidad", "dbo.Especialidads");
            DropForeignKey("dbo.Docentes", "id_departamento", "dbo.Departamentoes");
            DropForeignKey("dbo.Devolucions", "id_prestamo", "dbo.Prestamoes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Proyectors", new[] { "id_estado" });
            DropIndex("dbo.Proyectors", new[] { "id_marca" });
            DropIndex("dbo.Docentes", new[] { "id_departamento" });
            DropIndex("dbo.Docentes", new[] { "id_especialidad" });
            DropIndex("dbo.Prestamoes", new[] { "Responsable_Id" });
            DropIndex("dbo.Prestamoes", new[] { "Docente_DocenteId" });
            DropIndex("dbo.Prestamoes", new[] { "id_proyector" });
            DropIndex("dbo.Devolucions", new[] { "id_prestamo" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Marcas");
            DropTable("dbo.Estadoes");
            DropTable("dbo.Proyectors");
            DropTable("dbo.Especialidads");
            DropTable("dbo.Docentes");
            DropTable("dbo.Prestamoes");
            DropTable("dbo.Devolucions");
            DropTable("dbo.Departamentoes");
        }
    }
}
