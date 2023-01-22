namespace Gestionale_Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordini")]
    public partial class Ordini
    {
        [Key]
        public int IdOrdine { get; set; }

        public int? Prezzo { get; set; }

        public bool? Evaso { get; set; }

        [Required]
        public string Confermato { get; set; }

        public int IdUtente { get; set; }

        public int IdDettagliOrdine { get; set; }

        public virtual DettagliOrdine DettagliOrdine { get; set; }

        public virtual Utenti Utenti { get; set; }

        public static int TotaleOrdiniEvasi { get; set; }
        public static int? TotaleIncassato { get; set; }
    }
}
