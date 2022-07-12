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

    public static void AlterBooks(int id, BookModel replaceBook)
    {
        using var context = new LibraryContext();
        var originalBook = context.Books.Find(id);
        originalBook.Title = replaceBook.Title;
        originalBook.Author = replaceBook.Author;

        foreach (var property in typeof(BookModel).GetProperties())
        {
            if (property.Name != "Id")
            {
                property.SetValue(originalBook, property.GetValue(replaceBook));
            }
        }
        context.SaveChanges();
    }
    
    public static void DeleteBooks(int id)
    {
        using var context = new LibraryContext();
        var lostBook = context.Books.Find(id);
        context.Books.Remove(lostBook);
        context.SaveChanges();
    }
}

