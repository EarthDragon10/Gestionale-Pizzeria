namespace Gestionale_Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliOrdine")]
    public partial class DettagliOrdine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DettagliOrdine()
        {
            Ordini = new HashSet<Ordini>();
        }

        [Key]
        public int IdDettagliOrdine { get; set; }

        public int Quantita { get; set; }

        [Required]
        public string Nota { get; set; }

        public int? Importo { get; set; }

        public int IdProdotto { get; set; }
        public virtual Prodotti Prodotti { get; set; }
        [NotMapped()]
        public string StatoPreparazione { get; set; }
        [NotMapped()]
        public string Confermato { get; set; }

        public static List<DettagliOrdine> ListDettagliOrdine = new List<DettagliOrdine>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
