namespace Bookish.Models;

public class BookCopyModel
{
    public int Id { get; set; }
    public BookModel Book { get; set; }
    public MemberModel? Borrower { get; set; }
    public DateTime DueDate { get; set; }

    public bool IsAvailable()
    {
        return Borrower == null;
    }

    public bool IsOverdue()
    {
        return DueDate.Date < DateTime.Today.Date;
    }
}

public class BookCopyResult
{
    public BookModel book { get; set; }
    public List<BookCopyModel> copies { get; set; }
}