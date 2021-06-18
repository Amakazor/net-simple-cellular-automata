using System;
using System.Collections.Generic;
using System.Linq;

namespace Amakazor.Cellular
{
    public class CellState : IEquatable<CellState>
    {
        public string Name { get; private set; }
        public char Symbol { get; }
        public int Value { get; private set; }

        internal HashSet<Rule> Rules { get; private set; }

        public CellState(string name, int value, char symbol)
        {
            Name = name;
            Value = value;
            Symbol = symbol;
            Rules = new HashSet<Rule>();
        }

        public CellState(string name, int value, char symbol, IEnumerable<Rule> rules)
        {
            Name = name;
            Value = value;
            Symbol = symbol;
            Rules = new HashSet<Rule>(rules);
        }

        public void AddRule(Rule rule)
        {
            Rules.Add(rule);
        }

        public void AddRules(IEnumerable<Rule> rules)
        {
            Rules.UnionWith(rules);
        }

        internal Rule Test(IEnumerable<CellState> neigbors)
        {
            foreach (Rule rule in Rules) if (rule.Test(neigbors)) return rule;
            return null;
        }

        public override int GetHashCode()
        {
            int hash = 7649;
            hash = (hash * 7559) + Name.GetHashCode();
            hash = (hash * 7559) + Rules.GetHashCode();
            return hash;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CellState);
        }

        public bool Equals(CellState other)
        {
            return other != null &&
                   Name == other.Name &&
                   EqualityComparer<HashSet<Rule>>.Default.Equals(Rules, other.Rules);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}