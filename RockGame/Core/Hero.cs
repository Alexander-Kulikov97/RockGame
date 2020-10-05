using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace RockGame.Core
{
    public class Hero
    {
        private string[,] _map;

        public BitmapImage imagesHero { get; private set; }

        private List<string> _path;

        public List<string> Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        private int _health = 100;
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }

        public Hero(string[,] map)
        {
            this._map = map;

            var rootFolder = Directory.GetCurrentDirectory();
            rootFolder = rootFolder.Substring(0, rootFolder.IndexOf(@"\RockGame\", StringComparison.Ordinal) + @"\RockGame".Length);
            rootFolder = rootFolder + @"\RockGame\Image\";

            imagesHero = new BitmapImage(new Uri(rootFolder + "hero.png"));
            Path = solve(map);
        }

        private static List<string> solve(string[,] board)
        {
            List<Cell> shortestPathToGoal = new List<Cell>();
            List<string> stringPath = new List<string>();
            for (int y = 0; y < board.GetLength(0); y++)
                for (int x = 0; x < board.GetLength(1); x++)
                    if (board[y, x] == "S"/*Start*/)
                        walk(board, y, x, 0, initVisited(board), new List<Cell>(), shortestPathToGoal, new List<string>(), stringPath, string.Empty);

            return stringPath;
        }



        private static void walk(string[,] board, int y, int x, int keyRing, bool[,] visited, List<Cell> path, List<Cell> shortestPathToGoal, List<string> stringPath,
            List<string> stringshortestPathToGoal, string move)
        {
            if (y < 0 || y >= board.GetLength(0) || x < 0 || x >= board.GetLength(1) || visited[y, x])
                return; 
            char point = board[y, x][0];
            if (point == 'X')
                return; 
            if (point == 'Q')
            {
                shortestPathToGoal.Clear();
                stringshortestPathToGoal.Clear();
                shortestPathToGoal.AddRange(path);
                stringshortestPathToGoal.AddRange(stringPath);
                shortestPathToGoal.Add(new Cell(y, x));
                return; 
            }
            if (shortestPathToGoal.Count > 0 && path.Count + 2 >= shortestPathToGoal.Count)
                return; 
            if (point >= 'A' && point <= 'E')
            { 
                if ((keyRing & (1 << (point - 'A'))) == 0)
                    return; 
            }
            else if (point >= 'a' && point <= 'e')
            { 
                if ((keyRing & (1 << (point - 'a'))) == 0)
                {
                    keyRing |= (1 << (point - 'a')); 
                    visited = initVisited(board); 
                }
            }
            visited[y, x] = true;
            path.Add(new Cell(y, x));
            stringPath.Add(move);
            walk(board, y, x + 1, keyRing, visited, path, shortestPathToGoal, stringPath, stringshortestPathToGoal, "R"); // right
            walk(board, y + 1, x, keyRing, visited, path, shortestPathToGoal, stringPath, stringshortestPathToGoal, "D"); // down
            walk(board, y, x - 1, keyRing, visited, path, shortestPathToGoal, stringPath, stringshortestPathToGoal, "L"); // left
            walk(board, y - 1, x, keyRing, visited, path, shortestPathToGoal, stringPath, stringshortestPathToGoal, "U"); // up
            if (path.Any())
            {
                path.RemoveAt(path.Count - 1);

            }
            if (stringPath.Any())
            {
                stringPath.RemoveAt(stringPath.Count - 1);
            }
            visited[y, x] = false;
        }

        private static bool[,] initVisited(string[,] board)
        {
            bool[,] visited = new bool[board.GetLength(0), board.GetLength(1)];
            for (int x = 0; x < board.GetLength(0); x++)
                for (int y = 0; y < board.GetLength(1); y++)
                    visited[x, y] = false;
            return visited;
        }

        public void SaveToFile(string fileName)
        {
            IEnumerable<string> path = Path;
            File.WriteAllLines(fileName, new string[2] { 
                path.Count().ToString(), string.Join("", path) 
            });
        }
    }
}
