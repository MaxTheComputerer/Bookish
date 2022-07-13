using System.Reflection;

namespace Bookish.Models;

public class SearchBooksService
{
    // Tests if <searchParameter> is contained in <property> of <book> 
    private static bool BookHasMatchingProperty(BookModel book, PropertyInfo property, string searchParameter)
    {
        var searchTerms = searchParameter.Split(' ');
        var bookPropertyValue = property.GetValue(book);
        if (bookPropertyValue == null)
        {
            return false;
        }
        var bookPropertyWords = ((string)bookPropertyValue).ToLower().Split();
        return searchTerms.All(word => bookPropertyWords.Contains(word.ToLower()));
    }
    
    public static List<BookModel> SearchForBook(BookModel searchParameters, string orderBy = "Author")
    {
        var orderByProperty = typeof(BookModel).GetProperty(orderBy);
        using var context = new LibraryContext();
        var books = context.Books.ToList();

        foreach (var property in typeof(BookModel).GetProperties())
        {
            var parameter = property.GetValue(searchParameters);
            if (property.Name != "Id" && parameter != null)
            {
                var parameterString = (string) parameter;
                books = books.Where(book => BookHasMatchingProperty(book, property, parameterString)).ToList();
            }
        }

        return books.OrderBy(b => orderByProperty.GetValue(b)).ToList();
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