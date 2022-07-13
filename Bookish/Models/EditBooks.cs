namespace Bookish.Models;

public class EditBooks
{
    static LibraryContext context = new LibraryContext();
    public static List<BookModel> GetBookList()
    {
        var books = context.Books.ToList();
        return books;
    }
    
    public static BookModel GetBookFromId(int id)
    {
        BookModel book = context.Books.Single(b =>b.Id == id);
        return book;
    }
        
    public static void InsertBooks(BookModel newbook)
    {
        context.Books.Add(newbook);
        context.SaveChanges();
    }

    public static void AlterBooks(int id, BookModel replaceBook)
    {
        BookModel originalBook = GetBookFromId(id);
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
        BookModel lostBook = GetBookFromId(id);
        context.Books.Remove(lostBook);
        context.SaveChanges();
    }
}

