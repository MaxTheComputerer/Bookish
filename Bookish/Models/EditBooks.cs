namespace Bookish.Models;

public class EditBooks
{
    public static void InsertBooks(string author, string title)
    {
        using var context = new LibraryContext();
        var book = new BookModel
        {
            Author = author,
            Title = title
        };
        context.Books.Add(book);
        context.SaveChanges();
    }
}