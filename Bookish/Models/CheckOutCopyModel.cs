namespace Bookish.Models;

public class CheckOutCopyModel
{
    public int Id { get; set; }
    public int BorrowerId { get; set; }
    public DateTime DueDate { get; set; }
}