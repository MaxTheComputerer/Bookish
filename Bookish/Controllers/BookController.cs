using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Bookish.Controllers;

public class BookController : Controller
{
    public IActionResult Catalogue()
    {
        var books = EditBooks.GetBookList();
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
        EditBooks.InsertBooks(newBook);
        return View(newBook);
    }
    [HttpGet]
    public ActionResult EditBook(int id)
    {
        BookModel updateBook = EditBooks.GetBookFromId(id);
        return View(updateBook);
    }
    
    [HttpPost]
    public ActionResult EditBook(BookModel replaceBook)
    {
        EditBooks.AlterBooks(replaceBook.Id, replaceBook);
        return RedirectToAction(nameof(SearchBooksResults));
    }
    
    [HttpPost]
    public ActionResult DeleteBook(BookModel lostBook)
    {
        EditBooks.DeleteBooks(lostBook.Id);
        return View();
    }

    [HttpGet]
    public IActionResult SearchBooks()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SearchBooks(BookModel search)
    {
        if (SearchBooksModel.IsFormBlank(search))
        {
            return View(search);
        }

        return RedirectToAction(nameof(SearchBooksResults), search);
    }
    
    public IActionResult SearchBooksResults(BookModel search)
    {
        var results = SearchBooksModel.SearchForBook(search);
        return View(results);
    }

    //[HttpPost]
    public IActionResult ViewCopies()
    {
        //temp
        var book = SearchBooksModel.SearchForBook(new BookModel() { Author = "George" }).First();
            
        var copies = BookCopyService.GetCopies(book);
        var result = new BookCopyResult()
        {
            book = book,
            copies = copies
        };
        return View(result);
    }
}