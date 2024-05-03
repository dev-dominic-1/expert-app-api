﻿using ExpertAppApi.Entities;
using ExpertAppApi.Entities.Call;
using ExpertAppApi.Entities.User;
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

        EntityTypeBuilder<Call> call = modelBuilder.Entity<Call>();
        call.HasOne(e => e.Expert).WithMany(e => e.Calls).OnDelete(DeleteBehavior.Cascade);

        EntityTypeBuilder<CallDetails> callDetails = modelBuilder.Entity<CallDetails>();
        callDetails.HasOne(e => e.Call).WithOne(e => e.CallDetails).OnDelete(DeleteBehavior.Cascade);

        EntityTypeBuilder<User> user = modelBuilder.Entity<User>();
        user.Property(e => e.Password);
    }

    // EXPERT DATA
    public DbSet<Expert> Expert { get; set; }
    public DbSet<ExpertPhotoUrl> ExpertPhotoUrl { get; set; }
    public DbSet<ExpertFees> ExpertFee { get; set; }
    
    // CALL DATA
    public DbSet<Call> Call { get; set; }
    public DbSet<CallDetails> CallDetails { get; set; }
    
    // USER DATA
    public DbSet<User> User { get; set; }
}