using Microsoft.EntityFrameworkCore;
using TestTechniqueDotnet.Models;

namespace TestTechniqueDotnet.Context;

public class PetStoreContext : DbContext
{
    public PetStoreContext(DbContextOptions<PetStoreContext> options) : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Animal> Animals { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(b =>
        {
            b.HasMany(e => e.Transactions)
                .WithOne()
                .HasForeignKey(x => x.ClientId)
                .IsRequired();
            b.HasMany(c => c.Animals)
                .WithOne(x => x.Master)
                .HasForeignKey(x => x.MasterId);
        });
        
        modelBuilder.Entity<Animal>(b =>
        {   
            b.HasOne(x => x.Transaction)
                .WithOne()
                .HasForeignKey<Transaction>(x => x.AnimalId)
                .IsRequired();
        });
    }
}