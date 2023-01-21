namespace Gestionale_Pizzeria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedDettagliOrdine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DettagliOrdine", "StatoPreparazione", c => c.String());
            AddColumn("dbo.DettagliOrdine", "Confermato", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DettagliOrdine", "Confermato");
            DropColumn("dbo.DettagliOrdine", "StatoPreparazione");
        }
    }
}
