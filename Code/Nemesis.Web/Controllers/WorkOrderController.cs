﻿using Nemesis.DAL;
using Nemesis.Domain;
using Nemesis.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Controllers
{
    public class WorkOrderController : Controller
    {
        // GET: WorkOrder
        public ActionResult Index()
        {
            return GetWorkOrders<WorkOrder>();
        }

        private ActionResult GetWorkOrders<T>() where T : WorkOrder
        {
            using (var repo = new GenericRepository<T>(new NemesisContext()))
            {
                IList<WorkOrder> workOrders = repo.Get(null, null, "Client, AssignedToTeams").ToList<WorkOrder>();
                return View(workOrders);
            }

        }


        [HttpGet]
        public ActionResult CreateWorkOrder()
        {
            WorkOrderViewModel wovm = new WorkOrderViewModel();
            wovm.StartDate = DateTime.Now;
            wovm.EstimatedEndDate = DateTime.Now;

            wovm.Clients = GetClients<Client>();
            wovm.Teams = GetTeams<Team>();
            return View(wovm);
        }

        private MultiSelectList GetTeams<T>()
            where T : Team
        {
            IEnumerable<Team> team = new List<Team>();

            using (var repo = new GenericRepository<Team>(new NemesisContext()))
            {
                team = repo.Get();
            }

            return new MultiSelectList(team, "Id", "Display");
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
                var ctx = new NemesisContext();
                using (var repo = new GenericRepository<WorkOrder>(ctx))
                {
                    wo.Name = wovm.Name;
                    wo.Description = wovm.Description;
                    wo.StartDate = wovm.StartDate;
                    wo.EstimatedEndDate = wovm.EstimatedEndDate;

                    if (wovm.Document != null)
                    {
                        byte[] uploadedFile = new byte[wovm.Document.InputStream.Length];
                        wovm.Document.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                        wo.Document = uploadedFile;
                    }


                    Client client = new Client();

                    using (var clRepo = new GenericRepository<Client>(ctx))
                    {
                        using (var teamRepo = new GenericRepository<Team>(ctx))
                        {
                            client = clRepo.GetByID(wovm.ClientId);

                            wo.Client = client;

                            List<Team> teams = new List<Team>();

                            if (wovm.IdTeams != null)
                            {
                                foreach (int id in wovm.IdTeams)
                                {
                                    Team team = teamRepo.GetByID(id);
                                    teams.Add(team);
                                }
                            }
                            wo.AssignedToTeams = teams;

                            repo.Insert(wo);
                            repo.Save();
                        }
                    }


                }
                return RedirectToAction("Index", "WorkOrder");
            }
            else
            {
                wovm.Clients = GetClients<Client>();
                wovm.Teams = GetTeams<Team>();
                return View("CreateWorkOrder", wovm);
            }
        }

        public ActionResult DownloadFile(int id)
        {
            using (var repo = new GenericRepository<WorkOrder>(new NemesisContext()))
            {
                WorkOrder workOrder = repo.GetByID(id);
                byte[] file = workOrder.Document;
                return File(file, "application/pdf");
            }
        }
    }
}