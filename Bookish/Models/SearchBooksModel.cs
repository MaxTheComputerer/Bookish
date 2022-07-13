namespace Bookish.Models;

public class SearchBooksModel
{
    public BookModel searchParameters { get; set; }
    public string orderBy { get; set; }
}