namespace Nmoor.Models.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Transfers = new HashSet<Transfers>();
        }

        [Key]
        [StringLength(50)]
        [Display(Name ="Username")]
        public string username { get; set; }

        [StringLength(255)]
        public string password { get; set; }

        [Display(Name = "Balance")]
        public decimal? balance { get; set; }

        [StringLength(10)]
        public string status { get; set; }

        [StringLength(150)]
        [Display(Name = "Token")]
        public string token { get; set; }

        [StringLength(100)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? signupdate { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Last signed in")]
        public DateTime? recentsignin { get; set; }

        [StringLength(50)]
        [Display(Name = "Full name")]
        public string fullname { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transfers> Transfers { get; set; }
    }
}
