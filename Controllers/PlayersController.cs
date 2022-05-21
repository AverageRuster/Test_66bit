using Test_66bit.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Test_66bit.Controllers
{
    public class PlayersController : Controller
    {
        private ApplicationContext db;
        public PlayersController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult AllPlayers()
        {
            var players = db.Players;
            return View(players);
        }

        [HttpGet]
        public IActionResult AddOrEditPlayer(int? id, bool add)
        {
            SelectList teams = new SelectList(db.Teams, "Id", "Name");
            ViewBag.Teams = teams;

            if (add)
            {
                ViewBag.Title = "Добавление";
            }
            else
            {
                ViewBag.Title = "Редактирование";

                var player = db.Players.Find(id);
                return View(player);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddOrEditPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }

            if (!db.Players.Contains(player))
            {
                db.Players.Add(player);
            }
            else
            {
                db.Players.Update(player);
            }

            db.SaveChanges();

            return RedirectToAction("AllPlayers");
        }

        public IActionResult RemovePlayer(int id)
        {
            var player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();

            return RedirectToAction("AllPlayers");
        }
    }
}
