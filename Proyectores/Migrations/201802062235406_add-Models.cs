namespace Proyectores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Personas", "id_especialidad", "dbo.Especialidads");
            DropForeignKey("dbo.Personas", "id_departamento", "dbo.Departamentoes");
            DropForeignKey("dbo.Prestamoes", "id_proyector", "dbo.Proyectors");
            DropForeignKey("dbo.Prestamoes", "Docente_id_persona", "dbo.Personas");
            DropForeignKey("dbo.Prestamoes", "Responsable_id_persona", "dbo.Personas");
            DropForeignKey("dbo.Proyectors", "id_marca", "dbo.Marcas");
            DropForeignKey("dbo.Proyectors", "id_estado", "dbo.Estadoes");
            DropForeignKey("dbo.Devolucions", "id_prestamo", "dbo.Prestamoes");
            DropIndex("dbo.Personas", new[] { "id_especialidad" });
            DropIndex("dbo.Personas", new[] { "id_departamento" });
            DropIndex("dbo.Prestamoes", new[] { "id_proyector" });
            DropIndex("dbo.Prestamoes", new[] { "Docente_id_persona" });
            DropIndex("dbo.Prestamoes", new[] { "Responsable_id_persona" });
            DropIndex("dbo.Proyectors", new[] { "id_marca" });
            DropIndex("dbo.Proyectors", new[] { "id_estado" });
            DropIndex("dbo.Devolucions", new[] { "id_prestamo" });
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            DropTable("dbo.Personas");
            DropTable("dbo.Especialidads");
            DropTable("dbo.Departamentoes");
            DropTable("dbo.Prestamoes");
            DropTable("dbo.Proyectors");
            DropTable("dbo.Marcas");
            DropTable("dbo.Estadoes");
            DropTable("dbo.Devolucions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Devolucions",
                c => new
                    {
                        id_devolucion = c.Int(nullable: false, identity: true),
                        id_prestamo = c.Int(nullable: false),
                        hora = c.Time(nullable: false, precision: 7),
                        anulada = c.Boolean(),
                    })
                .PrimaryKey(t => t.id_devolucion);
            
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
                "dbo.Proyectors",
                c => new
                    {
                        id_proyector = c.Int(nullable: false, identity: true),
                        id_marca = c.Int(nullable: false),
                        id_estado = c.Int(nullable: false),
                        nombre = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_proyector);
            
            CreateTable(
                "dbo.Prestamoes",
                c => new
                    {
                        id_prestamo = c.Int(nullable: false, identity: true),
                        id_proyector = c.Int(nullable: false),
                        id_docente = c.Int(nullable: false),
                        id_responsable = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        prestamo_anulado = c.Boolean(),
                        finalizado = c.Boolean(),
                        activo = c.Boolean(nullable: false),
                        Docente_id_persona = c.Int(),
                        Responsable_id_persona = c.Int(),
                    })
                .PrimaryKey(t => t.id_prestamo);
            
            CreateTable(
                "dbo.Departamentoes",
                c => new
                    {
                        id_departamento = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        mision = c.String(),
                        vision = c.String(),
                        historia = c.String(),
                        ubicacion = c.String(),
                        organizacion = c.String(),
                    })
                .PrimaryKey(t => t.id_departamento);
            
            CreateTable(
                "dbo.Especialidads",
                c => new
                    {
                        id_especialidad = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                    })
                .PrimaryKey(t => t.id_especialidad);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        id_persona = c.Int(nullable: false, identity: true),
                        nombre = c.String(maxLength: 35),
                        apellidos = c.String(),
                        telefono = c.String(),
                        correo = c.String(),
                        imagen = c.Binary(),
                        activo = c.Boolean(nullable: false),
                        id_especialidad = c.Int(),
                        id_departamento = c.Int(),
                        informacion_adicional = c.String(),
                        username = c.String(maxLength: 20),
                        password = c.String(),
                        is_staff = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id_persona);
            
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            CreateIndex("dbo.Devolucions", "id_prestamo");
            CreateIndex("dbo.Proyectors", "id_estado");
            CreateIndex("dbo.Proyectors", "id_marca");
            CreateIndex("dbo.Prestamoes", "Responsable_id_persona");
            CreateIndex("dbo.Prestamoes", "Docente_id_persona");
            CreateIndex("dbo.Prestamoes", "id_proyector");
            CreateIndex("dbo.Personas", "id_departamento");
            CreateIndex("dbo.Personas", "id_especialidad");
            AddForeignKey("dbo.Devolucions", "id_prestamo", "dbo.Prestamoes", "id_prestamo", cascadeDelete: true);
            AddForeignKey("dbo.Proyectors", "id_estado", "dbo.Estadoes", "id_estado", cascadeDelete: true);
            AddForeignKey("dbo.Proyectors", "id_marca", "dbo.Marcas", "id_marca", cascadeDelete: true);
            AddForeignKey("dbo.Prestamoes", "Responsable_id_persona", "dbo.Personas", "id_persona");
            AddForeignKey("dbo.Prestamoes", "Docente_id_persona", "dbo.Personas", "id_persona");
            AddForeignKey("dbo.Prestamoes", "id_proyector", "dbo.Proyectors", "id_proyector", cascadeDelete: true);
            AddForeignKey("dbo.Personas", "id_departamento", "dbo.Departamentoes", "id_departamento", cascadeDelete: true);
            AddForeignKey("dbo.Personas", "id_especialidad", "dbo.Especialidads", "id_especialidad", cascadeDelete: true);
        }
    }
}
