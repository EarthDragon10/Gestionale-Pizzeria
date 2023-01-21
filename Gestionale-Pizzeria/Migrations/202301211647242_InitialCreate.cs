namespace Gestionale_Pizzeria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DettagliOrdine",
                c => new
                    {
                        IdDettagliOrdine = c.Int(nullable: false, identity: true),
                        Quantita = c.Int(nullable: false),
                        Nota = c.String(nullable: false),
                        Importo = c.Int(),
                        IdProdotto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDettagliOrdine)
                .ForeignKey("dbo.Prodotti", t => t.IdProdotto)
                .Index(t => t.IdProdotto);
            
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        IdOrdine = c.Int(nullable: false, identity: true),
                        Prezzo = c.Int(),
                        Evaso = c.Boolean(),
                        Confermato = c.String(nullable: false),
                        IdUtente = c.Int(nullable: false),
                        IdDettagliOrdine = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOrdine)
                .ForeignKey("dbo.Utenti", t => t.IdUtente)
                .ForeignKey("dbo.DettagliOrdine", t => t.IdDettagliOrdine)
                .Index(t => t.IdUtente)
                .Index(t => t.IdDettagliOrdine);
            
            CreateTable(
                "dbo.Utenti",
                c => new
                    {
                        IdUtente = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        Pwd = c.String(nullable: false, maxLength: 20),
                        Nome = c.String(nullable: false),
                        Cognome = c.String(nullable: false),
                        Indirizzo = c.String(),
                        IdRuolo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdUtente)
                .ForeignKey("dbo.Ruoli", t => t.IdRuolo)
                .Index(t => t.IdRuolo);
            
            CreateTable(
                "dbo.Ruoli",
                c => new
                    {
                        IdRuolo = c.Int(nullable: false, identity: true),
                        TipoRuoli = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdRuolo);
            
            CreateTable(
                "dbo.Prodotti",
                c => new
                    {
                        IdProdotto = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        UrlImg = c.String(),
                        PrezzoVendita = c.Decimal(nullable: false, storeType: "money"),
                        TempoDiPreparazione = c.Int(nullable: false),
                        Ingredienti = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IdProdotto);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DettagliOrdine", "IdProdotto", "dbo.Prodotti");
            DropForeignKey("dbo.Ordini", "IdDettagliOrdine", "dbo.DettagliOrdine");
            DropForeignKey("dbo.Utenti", "IdRuolo", "dbo.Ruoli");
            DropForeignKey("dbo.Ordini", "IdUtente", "dbo.Utenti");
            DropIndex("dbo.Utenti", new[] { "IdRuolo" });
            DropIndex("dbo.Ordini", new[] { "IdDettagliOrdine" });
            DropIndex("dbo.Ordini", new[] { "IdUtente" });
            DropIndex("dbo.DettagliOrdine", new[] { "IdProdotto" });
            DropTable("dbo.Prodotti");
            DropTable("dbo.Ruoli");
            DropTable("dbo.Utenti");
            DropTable("dbo.Ordini");
            DropTable("dbo.DettagliOrdine");
        }
    }
}
