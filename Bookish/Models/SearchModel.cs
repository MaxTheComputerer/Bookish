﻿using System.Reflection;

namespace Bookish.Models;

public class SearchModel
{
    // Tests if <searchParameter> is contained in <property> of <book> 
    private static bool BookHasMatchingProperty(BookModel book, PropertyInfo property, string searchParameter)
    {
        var searchTerms = searchParameter.Split(' ');
        var bookPropertyValue = property.GetValue(book);
        if (bookPropertyValue == null)
        {
            return false;
        }

        var bookPropertyWords = ((string)bookPropertyValue).ToLower().Split();

        return searchTerms.All(word => bookPropertyWords.Contains(word.ToLower()));
    }
    
    public static List<BookModel> SearchForBook(BookModel searchParameters)
    {
        using var context = new LibraryContext();
        var books = context.Books.ToList();

        // Iterate over all possible properties of a book, except its Id
        foreach (var property in typeof(BookModel).GetProperties())
        {
            var parameter = property.GetValue(searchParameters);
            if (property.Name != "Id" && parameter != null)
            {
                var parameterString = (string) parameter;
                // Filter to only books which match the user's search
                books = books.Where(book => BookHasMatchingProperty(book, property, parameterString)).ToList();
            }
        }

        return books;
    }

    public static bool IsFormBlank(BookModel searchParameters)
    {
        foreach (var property in typeof(BookModel).GetProperties())
        {
            var searchValue = property.GetValue(searchParameters);
            if (property.Name != "Id" && searchValue != null)
            {
                return false;
            }
        }

        return true;
    }
}