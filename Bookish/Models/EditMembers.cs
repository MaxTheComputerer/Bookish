namespace Bookish.Models;

public class EditMembers
{
    public static void InsertMembers(string name)
    {
        using var context = new LibraryContext();
        var member = new MemberModel
        {
            Name = name
        };
        context.Members.Add(member);
        context.SaveChanges();
    }

    public static void AlterMembers(int id, MemberModel replaceMember)
    {
        using var context = new LibraryContext();
        var originalMember = context.Members.Find(id);
        originalMember.Name = replaceMember.Name;

        foreach (var property in typeof(MemberModel).GetProperties())
        {
            if (property.Name != "Id")
            {
                property.SetValue(originalMember, property.GetValue(replaceMember));
            }
        }
        context.SaveChanges();
    }
    
    public static void DeleteMembers(int id)
    {
        using var context = new LibraryContext();
        var lostMember = context.Members.Find(id);
        context.Members.Remove(lostMember);
        context.SaveChanges();
    }
}