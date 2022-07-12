using Microsoft.EntityFrameworkCore;

namespace Bookish.Models;

public class LibraryContext : DbContext
{
    public DbSet<BookModel> Books { get; set; }
    public DbSet<BookCopyModel> BookCopies { get; set; }

    public DbSet<MemberModel> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=bookishLibrary;Trusted_Connection=True;");
    }
}
