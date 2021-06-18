using Amakazor.Cellular;
using System;
using System.Collections.Generic;

namespace GOLGlider
{
    class Program
    {
        static void Main(string[] args)
        {
            CellState alive = new CellState("Alive", 1, '#');
            CellState dead = new CellState("Dead", 0, '.');

            Rule underpopulation = new Rule(dead, new List<CellState> { alive }, 0, 1);
            Rule overpopulation = new Rule(dead, new List<CellState> { alive }, 4, 8);
            Rule reproduction = new Rule(alive, new List<CellState> { alive }, 3, 3);

            dead.AddRule(reproduction);
            alive.AddRules(new List<Rule> { underpopulation, overpopulation });

            Dictionary<Point, CellState> initialState = new Dictionary<Point, CellState>
            {
                { new Point(-1, -1), dead }, { new Point(0, -1), dead }, { new Point(1, -1), alive },
                { new Point(-1, 0), alive }, { new Point(0, 0), dead }, { new Point(1, 0), alive },
                { new Point(-1, 1), dead }, { new Point(0, 1), alive }, { new Point(1, 1), alive },
            };

            CellularAutomata gameOfLife = new CellularAutomata(initialState, dead);

            Console.Write(gameOfLife.ToString());

            for (int i = 0; i < 200; i++)
            {
                gameOfLife.Iterate(1);
                Console.Write("\n\n" + gameOfLife.ToString());
            }
        }
    }
}
