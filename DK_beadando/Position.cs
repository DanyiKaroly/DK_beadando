﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DK_beadando
{
    public class Position
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    public enum Robotstate
    {
        idle,
        moving,
        pickup,
        drop,
        charging,
        maintenance
    }
    public enum RobotStatus
    {
        active,
        inactive,
        error
    }
    public enum chargingpadstatus
    {
        Busy,
        Free,
        Error
    }
    public enum ItemsStatus
    {
        AVAILABLE,
        RESERVED,
        UNAVALIABLE,
        FOUND,
        DAMAGED
    }
    public class Robot
    {
        public int id { get; set; }
        public Position position { get; set; }
        public RobotStatus status { get; set; }
        public Robotstate RobotState { get; set; }
        public int BatteryLevel { get; set; }
        public Task task { get; set; }
        public List<Shelf> targetShelves { get; set; } = new List<Shelf>();
        public List<Point> Path = new List<Point>();
        public int WaitingTime { get; set; } = 0; // új mező


    }
    public class Task
    {
        public int TruckId { get; set; }
        public List<Targy> Items { get; set; } = new List<Targy>();
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
        public List<Targy> Targyak { get; set; } = new List<Targy>();

    }
    public class Chargingpad
    {
        public int id { get; set; }
        public Position position { get; set; }
        public chargingpadstatus status { get; set; }

    }

    public class Targy
    {
        public int TargyId { get; set; }
        public int Mennyiseg { get; set; }
        public ItemsStatus Status { get; set; }
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
        public List<Chargingpad> chargingpads { get; set; }
        public List<Robot> robots { get; set; }
        public List<Truck> trucks { get; set; }
        public List<Shelf> shelves { get; set; }
        public Member member { get; set; }
        public List<Task> tasks { get; set; } = new List<Task>();
    }
}
