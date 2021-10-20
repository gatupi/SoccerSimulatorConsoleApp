using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class MatchRecords : IMatchRecords {

        public static void SoccerMatch(SoccerTeam home, SoccerTeam away, int homeGoals, int awayGoals) {

            if (home != null && away != null) {

                if (home == away)
                    throw new SoccerException("A team cannot play against itself!");

                if (homeGoals < 0 || awayGoals < 0)
                    throw new SoccerException("A team cannot score negative goals!");

                home.Record.AsHome.GoalsFor += homeGoals;
                home.Record.AsHome.GoalsAgainst += awayGoals;
                away.Record.AsAway.GoalsFor += awayGoals;
                away.Record.AsAway.GoalsAgainst += homeGoals;

                if (homeGoals > awayGoals) {
                    home.Record.AsHome.Won++;
                    away.Record.AsAway.Lost++;
                }
                else if (awayGoals > homeGoals) {
                    home.Record.AsHome.Lost++;
                    away.Record.AsAway.Won++;
                }
                else {
                    home.Record.AsHome.Drawn++;
                    away.Record.AsAway.Drawn++;
                }
            }
        }

        public int Points => Won * 3 + Drawn;

        public int Played => Won + Drawn + Lost;

        public int Won { get; private set; }

        public int Drawn { get; private set; }

        public int Lost { get; private set; }

        public int GoalsFor { get; private set; }

        public int GoalsAgainst { get; private set; }

        public int GoalDifference => GoalsFor - GoalsAgainst;

        public int[] TableOrder => new int[] { Played, Won, Drawn, Lost, GoalsFor, GoalsAgainst, GoalDifference, Points };

        public string Csv => $"{Played},{Won},{Drawn},{Lost},{GoalsFor},{GoalsAgainst},{GoalDifference},{Points}";

    }
}