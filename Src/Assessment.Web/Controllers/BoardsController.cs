using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Assessment.Web.Models;
using Newtonsoft.Json;

namespace Assessment.Web.Controllers
{
    [Route("api/[controller]")]
    public class BoardsController : Controller
    {
        public IBoardRepository boards;

        public BoardsController(IBoardRepository boards)
        {
            this.boards = boards;
        }

        [HttpGet]
        public IEnumerable<Board> GetAll()
        {
            return boards.GetAll();
        }

        [HttpGet("{id}")]
        public Board Find(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id), "Board ID must be greater than zero.");

            return boards.Find(id);
        }
        /// <summary>
        /// Create a board
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<Board> CreateBoard(string name)
        {
            return boards.CreateBoard(name);
        }
        /// <summary>
        /// Delete a board
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        [HttpDelete("{boardId}")]
        public IEnumerable<Board> DeleteBoard(int boardId)
        {
            return boards.DeleteBoard(boardId);
        }
        /// <summary>
        /// Add a Pin to a board
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<Board> AddPin(int boardId,string text)
        {
            return boards.AddPin(boardId, text);
        }

        /// <summary>
        /// View Pins attached to a board
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<PostIt> ViewPin(int boardId)
        {
            return boards.ViewPin(boardId);
        }
        /// <summary>
        /// Delete a Pin
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<Board> DeleteaPin(int boardId,int id)
        {
            return boards.DeleteaPin(boardId,id);
        }
        /// <summary>
        /// Delete board and Pin
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<Board> DeleteaBoardPin(int boardId, int id)
        {
            return boards.DeleteBoardPin(boardId, id);
        }
    }
}
