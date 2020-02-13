using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agregator_linków.Attribute;
using agregator_linków.Data;
using agregator_linków.Repository;
using agregator_linków.Viewmodel;
using ComponetRegister.Model;
using ComponetRegister.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace agregator_linków.Controllers
{
    public class LinkController : Controller
    {

        private MainRepository repository;
        private IRepRegistercs repRegisters;
        public LinkController(Dbcontext dbcontext, IRepRegistercs repRegisters)
        {
            this.repository = new MainRepository(dbcontext);
            this.repRegisters = repRegisters;
        }


        public IActionResult Index(int page=1)
        {
            string message = TempData["data"] as string;
            ViewData["Register"] = message;
            page = repository.link.CheckPage(page);
            ViewBag.maxPage = repository.link.GetMaxValuePage();
            ViewBag.Page = page;
            if (User.Identity.IsAuthenticated)
            {
               List<ViewIndexLink> model;
                int? id = repository.user.GetID(User.Identity.Name);
                if(id!=null)
                model = repository.link.MapViewIndexLink(id.Value, page);
                else
                model = repository.link.MapViewIndexLink(page);
                return View(model);
            }
            else
            {
                var model = repository.link.MapViewIndexLink(page);
                return View(model);
            }
        }

        [AuthorizeUser]
        public IActionResult AddLink()
        {
            
            return View();
        }

        [AuthorizeUser]
        [HttpPost]
        public IActionResult AddLink(ViewAddLink viewLink)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    viewLink.like = 0;
                    viewLink.time = DateTime.Now;
                    viewLink.user = repository.user.GetUser(User.Identity.Name);
                    repository.link.AddLink(viewLink);
                    TempData["data"] = "Successfully Add";
                    Register register = new Register("addLink " + viewLink.url, viewLink.time.ToString());
                    repRegisters.Add(register);
                    return RedirectToAction("Index", "User");
                }
            }

            return View(viewLink);
        }
        [AuthorizeUser]
        public IActionResult EditLink(int id)
        {
            var model = repository.link.GetViewLink(id);
           
            return View(model);
        }
        [HttpPost]
        [AuthorizeUser]
        public IActionResult EditLink(ViewEditLink viewLink)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {                
                    repository.link.EditLink(viewLink);
                    TempData["data"] = "Successfully Edited";
                    Register register = new Register("EditLink " + viewLink.url, DateTime.Now.ToString());
                    repRegisters.Add(register);
                    return RedirectToAction("Index","User");
                }
            }

            return View(viewLink);
        }


        [AuthorizeUser]
        public IActionResult Remove(int id)
        {

            repository.link.RemoveLink(id);
            TempData["data"] = "Successfully Remove";
            Register register = new Register("RemoveLink by id = "+id , DateTime.Now.ToString());
            repRegisters.Add(register);
            return RedirectToAction("Index", "User");
        }


        [AuthorizeUser]
        public IActionResult Like(int linkId)
        {
            bool checkOption;
                int? id = repository.user.GetID(User.Identity.Name);
                if (id != null)
                checkOption=repository.link.Like(linkId, id.Value);
                    
                else
                    throw new ArgumentException("No parameter found");

            return Json(checkOption);
        }

    }
}