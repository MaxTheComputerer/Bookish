using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Bookish.Controllers;

public class MemberController : Controller
{
    public IActionResult MemberList()
    {
        var members = EditMembers.GetMemberList();
        return View(members);
    }
    
    [HttpGet]
    public IActionResult AddMember()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult AddMember(MemberModel newMember)
    {
        EditMembers.InsertMembers(newMember);
        return View(newMember);
    }
    
    public ActionResult EditMember(int id)
    {
        MemberModel updateMember = EditMembers.GetMemberFromId(id);
        return View(updateMember);
    }
    
    [HttpPost]
    public ActionResult EditMember(MemberModel replaceMember)
    {
        EditMembers.AlterMembers(replaceMember.Id, replaceMember);
        return RedirectToAction(nameof(SearchMembersResults));
    }
    
    [HttpPost]
    public ActionResult DeleteMember(MemberModel lostMember)
    {
        EditMembers.DeleteMembers(lostMember.Id);
        return View();
    }

    [HttpGet]
    public IActionResult SearchMembers()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SearchMembers(MemberModel search)
    {
        if (SearchMembersModel.IsFormBlank(search))
        {
            return View(search);
        }
        return RedirectToAction(nameof(SearchMembersResults), search);
    }
    
    public IActionResult SearchMembersResults(MemberModel search)
    {
        var results = SearchMembersModel.SearchForMember(search);
        return View(results);
    }
}