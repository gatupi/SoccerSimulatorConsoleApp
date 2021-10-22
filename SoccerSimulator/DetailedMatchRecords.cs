using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class DetailedMatchRecords {

        SoccerChampionship _championship;

        public DetailedMatchRecords() : this(null) { }

        public DetailedMatchRecords(SoccerChampionship championship) {
            _championship = championship;
            AsHome = new MatchRecords(championship);
            AsAway = new MatchRecords(championship);
        }

        public MatchRecords AsHome { get; private set; }

        public MatchRecords AsAway { get; private set; }

        public MatchRecords Total => AsHome + AsAway;

        public SoccerChampionship Championship => _championship;

        public int[] TableOrder =>
            new int[] { Total.Played, Total.Won, Total.Drawn, Total.Lost, Total.GoalsFor, Total.GoalsAgainst, Total.GoalDifference, Total.Points };

        public string Csv =>
            $"{Total.Played},{Total.Won},{Total.Drawn},{Total.Lost},{Total.GoalsFor},{Total.GoalsAgainst},{Total.GoalDifference},{Total.Points}";

        public static DetailedMatchRecords operator +(DetailedMatchRecords a) => a;

        public static DetailedMatchRecords operator +(DetailedMatchRecords a, DetailedMatchRecords b) =>
            new DetailedMatchRecords {
                AsHome = a.AsHome + b.AsHome,
                AsAway = a.AsAway + b.AsAway
            };
    }
}
