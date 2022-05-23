using Test_66bit.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var players = db.Players.Include(p => p.Team);
            return View(players);
        }

        [HttpGet]
        public IActionResult AddOrEditPlayer(int? id, bool add, int? rowId)
        {
            ViewBag.Teams = db.Teams;
            ViewBag.Add = add;
            ViewBag.RowId = rowId;

            if (!add)
            {
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

        public IActionResult AddTeam(string name)
        {
            name = name.Trim();
            Team team = new();
            team.Name = name;
            int? teamId = null;
            bool old = db.Teams.Any(t => t.Name == name);

            if (old)
            {
                teamId = db.Teams.FirstOrDefault(t => t.Name == name).Id;
            }
            else
            {
                db.Teams.Add(team);
                db.SaveChanges();
                teamId = db.Teams.FirstOrDefault(t => t == team).Id;
            }

            return Json(new { id = teamId, oldTeam = old });
        }
    }
}
