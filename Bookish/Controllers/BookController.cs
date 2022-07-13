using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Bookish.Controllers;

public class BookController : Controller
{
    [HttpGet]
    public IActionResult Catalogue()
    {
        var books = BookEditService.GetBookList();
        return View(books);
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
    
    [HttpPost]
    public IActionResult SearchBooks(BookModel search)
    {
        if (BookSearchService.IsFormBlank(search))
        {
            return View(search);
        }

        return RedirectToAction(nameof(SearchBooksResults), search);
    }
    
    [HttpPost]
    public IActionResult SearchBooksResults(BookModel search)
    {
        var results = BookSearchService.SearchForBook(search);
        return View(results);
    }

    //[HttpPost]
    public IActionResult ViewCopies()
    {
        //temp
        var book = BookSearchService.SearchForBook(new BookModel() { Author = "George" }).First();
            
        var copies = BookCopyService.GetCopies(book);
        var result = new BookCopyResult()
        {
            book = book,
            copies = copies
        };
        return View(result);
    }
}