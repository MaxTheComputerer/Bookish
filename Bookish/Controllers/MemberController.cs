using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Bookish.Controllers;

public class MemberController : Controller
{
    [HttpGet]
    public IActionResult MemberList()
    {
        var members = MemberEditService.GetMemberList();
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
        if (MemberSearchService.IsFormBlank(newMember))
        {
            return View();
        }
        else
        {
            MemberEditService.InsertMember(newMember);
            return View(newMember);
        }
    }
    
    public ActionResult EditMember(int id)
    {
        MemberModel updateMember = MemberEditService.GetMemberFromId(id);
        return View(updateMember);
    }
    
    [HttpPost]
    public ActionResult EditMember(MemberModel replaceMember)
    {
        MemberEditService.AlterMember(replaceMember.Id, replaceMember);
        return RedirectToAction(nameof(MemberList));
    }
    
    [HttpPost]
    public ActionResult DeleteMember(MemberModel lostMember)
    {
        MemberEditService.DeleteMember(lostMember.Id);
        return RedirectToAction(nameof(MemberList));
    }

    [HttpGet]
    public IActionResult SearchMembers()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult SearchMembers(MemberModel search)
    {
        if (MemberSearchService.IsFormBlank(search))
        {
            return View(search);
        }
        return RedirectToAction(nameof(SearchMembersResults), search);
    }
    
    [HttpPost]
    public IActionResult SearchMembersResults(MemberModel search)
    {
        var results = MemberSearchService.SearchForMember(search);
        return View(results);
    }
}