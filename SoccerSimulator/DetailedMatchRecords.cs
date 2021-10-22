using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class DetailedMatchRecords : IMatchRecords {

        public DetailedMatchRecords() {
            AsHome = new MatchRecords();
            AsAway = new MatchRecords();
        }

        public MatchRecords AsHome { get; private set; }

        public MatchRecords AsAway { get; private set; }

        public int Points => AsHome.Points + AsAway.Points;

        public int Played => AsHome.Played + AsAway.Played;

        public int Won => AsHome.Won + AsAway.Won;

        public int Drawn => AsHome.Drawn + AsAway.Drawn;

        public int Lost => AsHome.Lost + AsAway.Lost;

        public int GoalsFor => AsHome.GoalsFor + AsAway.GoalsFor;

        public int GoalsAgainst => AsHome.GoalsAgainst + AsAway.GoalsAgainst;

        public int GoalDifference => AsHome.GoalDifference + AsAway.GoalDifference;

        public int[] TableOrder => new int[] { Played, Won, Drawn, Lost, GoalsFor, GoalsAgainst, GoalDifference, Points };

        public string Csv => $"{Played},{Won},{Drawn},{Lost},{GoalsFor},{GoalsAgainst},{GoalDifference},{Points}";
    }
}
