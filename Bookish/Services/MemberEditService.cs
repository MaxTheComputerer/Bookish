using Microsoft.EntityFrameworkCore;

namespace Bookish.Models;

public class MemberEditService
{
    static readonly LibraryContext context = new LibraryContext();
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

    public static MemberModel nullConvert(MemberModel member)
    {
        foreach (var property in typeof(MemberModel).GetProperties())
        {
            if (property.GetValue(member) is null)
            {
                property.SetValue(member, " ");
            }
        }
        return member;
    }
    
    public static void InsertMember(MemberModel newMember)
    {
        newMember = nullConvert(newMember);
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
        originalMember = nullConvert(originalMember);
        context.SaveChanges();
    }
    
    public static void DeleteMember(int id)
    {
        var lostMember = GetMemberFromId(id);
        context.Members.Remove(lostMember);
        context.SaveChanges();
    }
    
    public static List<BookCopyModel> GetLoans(int memberId)
    {
        var context = new LibraryContext();
        var loans = context.BookCopies
            .Where(c => c.Borrower.Id == memberId)
            .Include(c => c.Book)
            .ToList();

        return loans;
    }
}