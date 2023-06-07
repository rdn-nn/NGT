using NGT.Application;
using NGT.Data;
using NGT.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace NGT.Areas.Admin.Controllers
{
    public class OSItemController : AdminController
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Index()
        {
            List<int> listaIds = TempData["listaIds"] as List<int>;
            int OSId = listaIds.Last();

            ViewBag.Itens = db.OSItem.Include(x => x.OrdServico).Include(x => x.ItemDesc).ToList();

            foreach (var item in ViewBag.Itens)
            {
                if(item.OrdServicoId == OSId)
                {
                    OSItem osi = db.OSItem.Find(item.Id);
                    db.OSItem.Remove(osi);  
                }
            }
            db.SaveChanges();

            if (listaIds != null)
            {
                
                for (int i = 0; i <= listaIds.Count - 2; i++)
                {
                    OSItem osi = new OSItem
                    {
                        OrdServicoId = OSId,
                        ItemDescId = listaIds[i],
                    };
                    db.OSItem.Add(osi);
                    
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index", "OS");
        }
    }
}