namespace Bookish.Models;

public class SearchModel<TModel>
{
    public TModel searchParameters { get; set; }
    public string orderBy { get; set; }
    public string pageTitle { get; set; }
}