using System.Reflection;

namespace Bookish.Models;

public class SearchMembersModel
{
    // Tests if <searchParameter> is contained in <property> of <member> 
    private static bool MemberHasMatchingProperty(MemberModel member, PropertyInfo property, string searchParameter)
    {
        var searchTerms = searchParameter.Split(' ');
        var memberPropertyValue = property.GetValue(member);
        if (memberPropertyValue == null)
        {
            return false;
        }

        var memberPropertyWords = ((string)memberPropertyValue).ToLower().Split();

        return searchTerms.All(word => memberPropertyWords.Contains(word.ToLower()));
    }
    
    public static List<MemberModel> SearchForMember(MemberModel searchParameters)
    {
        using var context = new LibraryContext();
        var members = context.Members.ToList();

        // Iterate over all possible properties of a member, except its Id
        foreach (var property in typeof(MemberModel).GetProperties())
        {
            var parameter = property.GetValue(searchParameters);
            if (property.Name != "Id" && parameter != null)
            {
                var parameterString = (string) parameter;
                // Filter to only members which match the user's search
                members = members.Where(member => MemberHasMatchingProperty(member, property, parameterString)).ToList();
            }
        }

        return members;
    }

    public static bool IsFormBlank(MemberModel searchParameters)
    {
        foreach (var property in typeof(MemberModel).GetProperties())
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