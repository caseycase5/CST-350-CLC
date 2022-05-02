using CST_350_CLC.Models;
using CST_350_CLC.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CST_350_CLC.Controllers
{
    public class GameController : Controller
    {
        GameDAO gameDAO = new GameDAO();
        SavesDAO savesDAO = new SavesDAO();


        public IActionResult Index()
        {
            return View(gameDAO.startup());
        }

        public IActionResult ShowUpdatedGrid(int id)
        {
            return PartialView(gameDAO.updateBoard(id));
        }


        // Old method of processing clicks USE PARTIAL PAGE UPDATES VIA "ShowUpdatedGrid" and JS function.
        public IActionResult CellClick(int id)
        {
            Console.WriteLine("Id passed to update board function is: " + id);
            return View("Index", gameDAO.updateBoard(id));
        }

        // Save Game Method. Reference the SavesDAO file to execute
        public IActionResult SaveGame(GameDataModel gameDataModel)
        {
            return View("Index", savesDAO.Insert(gameDataModel));
        }


        // Show Saved Games Method. Reference the SavesDAO file to execute
        public IActionResult ShowSavedGames()
        {
            return PartialView("ShowSavedGames", savesDAO.GetGames());
        }

        // Delete
        public IActionResult Delete(int Id)
        {
            return PartialView("ShowSavedGames", savesDAO.GetGameById(Id));
        }
    }
}
