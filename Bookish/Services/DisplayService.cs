namespace Bookish.Models;

public class DisplayService
{
    public static string FormatPropertyName(string name)
    {
        return name.Replace('_', ' ');
    }
}