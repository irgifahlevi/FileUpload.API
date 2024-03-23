using System;
using System.Collections.Generic;
using FileUpload.API.Enum;
using FileUpload.API.Models;
using FileUpload.API.Models.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace FileUpload.API.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FileHandling> FileHandlings { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<FileHandling>(entity =>
    //    {
    //        entity.Property(e => e.Id).ValueGeneratedOnAdd();
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = Applications.System;
                    entry.Entity.RowStatus = (short)EnumTypes.Active;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.LastModifiedBy = Applications.System;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            // Handle the specific
            var errorMessage = "Error saving changes: " + ex.Message;
            if (ex.InnerException is SqlException sqlException && sqlException.Number == 8152)
            {
                errorMessage += "\nTruncated value: " + sqlException.Errors[0].Message;
            }
            throw;
        }
    }
}
