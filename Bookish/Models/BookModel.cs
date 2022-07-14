﻿namespace Bookish.Models;

public class BookModel
{
    public int Id { get; set; }
    public string? Author { get; set; }
    public string? Title { get; set; }
    public int Publication_Year { get; set; }
    public string? ISBN { get; set; }
    public string? Genre { get; set; }

}