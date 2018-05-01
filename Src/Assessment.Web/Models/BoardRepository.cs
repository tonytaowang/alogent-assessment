using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assessment.Web.Models
{
    public interface IBoardRepository
    {
        IQueryable<Board> GetAll();
        Board Find(int id);
        bool Add(Board board);
        bool Delete(Board board);
        IQueryable<Board> AddBoard(string value);
        IQueryable<Board> DeleteBoard(int boardId);
    }

    public class BoardRepository : IBoardRepository
    {
        private List<Board> boards;

        public BoardRepository()
        {
            boards = GetBoardsFromFile();
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
        
        public IQueryable<Board> AddBoard(string value)
        {
            try
            {
                List<Board> data = GetBoardsFromFile();
                int newId = data.Max(x => x.Id);
                Board board = new Board();
                board.Id = ++newId;
                board.Name = value;
                board.CreatedAt = DateTime.Now;
                data.Add(board);
                var filePath = Application.Configuration["DataFile"];
                if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                var output = JsonConvert.SerializeObject(data);
                File.WriteAllText(filePath, output);
                return GetBoardsFromFile().AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Board> DeleteBoard(int boardId)
        {
            try
            {
                var filePath = Application.Configuration["DataFile"];
                if (!Path.IsPathRooted(filePath)) filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                var json = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<List<Board>>(json);
                if (boardId > 0)
                {
                    var boardToDeleted = data.FirstOrDefault(obj => obj.Id == boardId);
                    data.Remove(boardToDeleted);
                    var output = JsonConvert.SerializeObject(data);
                    File.WriteAllText(filePath, output);
                }
                return GetBoardsFromFile().AsQueryable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
