using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public class BookController : Controller
{
    [HttpGet]
    public IActionResult Catalogue(string orderBy = "Author")
    {
        var searchModel = new SearchModel<BookModel>()
        {
            orderBy = orderBy,
            pageTitle = "Catalogue"
        };
        return RedirectToAction(nameof(SearchBooksResults), searchModel);
    }

    [HttpGet]
    public IActionResult AddBook()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult AddBook(BookModel newBook)
    {
        BookEditService.InsertBook(newBook);
        return View(newBook);
    }
    
    [HttpGet]
    public ActionResult EditBook(int id)
    {
        BookModel updateBook = BookEditService.GetBookFromId(id);
        return View(updateBook);
    }
    
    [HttpPost]
    public ActionResult EditBook(BookModel replaceBook)
    {
        BookEditService.AlterBook(replaceBook.Id, replaceBook);
        return RedirectToAction(nameof(Catalogue));
    }
    
    [HttpPost]
    public ActionResult DeleteBook(BookModel lostBook)
    {
        BookEditService.DeleteBook(lostBook.Id);
        return RedirectToAction(nameof(Catalogue));
    }

    [HttpGet]
    public IActionResult SearchBooks()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SearchBooksResults(SearchModel<BookModel> search)
    {
        ViewData["searchParams"] = search;
        ViewData["Title"] = search.pageTitle;
        if (search.searchParameters == null)
        {
            search.searchParameters = new BookModel();
        }

        if (search.orderBy != null)
        {
            return View(BookSearch.Search(search.searchParameters, search.orderBy));
        }
        else
        {
            return View(BookSearch.Search(search.searchParameters));
        }
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
    public ActionResult AddMultipleCopies(AddMultipleCopiesModel parameters)
    {
        for (int i = 0; i < parameters.numberOfCopies; i++)
        {
            BookCopyService.InsertBookCopy(parameters.bookId);
        }
        return RedirectToAction(nameof(ViewCopies), new { Id = parameters.bookId });
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
}