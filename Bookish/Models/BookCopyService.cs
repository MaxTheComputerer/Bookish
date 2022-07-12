using Microsoft.EntityFrameworkCore;

namespace Bookish.Models;

public class BookCopyService
{
    public static void InsertBookCopy(BookModel book)
    {
        using var context = new LibraryContext();
        var copy = new BookCopyModel
        {
            Book = book
        };
        context.BookCopies.Add(copy);
        context.SaveChanges();
    }
    
    public static List<BookCopyModel> GetCopies(BookModel book)
    {
        using var context = new LibraryContext();
        return context.BookCopies
            .Where(c => c.Book == book)
            .Include(c => c.Book)
            .Include(c => c.Borrower)
            .ToList();
    }

    public static void CheckOutCopy(int copyId, MemberModel borrower, DateTime dueDate)
    {
        using var context = new LibraryContext();
        var copy = context.BookCopies.First(c => c.Id == copyId);
        copy.Borrower = borrower;
        copy.DueDate = dueDate;
        context.SaveChanges();
    }
    
    public static void CheckInCopy(int copyId)
    {
        using var context = new LibraryContext();
        var copy = context.BookCopies.First(c => c.Id == copyId);
        copy.Borrower = null;
        copy.DueDate = new DateTime();
        context.SaveChanges();
    }
}