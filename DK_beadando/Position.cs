using System;
using System.Collections.Generic;

namespace DK_beadando
{
    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    public enum RobotState
    {
        Idle,
        Moving,
        Pickup,
        Drop,
        Charging,
        Maintenance
    }
    public class Robot
    {
        public int id { get; set; }
        public Position position { get; set; }
        public string status { get; set; }
        public RobotState state { get; set; }
    }

    public class Truck
    {
        public int id { get; set; }
        public Position position { get; set; }
        public string status { get; set; }
    }

    public class Shelf
    {
        public int id { get; set; }
        public Position position { get; set; }
    }

    public class Matrix
    {
        public int width { get; set; }
        public int height { get; set; }
    }
    public class Member
    {
        public int id { get; set; }
        public Position position { get; set; }
    }

    public class MazeData
    {
        public Matrix matrix { get; set; }
        public List<Robot> robots { get; set; }
        public List<Truck> trucks { get; set; }
        public List<Shelf> shelves { get; set; }
        public Member member { get; set; }
    }
}
