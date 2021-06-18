using Amakazor.Cellular;
using System;
using System.Collections.Generic;

namespace GOLGosperGliderGun
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

            Dictionary<Point, CellState> initialState = new Dictionary<Point, CellState>();

            string gun =
                "000000000000000000000000100000000000\n" +
                "000000000000000000000010100000000000\n" +
                "000000000000110000001100000000000011\n" +
                "000000000001000100001100000000000011\n" +
                "110000000010000010001100000000000000\n" +
                "110000000010001011000010100000000000\n" +
                "000000000010000010000000100000000000\n" +
                "000000000001000100000000000000000000\n" +
                "000000000000110000000000000000000000\n";

            int x = 36;
            int y = 9;

            var lines = gun.Split('\n');

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    initialState.Add(new Point(i, j), lines[j][i] == '0' ? dead : alive);
                }
            }

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
