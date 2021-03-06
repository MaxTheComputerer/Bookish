namespace Bookish.Models;

public class BookEditService
{
    static readonly LibraryContext context = new LibraryContext();
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
    
    public static void InsertBook(BookModel newBook)
    {
        context.Books.Add(newBook);
        context.SaveChanges();
    }

    public static void AlterBook(int id, BookModel replaceBook)
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
    
    public static void DeleteBook(int id)
    {
        BookModel lostBook = GetBookFromId(id);
        context.Books.Remove(lostBook);
        context.SaveChanges();
    }
}

