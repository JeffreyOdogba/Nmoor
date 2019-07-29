namespace Nmoor.Models.DbContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NmoorEntity : DbContext
    {
        public NmoorEntity()
            : base("name=NmoorEntity")
        {
        }

        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<Transfers> Transfers { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .Property(e => e.senderUsername)
                .IsUnicode(false);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.receiverUsername)
                .IsUnicode(false);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.transactionType)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Transfers>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.balance)
                .HasPrecision(10, 2);

            modelBuilder.Entity<User>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.token)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.fullname)
                .IsUnicode(false);
        }
    }
}
