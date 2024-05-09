using ExpertAppApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpertAppApi.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // USER MODEL
        EntityTypeBuilder<User> user = modelBuilder.Entity<User>();
        user.HasOne(e => e.PhotoUrl).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
        
        // EXPERT MODEL
        EntityTypeBuilder<ExpertPhotoUrl> expertPhotoUrl = modelBuilder.Entity<ExpertPhotoUrl>();
        expertPhotoUrl.HasOne(e => e.Expert).WithOne(e => e.PhotoUrl).OnDelete(DeleteBehavior.Cascade);

        EntityTypeBuilder<ExpertFees> expertFees = modelBuilder.Entity<ExpertFees>();
        expertFees.HasOne(e => e.Expert).WithOne(e => e.Fees).OnDelete(DeleteBehavior.Cascade);

        // CALL MODEL
        EntityTypeBuilder<Call> call = modelBuilder.Entity<Call>();
        call.HasOne(e => e.Expert).WithMany(e => e.Calls).OnDelete(DeleteBehavior.Cascade);
        call.HasOne(e => e.User).WithMany(e => e.Calls).OnDelete(DeleteBehavior.Cascade);

        EntityTypeBuilder<CallDetails> callDetails = modelBuilder.Entity<CallDetails>();
        callDetails.HasOne(e => e.Call).WithOne(e => e.CallDetails).OnDelete(DeleteBehavior.Cascade);
        
        // GROUP CALL MODEL
        EntityTypeBuilder<GroupCall> groupCall = modelBuilder.Entity<GroupCall>();
        groupCall.HasOne(e => e.Expert).WithMany(e => e.GroupCalls).OnDelete(DeleteBehavior.Cascade);

        EntityTypeBuilder<GroupCallDetails> groupCallDetails = modelBuilder.Entity<GroupCallDetails>();
        groupCallDetails.HasOne(e => e.GroupCall).WithOne(e => e.GroupCallDetails).OnDelete(DeleteBehavior.Cascade);

        EntityTypeBuilder<GroupCallRegistration> groupCallRegistration = modelBuilder.Entity<GroupCallRegistration>();
        groupCallRegistration.HasOne(e => e.GroupCall).WithMany(e => e.GroupCallRegistrations);
        groupCallRegistration.HasOne(e => e.User).WithMany(e => e.GroupCallRegistrations)
            .OnDelete(DeleteBehavior.Cascade);
    }

    // EXPERT DATA
    public DbSet<Expert> Expert { get; set; }
    public DbSet<ExpertPhotoUrl> ExpertPhotoUrl { get; set; }
    public DbSet<ExpertFees> ExpertFee { get; set; }
    
    // CALL DATA
    public DbSet<Call> Call { get; set; }
    public DbSet<CallDetails> CallDetails { get; set; }
    
    // GROUP CALL DATE
    public DbSet<GroupCall> GroupCall { get; set; }
    public DbSet<GroupCallDetails> GroupCallDetails { get; set; }
    public DbSet<GroupCallRegistration> GroupCallRegistration { get; set; }
    
    // USER DATA
    public DbSet<User> User { get; set; }
    public DbSet<UserPhotoUrl> UserPhotoUrl { get; set; }
}