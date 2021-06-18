using System.Collections.Generic;

namespace Amakazor.Cellular
{
    internal class Cell
    {
        internal CellState CurrentState { get; private set; }
        internal CellState NextState { private get; set; }
        internal bool IsActive { get; set; }

        internal Cell(CellState currentState)
        {
            CurrentState = currentState;
            NextState = null;
            IsActive = true;
        }

        internal void Tick(IEnumerable< CellState> neigbors)
        {
            Rule testResult = CurrentState.Test(neigbors);

            if (testResult != null) NextState = testResult.CellStateAfter;
        }

        internal void Apply()
        {
            if (NextState != null && NextState != CurrentState)
            {
                CurrentState = NextState;
                NextState = null;
                IsActive = true;
            } 
            else
            {
                IsActive = false;
            }
        }

        public override string ToString()
        {
            return CurrentState.ToString();
        }
    }
}