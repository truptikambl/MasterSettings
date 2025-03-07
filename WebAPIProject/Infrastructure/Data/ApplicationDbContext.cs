using Microsoft.EntityFrameworkCore;
using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Department> Department { get; set; }

    public DbSet<Fabricc> Fabric { get; set; }

    public DbSet<DyeStuff> DyeStuff { get; set; }
    public  DbSet<Construction> constructions { get; set; }

    public DbSet<SymbolCategory> SymbolCategory { get; set; }

    public DbSet<IndividualCareSymbol> individualCareSymbols { get; set; }

    public DbSet<Users> Users { get; set; }

    public DbSet<DyeType> DyeType { get; set; }

    public DbSet<DyeingMethod> DyeingMethod { get; set; }

    public DbSet<Notification> Notification { get; set; }

    


    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-M0AELQNJ\\SQLEXPRESS;Database=webapi;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Company__3214EC076CD458BE");

            entity.ToTable("Company");

           
            entity.Property(e => e.CompanyName).HasMaxLength(100);
        });
        OnModelCreatingPartial(modelBuilder);
    }
    

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


