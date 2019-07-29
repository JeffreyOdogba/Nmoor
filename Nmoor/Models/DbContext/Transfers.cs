namespace Nmoor.Models.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transfers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int transferID { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        public int? invoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual User User { get; set; }
    }
}
