﻿using System;
using System.Collections.Generic;

namespace MazeSolver
{
    public class MazeNode : IComparable
    {
        public (int X, int Y) Position { get; set; }
        public (int X, int Y) Left => (Position.X - 1, Position.Y);
        public (int X, int Y) Right => (Position.X + 1, Position.Y);
        public (int X, int Y) Up => (Position.X, Position.Y - 1);
        public (int X, int Y) Down => (Position.X, Position.Y + 1);
        public double DistanceFromStart { get; set; }
        public double DistanceFromEnd { get; set; }
        public MazeNode PreviousNode { get; set; }

        public MazeNode((int x, int y) pos)
        {
            Position = pos;
            DistanceFromStart = double.MaxValue;
            DistanceFromEnd = double.MaxValue;
        }

        public MazeNode((int x, int y) pos, MazeNode end)
        {
            DistanceFromStart = double.MaxValue;
            DistanceFromEnd = Math.Abs(GetDistanceFromNode(end));
            Position = pos;
        }

        public double GetDistanceFromNode(MazeNode node)
        {
            return Math.Sqrt(Math.Pow(Position.X - node.Position.X, 2) + Math.Pow(Position.Y - node.Position.Y, 2));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MazeNode))
                return false;
            else
                return Position == (obj as MazeNode).Position;
        }

        public static bool operator ==(MazeNode l, MazeNode r)
        {
            if (l is null && r is null)
                return true;
            else if (l is null ^ r is null)
                return false;
            else
                return l.Equals(r);
        }

        public static bool operator !=(MazeNode l, MazeNode r)
        {
            if (l is null && r is null)
                return false;
            else if (l is null ^ r is null)
                return true;
            else
                return !l.Equals(r);
        }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            MazeNode otherMazeNode = obj as MazeNode;

            if (otherMazeNode != null)
                return (DistanceFromStart + DistanceFromEnd).CompareTo(otherMazeNode.DistanceFromStart + otherMazeNode.DistanceFromEnd);
            else
                throw new ArgumentException("Object is not a MazeNode");
        }

        public override int GetHashCode()
        {
            var hashCode = 2125806639;
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            return hashCode;
        }
    }
}