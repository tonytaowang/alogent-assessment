using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assessment.Web.Models
{
    public interface IBoardRepository
    {
        IQueryable<Board> GetAll();
        Board Find(int id);
        bool Add(Board board);
        bool Delete(Board board);
        IQueryable<Board> DeleteBoard(int boardId);
        IQueryable<Board> CreateBoard(string name);
        IQueryable<Board> AddPin(int boardId,string text);
        IEnumerable<PostIt> ViewPin(int boardId);
        IQueryable<Board> DeleteaPin(int boardId, int id);
        IQueryable<Board> DeleteBoardPin(int boardId, int id);
    }

    public class BoardRepository : IBoardRepository
    {
        private List<Board> boards;

        public BoardRepository()
        {
            boards = GetBoardsFromFile();
        }
        /// <summary>
        /// Delete Board and Pin
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Board> DeleteBoardPin(int boardId, int id)
        {
           return DeleteBoard(boardId);
        }
        /// <summary>
        /// Delete a Pin
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Board> DeleteaPin(int boardId, int id)
        {
            try
            {
                var filePath = Application.Configuration["DataFile"];
                if (!Path.IsPathRooted(filePath))
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                var data = File.ReadAllText(filePath);
                var boardDetails = JsonConvert.DeserializeObject<List<Board>>(data);
                if (boardId > 0)
                {
                    var boardwithPinDeleted = boardDetails?.Where(s => s.Id == boardId).FirstOrDefault();
                    boardwithPinDeleted.PostIt.RemoveAll(s => s.Id == id);
                    var jsonstring = JsonConvert.SerializeObject(boardDetails);
                    var output = JsonConvert.SerializeObject(jsonstring);
                    File.WriteAllText(filePath, output);
                }

            }
            catch
            {

            }
            return GetBoardsFromFile().AsQueryable();
        }
        /// <summary>
        /// View Pins attached to a board
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        public IEnumerable<PostIt> ViewPin(int boardId)
        {
            try
            {
                var boarddata = GetBoardsFromFile();
                var boardSelect = boarddata?.Where(s => s.Id == boardId).FirstOrDefault();
                return boardSelect.PostIt;
            }
            catch(Exception ex)
            {

            }
            return null;
        }
        /// <summary>
        /// Add a pin to a board
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public IQueryable<Board> AddPin(int boardId,string text)
        {
            try
            {
                var boarddata = GetBoardsFromFile();
                var boardSelect = boarddata?.Where(s => s.Id == boardId).FirstOrDefault();
                Random r = new Random();
                boardSelect.PostIt.Add(new PostIt() { Id = r.Next(1, 1000), Text = text, CreatedAt = DateTime.Now });
                var filePath = Application.Configuration["DataFile"];
                if (!Path.IsPathRooted(filePath))
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                var output = JsonConvert.SerializeObject(boarddata);
                File.WriteAllText(filePath, output);
                
            }
            catch(Exception ex)
            {
                //Log Exception
            }
            return GetBoardsFromFile().AsQueryable();
        }
        /// <summary>
        /// Delete an exisitng board
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        public IQueryable<Board> DeleteBoard(int boardId)
        {
            try
            {
                var filePath = Application.Configuration["DataFile"];
                if (!Path.IsPathRooted(filePath))
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                var data = File.ReadAllText(filePath);
                var boardDetails = JsonConvert.DeserializeObject<List<Board>>(data);
                if (boardId > 0)
                {
                    var boardToDeleted = boardDetails?.Where(s => s.Id == boardId).FirstOrDefault();
                    var jsonstring=JsonConvert.SerializeObject(data);
                    JObject jo = JObject.Parse(jsonstring);
                    jo.Property(boardToDeleted?.Name).Remove();
                    jsonstring = jo?.ToString();
                    var output = JsonConvert.SerializeObject(jsonstring);
                    File.WriteAllText(filePath, output);
                }
              
            }
            catch (Exception ex)
            {
              //Log
            }
            return GetBoardsFromFile().AsQueryable();
        }
        private List<Board> GetBoardsFromFile()
        {
            var filePath = Application.Configuration["DataFile"];
            if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            var json = System.IO.File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<Board>>(json);
        }

        public IQueryable<Board> GetAll()
        {
            return boards.AsQueryable();
        }
        /// <summary>
        /// Create a board
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<Board> CreateBoard(string name)
        {
            try
            {
                var boarddata = GetBoardsFromFile();
                var id = boarddata.Max(x => x.Id);
                boarddata.Add(new Board() { Id = ++id, Name = name, CreatedAt = DateTime.Now });
                var filePath = Application.Configuration["DataFile"];
                if (!Path.IsPathRooted(filePath))
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                var output = JsonConvert.SerializeObject(boarddata);
                File.WriteAllText(filePath, output);
                
            }
            catch (Exception ex)
            {
             //Log Error 
            }
            return GetBoardsFromFile().AsQueryable();
        }

        public Board Find(int id)
        {
            return boards.FirstOrDefault(x => x.Id == id);
        }

        public bool Add(Board board)
        {
            if (Find(board.Id) != null) return false;

            boards.Add(board);

            return true;
        }

        public bool Delete(Board board)
        {
            if (Find(board.Id) == null) return false;
            return boards.Remove(board);
        }
    }
}
