using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    interface IMatchRecords {

        public int Points { get; }

        public int Played { get; }

        public int Won { get; }

        public int Drawn { get; }

        public int Lost { get; }

        public int GoalsFor { get; }

        public int GoalsAgainst { get; }

        public int GoalDifference { get; }

    }
}
