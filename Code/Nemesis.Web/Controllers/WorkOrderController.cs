using Nemesis.DAL;
using Nemesis.Domain;
using Nemesis.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Controllers
{
    public class WorkOrderController : Controller
    {
        // GET: WorkOrder

        [HttpGet]
        public ActionResult CreateWorkOrder()
        {
            WorkOrderViewModel wovm = new WorkOrderViewModel();
            wovm.StartDate = DateTime.Now;
            wovm.EstimatedEndDate = DateTime.Now;

            wovm.Clients = GetClients<Client>();
            return View(wovm);
        }

        private IEnumerable<SelectListItem> GetClients<T>()
            where T : Client
        {
            IEnumerable<Client> clients = null;

            using (var repo = new GenericRepository<Client>(new NemesisContext()))
            {
                clients = repo.Get();
            }

            return new SelectList(clients, "Id", "Name");
        }

        [HttpPost]
        public ActionResult CreateWorkOrder(WorkOrderViewModel wovm)
        {
            WorkOrder wo = new WorkOrder();
            return CreateWorkOrder<WorkOrder>(wo, wovm);
        }

        private ActionResult CreateWorkOrder<T>(WorkOrder wo, WorkOrderViewModel wovm)
            where T : WorkOrder
        {
            if (ModelState.IsValid)
            {
                using (var repo = new GenericRepository<WorkOrder>(new NemesisContext()))
                {
                    wo.Name = wovm.Name;
                    wo.Description = wovm.Description;
                    wo.StartDate = wovm.StartDate;
                    wo.EstimatedEndDate = wovm.EstimatedEndDate;

                    Client client = new Client();

                    using (var clRepo = new GenericRepository<Client>(new NemesisContext()))
                    {
                        client = clRepo.GetByID(wovm.ClientId);
                    }

                    wo.Client = client;

                    repo.Insert(wo);
                    repo.Save();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                wovm.Clients = GetClients<Client>();
                return View("CreateWorkOrder", wovm);
            }
        }
    }
}