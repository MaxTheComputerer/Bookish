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
    
    public static void DeleteBooks(int id)
    {
        Console.WriteLine(id);
        using var context = new LibraryContext();
        var lostBook = context.Books.First(b => b.Id == id);
        //var lostBook = SearchModel.SearchForBook(new BookModel() { Author = "Virginia Woolf" }).First();
        context.Books.Remove(lostBook);
        context.SaveChanges();
    }
}

