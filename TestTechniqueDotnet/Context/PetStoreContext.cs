using Microsoft.EntityFrameworkCore;
using TestTechniqueDotnet.Models;

namespace TestTechniqueDotnet.Context;

public class PetStoreContext : DbContext
{
    public PetStoreContext(DbContextOptions<PetStoreContext> options) : base(options)
    {
    }

    public DbSet<Client>? Clients { get; set; }
    public DbSet<Animal>? Animals { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(b =>
        {
            b.HasMany(e => e.Transactions)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId)
                .IsRequired();
            b.HasMany(c => c.Animals)
                .WithOne(x => x.Master);
        });
        
        modelBuilder.Entity<Animal>(b =>
        {   
            b.HasOne(e => e.Transaction)
                .WithOne(x => x.Animal)
                .IsRequired();
        });
    }
}