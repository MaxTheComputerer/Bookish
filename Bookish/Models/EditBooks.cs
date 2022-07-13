namespace Bookish.Models;

public class EditBooks
{
    public static void InsertBooks(BookModel newbook)
    {
        using var context = new LibraryContext();
        var book = new BookModel();
        foreach (var property in typeof(BookModel).GetProperties())
        {
            if (property.Name != "Id")
            {
                property.SetValue(book, property.GetValue(newbook));
            }
        }
        context.Books.Add(book);
        context.SaveChanges();
    }

    public static void AlterBooks(int id, BookModel replaceBook)
    {
        using var context = new LibraryContext();
        var originalBook = context.Books.Find(id);

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

