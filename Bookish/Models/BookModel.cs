namespace Bookish.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }

    public BookModel(string author, string title)
    {
        Author = author;
        Title = title;
    }

    public static void InsertBooks()
    {
        using (var context = new LibraryContext())
        {
            context.Books.Add(new BookModel("Max", "Musical micro-timing for live coding"));
            context.Books.Add(new BookModel("Claire", "The wave breaking"));
            context.SaveChanges();
        }
    }
}