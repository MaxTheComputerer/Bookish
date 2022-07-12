namespace Bookish.Models;

public class MemberEditService
{
    static LibraryContext context = new LibraryContext();
    public static List<MemberModel> GetMemberList()
    {
        var members = context.Members.ToList();
        return members;
    }
    
    public static MemberModel GetMemberFromId(int id)
    {
        MemberModel member = context.Members.Single(m =>m.Id == id);
        return member;
    }
    
    public static void InsertMember(MemberModel newMember)
    {
        context.Members.Add(newMember);
        context.SaveChanges();
    }

    public static void AlterMember(int id, MemberModel replaceMember)
    {
        var originalMember = GetMemberFromId(id);
        foreach (var property in typeof(MemberModel).GetProperties())
        {
            if (property.Name != "Id")
            {
                property.SetValue(originalMember, property.GetValue(replaceMember));
            }
        }
        context.SaveChanges();
    }
    
    public static void DeleteMember(int id)
    {
        var lostMember = GetMemberFromId(id);
        context.Members.Remove(lostMember);
        context.SaveChanges();
    }
}