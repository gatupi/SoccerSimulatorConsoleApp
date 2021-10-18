using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class SoccerMatch {

        public static void Play(SoccerChampionship championship, SoccerTeam home, SoccerTeam away, int homeGoals, int awayGoals) {

            if (home != null && away != null) {

                if (home == away)
                    throw new SoccerException("A team cannot play against itself!");

                if (homeGoals < 0 || awayGoals < 0)
                    throw new SoccerException("A team cannot score negative goals!");

                home.AsHome.GoalsFor += homeGoals;
                home.AsHome.GoalsAgainst += awayGoals;
                away.AsAway.GoalsFor += awayGoals;
                away.AsAway.GoalsAgainst += homeGoals;

                if (homeGoals > awayGoals) {
                    home.AsHome.Won++;
                    away.AsAway.Lost++;
                }
                else if (awayGoals > homeGoals) {
                    home.AsHome.Lost++;
                    away.AsAway.Won++;
                }
                else {
                    home.AsHome.Drawn++;
                    away.AsAway.Drawn++;
                }

                updateTeam(home);
                updateTeam(away);
            }           
        }

        private static void updateTeam(SoccerTeam team) {

            SoccerMatch total = team.Total;
            SoccerMatch home = team.AsHome;
            SoccerMatch away = team.AsAway;

            total.Won = home.Won + away.Won;
            total.Drawn = home.Drawn + away.Drawn;
            total.Lost = home.Lost + away.Lost;
            total.GoalsFor = home.GoalsFor + away.GoalsFor;
            total.GoalsAgainst = home.GoalsAgainst + away.GoalsAgainst;
        }

        public int Points { get => Won * 3 + Drawn; }

        public int Played { get => Won + Drawn + Lost; }

        public int Won { get; private set; }

        public int Drawn { get; private set; }

        public int Lost { get; private set; }

        public int GoalsFor { get; private set; }

        public int GoalsAgainst { get; private set; }

        public int GoalDifference { get => GoalsFor - GoalsAgainst; }

    }
}