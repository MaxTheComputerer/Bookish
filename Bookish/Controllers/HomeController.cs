using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ViewCopies(BookModel book)
    {
        if (book == null || book.Id == null)
        {
            return RedirectToAction(nameof(Catalogue));
        }
        var result = BookCopyService.GetCopies(book);
        return View(result);
    }
    
    [HttpPost]
    public ActionResult AddCopy(BookModel book)
    {
        BookCopyService.InsertBookCopy(book.Id);
        return RedirectToAction(nameof(ViewCopies), new { Id = book.Id });
    }
    
    [HttpPost]
    public ActionResult DeleteCopy(BookCopyModel copy)
    {
        var book = BookCopyService.GetBookFromCopy(copy.Id);
        BookCopyService.DeleteCopy(copy.Id);
        return RedirectToAction(nameof(ViewCopies), new { Id = book.Id });
    }
    
    [HttpPost]
    public ActionResult CheckInCopy(BookCopyModel copy)
    {
        BookCopyService.CheckInCopy(copy.Id);
        var book = BookCopyService.GetBookFromCopy(copy.Id);
        return RedirectToAction(nameof(ViewCopies), new { Id = book.Id });
    }
    
    [HttpPost]
    public ActionResult CheckOutCopy(CheckOutCopyModel checkOutModel)
    {
        using var context = new LibraryContext();
        var member = context.Members.Find(checkOutModel.BorrowerId);

        BookCopyService.CheckOutCopy(checkOutModel.Id, member, checkOutModel.DueDate);
        var book = BookCopyService.GetBookFromCopy(checkOutModel.Id);
        return RedirectToAction(nameof(ViewCopies), new { Id = book.Id });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}