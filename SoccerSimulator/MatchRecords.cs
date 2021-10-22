using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class MatchRecords : IMatchRecords {

        SoccerChampionship _championship;

        public MatchRecords() { }

        public MatchRecords(SoccerChampionship championship) {
            _championship = championship;
        }

        public static void SoccerMatch(SoccerTeam home, SoccerTeam away, int homeGoals, int awayGoals) {

            if (home != null && away != null) {

                if (home == away)
                    throw new SoccerException("A team cannot play against itself!");

                if (homeGoals < 0 || awayGoals < 0)
                    throw new SoccerException("A team cannot score negative goals!");

                home.Records.AsHome.GoalsFor += homeGoals;
                home.Records.AsHome.GoalsAgainst += awayGoals;
                away.Records.AsAway.GoalsFor += awayGoals;
                away.Records.AsAway.GoalsAgainst += homeGoals;

                if (homeGoals > awayGoals) {
                    home.Records.AsHome.Won++;
                    away.Records.AsAway.Lost++;
                }
                else if (awayGoals > homeGoals) {
                    home.Records.AsHome.Lost++;
                    away.Records.AsAway.Won++;
                }
                else {
                    home.Records.AsHome.Drawn++;
                    away.Records.AsAway.Drawn++;
                }
            }
        }

        public SoccerChampionship Championship => _championship;

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

        public static MatchRecords operator +(MatchRecords a) => a;

        public static MatchRecords operator +(MatchRecords a, MatchRecords b) =>
            new MatchRecords {
                Won = a.Played + b.Played,
                Drawn = a.Drawn + b.Drawn,
                Lost = a.Lost + b.Lost,
                GoalsFor = a.GoalsFor + b.GoalsFor,
                GoalsAgainst = a.GoalsAgainst + b.GoalsAgainst
            };
    }
}