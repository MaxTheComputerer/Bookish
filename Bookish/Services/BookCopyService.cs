﻿using Microsoft.EntityFrameworkCore;

namespace Bookish.Models;

public class BookCopyService
{
    static readonly LibraryContext context = new LibraryContext();
    
    public static BookCopyModel GetCopyFromId(int copyId)
    {
        var copy = context.BookCopies
            .Where(c => c.Id == copyId)
            .Include(c => c.Book)
            .Include(c => c.Borrower)
            .SingleOrDefault();
        return copy;
    }
    
    public static void InsertBookCopy(int bookId)
    {
        var book = context.Books.Find(bookId);
        var copy = new BookCopyModel
        {
            Book = book
        };
        context.BookCopies.Add(copy);
        context.SaveChanges();
    }
    
    public static void DeleteCopy(int copyId)
    {
        context.BookCopies.Remove(new BookCopyModel() { Id = copyId });
        context.SaveChanges();
    }
    
    public static BookCopyResult GetCopies(BookModel book)
    {
        book = context.Books.Find(book.Id);
        var copies =  context.BookCopies
            .Where(c => c.Book == book)
            .Include(c => c.Book)
            .Include(c => c.Borrower)
            .ToList();
        
        return new BookCopyResult()
        {
            book = book,
            copies = copies
        };
    }

    public static void CheckOutCopy(int copyId, MemberModel borrower, DateTime dueDate)
    {
        var copy = context.BookCopies.Find(copyId);
        copy.Borrower = borrower;
        copy.DueDate = dueDate;
        context.SaveChanges();
    }
    
    public static void RenewCopy(int copyId)
    {
        var copy = context.BookCopies.Find(copyId);
        copy.DueDate = DateTime.Today.AddDays(7);
        context.SaveChanges();
    }
    
    public static void CheckInCopy(int copyId)
    {
        var copy = context.BookCopies
            .Where(c => c.Id == copyId)
            .Include(c => c.Borrower)
            .FirstOrDefault();
        copy.Borrower = null;
        copy.DueDate = null;
        context.SaveChanges();
    }

    public static BookModel GetBookFromCopy(int copyId)
    {
        var copy = context.BookCopies
            .Where(c => c.Id == copyId)
            .Include(c => c.Book)
            .FirstOrDefault();
        return copy.Book;
    }
}