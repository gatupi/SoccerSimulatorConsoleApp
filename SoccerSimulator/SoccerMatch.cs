using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {

    class SoccerMatch {

        public SoccerMatch(SoccerChampionship championship, SoccerTeam home, SoccerTeam away, string scoreboard) { // scoreboard examples: "7-1", "2-2", "0-1"

            if (championship == null || home == null || away == null)
                throw new ArgumentNullException();

            if (!championship.HasRegistered(home) || !championship.HasRegistered(away))
                throw new SoccerException("One of the teams is not registered in this championship!");

            if (home == away)
                throw new SoccerException("A team cannot play against itself!");

            if (!ValidateScoreboard(scoreboard))
                throw new SoccerException("Invalid format for scoreboard!");

            string[] goals = scoreboard.Split('-');

            Championship = championship;
            Home = home;
            Away = away;
            HomeGoals = int.Parse(goals[0]);
            AwayGoals = int.Parse(goals[1]);
        }

        public SoccerChampionship Championship { get; }

        public SoccerTeam Home { get; }

        public SoccerTeam Away { get; }

        public int HomeGoals { get; }

        public int AwayGoals { get; }

        public SoccerTeam Winner => HomeGoals == AwayGoals ? null : (HomeGoals > AwayGoals ? Home : Away);

        public bool HasWinner => Winner != null;

        public static bool ValidateScoreboard(string scoreboard) {

            if (scoreboard == null)
                return false;

            string[] goals = scoreboard.Split('-');

            if (goals.Length != 2)
                return false;

            int? homeGoals = null;
            int? awayGoals = null;

            try {
                homeGoals = int.Parse(goals[0]);
                awayGoals = int.Parse(goals[1]);
            }
            catch (FormatException) { }

            return
                !(homeGoals == null || awayGoals == null);
        }

    }

    class Playoff {

        public Playoff(SoccerMatch regularTime, string penalties) {

            if (regularTime == null)
                throw new ArgumentNullException();

            RegularTime = regularTime;

            if (penalties != null) {
                if (regularTime.HasWinner)
                    throw new SoccerException("A play-off cannot have a penalty decision if there is a winner at the regular time!");

                else if (!SoccerMatch.ValidateScoreboard(penalties))
                    throw new FormatException("Invalid format for penalties!");
            }

            if (!regularTime.HasWinner)
                throw new SoccerException("A play-off whose regular time ends in a draw must have a penalty decision!");

            Penalties = new SoccerMatch(regularTime.Championship, regularTime.Home, regularTime.Away, penalties);

            if (!Penalties.HasWinner)
                throw new SoccerException("A play-off that extends to a penalty decision cannot have a penalty scoreboard defined as a draw!");
        }

        public SoccerMatch RegularTime { get; }

        public SoccerMatch Penalties { get; }
    }
}
