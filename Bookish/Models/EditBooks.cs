namespace Bookish.Models;

public class EditBooks
{
    public static void InsertBooks(string author, string title)
    {
        using var context = new LibraryContext();
        var book = new BookModel();
        book.Author = author;
        book.Title = title;
        context.Books.Add(book);
        context.SaveChanges();
    }
}