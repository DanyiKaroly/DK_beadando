using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK_beadando
{
    public class AStarPathfinder
    {
        public List<Point> FindPath(Point start, Point goal, bool[,] maze)
        {
            var openSet = new PriorityQueue<Point, int>();
            var cameFrom = new Dictionary<Point, Point>();
            var gScore = new Dictionary<Point, int>();
            var fScore = new Dictionary<Point, int>();

            openSet.Enqueue(start, 0);
            gScore[start] = 0;
            fScore[start] = Heuristic(start, goal);

            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { -1, 0, 1, 0 };

            while (openSet.Count > 0)
            {
                Point current = openSet.Dequeue();

                if (current == goal)
                {
                    return ReconstructPath(cameFrom, current);
                }

                for (int i = 0; i < 4; i++)
                {
                    Point neighbor = new Point(current.X + dx[i], current.Y + dy[i]);

                    if (neighbor.X < 0 || neighbor.Y < 0 || neighbor.X >= maze.GetLength(0) || neighbor.Y >= maze.GetLength(1))
                        continue;

                    if (!maze[neighbor.X, neighbor.Y]) // false = fal
                        continue;

                    int tentative_gScore = gScore[current] + 1;

                    if (!gScore.ContainsKey(neighbor) || tentative_gScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentative_gScore;
                        fScore[neighbor] = tentative_gScore + Heuristic(neighbor, goal);

                        if (!openSet.UnorderedItems.Any(x => x.Element == neighbor))
                        {
                            openSet.Enqueue(neighbor, fScore[neighbor]);
                        }
                    }
                }
            }

            return new List<Point>(); // nincs út
        }

        private int Heuristic(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y); // Manhattan distance
        }

        private List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            var totalPath = new List<Point> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Insert(0, current);
            }
            return totalPath;
        }
    }
}