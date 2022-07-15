using System.Reflection;

namespace Bookish.Models;

public abstract class DatabaseSearch
{
    // Tests if <searchParameter> is contained in <property> of <model> 
    private static bool ModelHasMatchingProperty<TModel>(TModel model, PropertyInfo property, string searchParameter)
    {
        var searchTerms = searchParameter.Split(' ');
        var modelPropertyValue = property.GetValue(model);
        if (modelPropertyValue == null)
        {
            return false;
        }
        var modelPropertyWords = ((string) modelPropertyValue).ToLower().Split();
        return searchTerms.All(word => modelPropertyWords.Contains(word.ToLower()));
    }

    protected static List<TModel> SearchThroughTable<TModel>(TModel searchParameters, string orderBy, List<TModel> table)
    {
        var orderByProperty = typeof(TModel).GetProperty(orderBy);
        foreach (var property in typeof(TModel).GetProperties())
        {
            var parameter = property.GetValue(searchParameters);
            if (property.Name != "Id" && parameter != null) // will fail for properties with non-null defaults
            {
                var parameterString = (string) parameter;
                table = table.Where(book => ModelHasMatchingProperty(book, property, parameterString)).ToList();
            }
        }

        return table.OrderBy(b => orderByProperty.GetValue(b)).ToList();
    }

    public static bool IsFormBlank<TModel>(TModel searchParameters)
    {
        foreach (var property in typeof(TModel).GetProperties())
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

public class BookSearch : DatabaseSearch
{
    public static List<BookModel> Search(BookModel searchParameters, string orderBy = "Author")
    {
        using var context = new LibraryContext();
        var table = context.Books.ToList();
        return SearchThroughTable<BookModel>(searchParameters, orderBy, table);
    }
}

public class MemberSearch : DatabaseSearch
{
    public static List<MemberModel> Search(MemberModel searchParameters, string orderBy = "Name")
    {
        using var context = new LibraryContext();
        var table = context.Members.ToList();
        return SearchThroughTable<MemberModel>(searchParameters, orderBy, table);
    }
}