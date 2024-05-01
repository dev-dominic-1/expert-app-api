using ExpertAppApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpertAppApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        EntityTypeBuilder<ExpertPhotoUrl> expertPhotoUrl = modelBuilder.Entity<ExpertPhotoUrl>();
        expertPhotoUrl.HasOne(e => e.Expert).WithOne(e => e.PhotoUrl).OnDelete(DeleteBehavior.Cascade);

        EntityTypeBuilder<ExpertFees> expertFees = modelBuilder.Entity<ExpertFees>();
        expertFees.HasOne(e => e.Expert).WithOne(e => e.Fees).OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<Expert> Expert { get; set; }
    public DbSet<ExpertPhotoUrl> ExpertPhotoUrl { get; set; }
    public DbSet<ExpertFees> ExpertFee { get; set; }
}