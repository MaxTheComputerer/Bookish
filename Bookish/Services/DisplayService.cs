namespace Bookish.Models;

public class DisplayService
{
    public static string FormatPropertyName(string name)
    {
        return name.Replace('_', ' ');
    }

    public static string GetInputType(string name)
    {
        return name switch
        {
            "DoB" => "date",
            "Email" => "email",
            "Mobile" => "tel",
            _ => "text"
        };
    }
    
    public static string FormatPropertyValue(string name, string value)
    {
        if (name == "DoB" && value != "")
        {
            return DateTime.Parse(value).ToShortDateString();
        }
        else
        {
            return value;
        }
    }

}