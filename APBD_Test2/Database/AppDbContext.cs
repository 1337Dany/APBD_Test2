using Microsoft.EntityFrameworkCore;
using System.Reflection;
using APBD_Test2.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<PublishingHouse> PublishingHouses { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        ConfigurePublishingHouse(modelBuilder);
        ConfigureBook(modelBuilder);
        ConfigureAuthor(modelBuilder);
        ConfigureGenre(modelBuilder);
        ConfigureBookAuthor(modelBuilder);
        ConfigureBookGenre(modelBuilder);
    }
    
    private void ConfigurePublishingHouse(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublishingHouse>(entity =>
        {
            entity.HasKey(ph => ph.IdPublishingHouse);
            
            entity.Property(ph => ph.Name)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(ph => ph.Country)
                .HasMaxLength(50);
                
            entity.Property(ph => ph.City)
                .HasMaxLength(50);
                
            entity.HasMany(ph => ph.Books)
                .WithOne(b => b.PublishingHouse)
                .HasForeignKey(b => b.IdPublishingHouse)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
    private void ConfigureBook(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.IdBook);
            
            entity.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(b => b.ReleaseDate)
                .HasColumnType("datetime");
                
            entity.HasOne(b => b.PublishingHouse)
                .WithMany(ph => ph.Books)
                .HasForeignKey(b => b.IdPublishingHouse)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(b => b.BookAuthors)
                .WithOne(ba => ba.Book)
                .HasForeignKey(ba => ba.IdBook)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(b => b.BookGenres)
                .WithOne(bg => bg.Book)
                .HasForeignKey(bg => bg.IdBook)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
    private void ConfigureAuthor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.IdAuthor);
            
            entity.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.HasMany(a => a.BookAuthors)
                .WithOne(ba => ba.Author)
                .HasForeignKey(ba => ba.IdAuthor)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
    private void ConfigureGenre(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(g => g.IdGenre);
            
            entity.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.HasMany(g => g.BookGenres)
                .WithOne(bg => bg.Genre)
                .HasForeignKey(bg => bg.IdGenre)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
    private void ConfigureBookAuthor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(ba => new { ba.IdBook, ba.IdAuthor });
            
            entity.HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.IdBook);
                
            entity.HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.IdAuthor);
        });
    }
    
    private void ConfigureBookGenre(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookGenre>(entity =>
        {
            entity.HasKey(bg => new { bg.IdBook, bg.IdGenre });
            
            entity.HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.IdBook);
                
            entity.HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.IdGenre);
        });
    }
}