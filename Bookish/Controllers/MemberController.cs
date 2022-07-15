using Microsoft.AspNetCore.Mvc;
using Bookish.Models;

namespace Bookish.Controllers;

public class MemberController : Controller
{
    [HttpGet]
    public IActionResult MemberList(string orderBy = "Name")
    {
        var searchModel = new SearchModel<MemberModel>()
        {
            orderBy = orderBy,
            pageTitle = "Member List"
        };
        return RedirectToAction(nameof(SearchMembersResults), searchModel);
    }
    
    [HttpGet]
    public IActionResult AddMember()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult AddMember(MemberModel newMember)
    {
        MemberEditService.InsertMember(newMember);
        return View(newMember);
    }
    
    [HttpGet]
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

    [HttpGet]
    public IActionResult SearchMembersResults(SearchModel<MemberModel> search)
    {
        ViewData["searchParams"] = search;
        ViewData["Title"] = search.pageTitle;
        if (search.searchParameters == null)
        {
            search.searchParameters = new MemberModel();
        }
        
        if (search.orderBy != null)
        {
            return View(MemberSearch.Search(search.searchParameters, search.orderBy));
        }
        else
        {
            return View(MemberSearch.Search(search.searchParameters));
        }
    }
    
    [HttpGet]
    public IActionResult ViewLoans(int id)
    {
        if (id == null || MemberEditService.GetMemberFromId(id) == null)
        {
            return RedirectToAction(nameof(MemberList));
        }
        var result = MemberEditService.GetLoans(id);
        return View(result);
    }
}