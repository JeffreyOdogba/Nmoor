namespace Nmoor.Models.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            Transfers = new HashSet<Transfers>();
        }

        public int invoiceId { get; set; }

        [StringLength(50)]
        [Display(Name = "Sender")]
        public string senderUsername { get; set; }

        [StringLength(50)]
        public string receiverUsername { get; set; }
        [Display(Name = "Amount")]
        public decimal? amount { get; set; }

        [Display(Name = "Date")]
        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public TimeSpan? time { get; set; }

        [Display(Name = "Status")]
        [StringLength(30)]
        public string status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transfers> Transfers { get; set; }
    }
}
