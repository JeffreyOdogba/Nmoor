namespace Nmoor.Models.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int transcationId { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(15)]
        public string transactionType { get; set; }

        public decimal? amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfTransaction { get; set; }

        public TimeSpan? timeOfTransaction { get; set; }

        [StringLength(15)]
        public string status { get; set; }
    }
}
