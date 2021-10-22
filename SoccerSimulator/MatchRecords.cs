using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class MatchRecords : IMatchRecords {

        SoccerChampionship _championship;

        public MatchRecords() : this(null) { }

        public MatchRecords(SoccerChampionship championship) {
            _championship = championship;
        }

        public static void PlayMatch(SoccerChampionship championship, SoccerTeam home, SoccerTeam away, int homeGoals, int awayGoals) {

            if (home != null && away != null) {

                if (home == away)
                    throw new SoccerException("A team cannot play against itself!");

                if (homeGoals < 0 || awayGoals < 0)
                    throw new SoccerException("A team cannot score negative goals!");

                if (!championship.HasRegistered(home) || !championship.HasRegistered(away))
                    throw new SoccerException("One of the teams is not registered in this championship!");

                MatchRecords homeRecords = home.RecordsFrom(championship).AsHome;
                MatchRecords awayRecords = away.RecordsFrom(championship).AsAway;

                homeRecords.GoalsFor += homeGoals;
                homeRecords.GoalsAgainst += awayGoals;
                awayRecords.GoalsFor += awayGoals;
                awayRecords.GoalsAgainst += homeGoals;

                if (homeGoals > awayGoals) {
                    homeRecords.Won++;
                    awayRecords.Lost++;
                }
                else if (awayGoals > homeGoals) {
                    homeRecords.Lost++;
                    awayRecords.Won++;
                }
                else {
                    homeRecords.Drawn++;
                    awayRecords.Drawn++;
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

        public string Csv => $"{Played},{Won},{Drawn},{Lost},{GoalsFor},{GoalsAgainst},{GoalDifference},{Points}";

        public static MatchRecords operator +(MatchRecords a, MatchRecords b) =>
            new MatchRecords {
                Won = a.Won + b.Won,
                Drawn = a.Drawn + b.Drawn,
                Lost = a.Lost + b.Lost,
                GoalsFor = a.GoalsFor + b.GoalsFor,
                GoalsAgainst = a.GoalsAgainst + b.GoalsAgainst
            };
    }
}