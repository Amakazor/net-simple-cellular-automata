using System;
using System.Collections.Generic;
using System.Linq;

namespace Amakazor.Cellular
{
    public class CellularAutomata
    {
        private Dictionary<Point, Cell> Cells { get; set; }
        private Point TopLeft { get; set; }
        private Point BottomRight { get; set; }
        private CellState DefaultState { get; }

        public CellularAutomata(IDictionary<Point, CellState> initialStates, CellState defaultState)
        {
            DefaultState = defaultState;

            Cells = new Dictionary<Point, Cell>();
            foreach (KeyValuePair<Point, CellState> keyValuePair in initialStates)
            {
                Cells.Add(keyValuePair.Key, new Cell(keyValuePair.Value));
            }

            SetStartingCoordinates();
        }

        public void Iterate(long iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Iterate();
            }
        }

        public override string ToString()
        {
            string returnString = "";

            for (long x = TopLeft.X; x <= BottomRight.X; x++)
            {
                for (long y = TopLeft.Y; y <= BottomRight.Y; y++)
                {
                    Point coordinates = new Point(x, y);
                    returnString += Cells.ContainsKey(coordinates) ? Cells[coordinates].CurrentState.Symbol : DefaultState.Symbol;
                }
                returnString += '\n';
            }

            return returnString;
        }

        private void Iterate()
        {
            ExpandSides(GetSidesToExpand());

            for (long x = TopLeft.X; x <= BottomRight.X; x++)
            {
                for (long y = TopLeft.Y; y <= BottomRight.Y; y++)
                {
                    Point coordinates = new Point(x, y);
                    if (!Cells.ContainsKey(coordinates)) Cells.Add(coordinates, new Cell(DefaultState));
                    Cells[coordinates].Tick(GetNeigbors(coordinates));
                }
            }

            for (long x = TopLeft.X; x <= BottomRight.X; x++)
            {
                for (long y = TopLeft.Y; y <= BottomRight.Y; y++)
                {
                    Cells[new Point(x, y)].Apply();
                }
            }
        }

        private Sides GetSidesToExpand()
        {
            Sides sides = new Sides();

            for (long i = TopLeft.X; i <= BottomRight.X; i++)
            {
                if (sides.Top && sides.Bottom) break;

                if (!sides.Top && !Cells[new Point(i, TopLeft.Y)].CurrentState.Equals(DefaultState)) sides.Top = true;
                if (!sides.Bottom && !Cells[new Point(i, BottomRight.Y)].CurrentState.Equals(DefaultState)) sides.Bottom = true;
            }

            for (long i = TopLeft.Y; i <= BottomRight.Y; i++)
            {
                if (sides.Left && sides.Right) break;

                if (!sides.Left && !Cells[new Point(i, TopLeft.X)].CurrentState.Equals(DefaultState)) sides.Left = true;
                if (!sides.Right && !Cells[new Point(i, BottomRight.X)].CurrentState.Equals(DefaultState)) sides.Right = true;
            }

            return sides;
        }

        private ISet<CellState> GetNeigbors(Point coordinates)
        {
            return new HashSet<CellState> (new HashSet<Point>
                {
                    new Point(-1, -1), new Point(0, -1), new Point(1, -1),
                    new Point(-1, 0), new Point(1, 0),
                    new Point(-1, 1), new Point(0, 1), new Point(1, 1)
                }.Select(point => Cells.ContainsKey(coordinates + point) ? Cells[coordinates + point].CurrentState : DefaultState));
        }

        private void ExpandSides(Sides sides)
        {
            if (sides.Top || sides.Left) TopLeft = new Point(TopLeft.X + (sides.Left ? -1 : 0), TopLeft.Y + (sides.Top ? -1 : 0));
            if (sides.Bottom || sides.Right) BottomRight = new Point(BottomRight.X + (sides.Right ? 1 : 0), BottomRight.Y + (sides.Bottom ? 1 : 0));
        }

        private void SetStartingCoordinates()
        {
            TopLeft = new Point(Cells.Keys.Select(key => key.X).Min(), Cells.Keys.Select(key => key.Y).Min());
            BottomRight = new Point(Cells.Keys.Select(key => key.X).Max(), Cells.Keys.Select(key => key.Y).Max());
        }
    }
}