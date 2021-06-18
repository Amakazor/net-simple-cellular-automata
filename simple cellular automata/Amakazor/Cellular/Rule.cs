using System.Collections.Generic;

namespace Amakazor.Cellular
{
    public class Rule
    {
        internal CellState CellStateAfter { get; private set; }
        internal HashSet<CellState> CellStatesToCount { get; private set; }
        internal int MinimalValue { get; private set; }
        internal int MaximalValue { get; private set; }

        public Rule(CellState cellStateAfter, IEnumerable<CellState> cellStatesToCount, int minimalValue, int maximalValue)
        {
            CellStateAfter = cellStateAfter;
            CellStatesToCount = new HashSet<CellState>(cellStatesToCount);
            MinimalValue = minimalValue;
            MaximalValue = maximalValue;
        }

        internal bool Test(IEnumerable<CellState> neigbors)
        {
            int value = 0;

            foreach (CellState cellState in neigbors)
            {
                if (CellStatesToCount.Contains(cellState)) value += cellState.Value;
            }

            return value >= MinimalValue && value <= MaximalValue;
        }
    }
}