using CST_350_CLC.Models;
using CST_350_CLC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST_350_CLC.Controllers {
    public class GameController : Controller {
        GameDAO gameDAO = new GameDAO();

        public IActionResult Index() {
            return View(gameDAO.startup());
        }

        public IActionResult ShowUpdatedGrid(int id) {
            return PartialView(gameDAO.updateBoard(id));
        }


        // Old method of processing clicks USE PARTIAL PAGE UPDATES VIA "ShowUpdatedGrid" and JS function.
        public IActionResult CellClick(int id) {
            Console.WriteLine("Id passed to update board function is: " + id);
            return View("Index", gameDAO.updateBoard(id));
        }

        // Save Game Method. Reference the SavesDAO file to execute


        // Show Saved Games Method. Reference the SavesDAO file to execute

    }
}
