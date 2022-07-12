namespace Bookish.Models;

public class SearchModel
{
    public static List<BookModel> SearchForBook(BookModel searchParameters)
    {
        using var context = new LibraryContext();
        var books = context.Books.ToList();

        foreach (var property in typeof(BookModel).GetProperties())
        {
            var searchValue = property.GetValue(searchParameters);
            if (property.Name != "Id" && searchValue != null)
            {
                books = books.Where(b => property.GetValue(b).Equals(searchValue)).ToList();
            }
        }

        return books;
    }

    public static bool IsFormBlank(BookModel searchParameters)
    {
        foreach (var property in typeof(BookModel).GetProperties())
        {
            var searchValue = property.GetValue(searchParameters);
            if (property.Name != "Id" && searchValue != null)
            {
                return false;
            }
        }

        return true;
    }
}