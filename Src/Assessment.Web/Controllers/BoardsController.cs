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

        [HttpPost]
        public bool Add(Board board)
        {
            return boards.Add(board);  
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return boards.Delete(GetBoard(id));
        }

        [HttpPost("{id}/post-its")]
        public bool Add(int id, [FromBody]PostIt postIt)
        {
            GetBoard(id).PostIts.Add(postIt);

            return true;
        }

        [HttpGet("{id}/post-its")]
        public ICollection<PostIt> GetPins(int id)
        {
            var board = this.boards.Find(id);
            if (board == null) throw new ArgumentException(nameof(id), "Board ID not found.");

            return board.PostIts;
        }


        [HttpDelete("{id}/post-its/{postItId}")]
        public bool DeletePin(int id, int postItId)
        {
            var postIts = GetBoard(id).PostIts;

            if (postIts != null)
            {
                var pin = postIts.FirstOrDefault(postIt => postIt.Id == postItId);
                if (pin != null)
                {
                    return postIts.Remove(pin);
                }
                else
                {
                    throw new ArgumentException(nameof(postItId), "postIt ID not found.");
                }
            }
            else
            {
                throw new ArgumentException(nameof(id), "Board has no postIts.");
            }
        }

        [HttpDelete("{id}")]
        public bool DeleteBoard(int id)
        {
            var board = GetBoard(id);
            var postIts = board.PostIts;

            if (postIts != null)
            {
                postIts.Clear();  
            }

            return boards.Delete(board);
        }

        private Board GetBoard(int id)
        {
            var board = this.boards.Find(id);
            if (board == null) throw new ArgumentException(nameof(id), "Board ID not found.");
            return board;
        }
    }
}
