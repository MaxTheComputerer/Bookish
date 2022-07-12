using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

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

    [HttpPost]
    public IActionResult Index(BookModel search)
    {
        if (SearchModel.IsFormBlank(search))
        {
            return View(search);
        }

        return RedirectToAction(nameof(Search), search);
    }
    
    public IActionResult Search(BookModel search)
    {
        var results = SearchModel.SearchForBook(search);
        return View(results);
    }
    
    public IActionResult Catalogue()
    {
        using var context = new LibraryContext();
        var books = context.Books.ToList();
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
        EditBooks.InsertBooks(newBook.Author, newBook.Title);
        return View(newBook);
    }

    //[HttpPost]
    public IActionResult GetCopies()
    {
        //temp
        var book = SearchModel.SearchForBook(new BookModel() { Author = "George" }).First();
            
        var copies = BookCopyService.GetCopies(book);
        var result = new BookCopyResult()
        {
            book = book,
            copies = copies
        };
        return View(result);
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