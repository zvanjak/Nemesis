using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nemesis.Domain;
using Nemesis.DAL;

namespace Nemesis.Web.Controllers
{
    public class ClientController : Controller
    {
        
        // GET: /Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Description,IsArchived")] Client client)
        {
            if (ModelState.IsValid)
            {
                using (var repo = new GenericRepository<Client>(new NemesisContext()))
                {
                    Client newClient = new Client();
                    newClient.Name = client.Name;
                    newClient.Description = client.Description;

                    repo.Insert(newClient);
                    repo.Save();
                }
                return RedirectToAction("Index", "Home");
            }

            return View(client);
        }
    }
}
