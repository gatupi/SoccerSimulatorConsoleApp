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

        public virtual int HomeGoals { get; }

        public virtual int AwayGoals { get; }

        public virtual SoccerTeam Winner => HomeGoals == AwayGoals ? null : (HomeGoals > AwayGoals ? Home : Away);

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

        public static string SumOfScoreboards(string scoreboard_1, string scoreboard_2) {

            bool vsb_1 = ValidateScoreboard(scoreboard_1);
            bool vsb_2 = ValidateScoreboard(scoreboard_2);

            if (!vsb_1 && !vsb_2)
                return null;

            if (vsb_1 && scoreboard_2 == null)
                return scoreboard_1;

            if (vsb_2 && scoreboard_1 == null)
                return scoreboard_2;

            if (vsb_1 && vsb_2) {
                string[] goals_1 = scoreboard_1.Split('-');
                string[] goals_2 = scoreboard_2.Split('-');
                int homeGoals = int.Parse(goals_1[0]) + int.Parse(goals_2[0]);
                int awayGoals = int.Parse(goals_1[1]) + int.Parse(goals_2[1]);

                return $"{homeGoals}-{awayGoals}";
            }

            return null;
        }

    }

    class NoOvertimePlayoff : SoccerMatch {

        public NoOvertimePlayoff(SoccerChampionship championship, SoccerTeam home, SoccerTeam away, string regularTime, string penalties = null) :
            base(championship, home, away, regularTime) {

            if (penalties != null) {
                if (HasWinner)
                    throw new SoccerException("A play-off cannot have a penalty decision if there is a winner at the regular time!");

                else if (!ValidateScoreboard(penalties))
                    throw new FormatException("Invalid format for penalties!");

                else {
                    string[] penaltiesResult = penalties.Split('-');
                    HomeGoalsAtPenalties = int.Parse(penaltiesResult[0]);
                    AwayGoalsAtPenalties = int.Parse(penaltiesResult[1]);

                    if (HomeGoalsAtPenalties == AwayGoalsAtPenalties)
                        throw new SoccerException("A play-off that extends to a penalty decision cannot have a penalty scoreboard resulting in a draw!");
                }
            }
            else if (!HasWinner)
                throw new SoccerException("A play-off whose regular time and overtime end in a draw must have a penalty decision!");
        }

        public virtual int HomeGoalsAtRegularTime => HomeGoals;

        public virtual int AwayGoalsAtRegularTime => AwayGoals;

        public int? HomeGoalsAtPenalties { get; }

        public int? AwayGoalsAtPenalties { get; }

        public override SoccerTeam Winner {
            get {
                SoccerTeam winner = base.Winner;
                if (winner == null)
                    return HomeGoalsAtPenalties > AwayGoalsAtPenalties ? Home : Away;
                return winner;
            }
        }
    }

    class Playoff : NoOvertimePlayoff {

        public Playoff(SoccerChampionship championship, SoccerTeam home, SoccerTeam away, string regularTime) :
            this(championship, home, away, regularTime, null) {
        
        }

        public Playoff(SoccerChampionship championship, SoccerTeam home, SoccerTeam away, string regularTime, string overtime, string penalties = null)
            : base(championship, home, away, SumOfScoreboards(regularTime, overtime), penalties) {

            if (overtime != null) {
                string[] overtimeResult = overtime.Split('-');
                HomeGoalsAtOvertime = int.Parse(overtimeResult[0]);
                AwayGoalsAtOvertime = int.Parse(overtimeResult[1]);
            }
        }

        public override int HomeGoalsAtRegularTime => HomeGoals - HomeGoalsAtOvertime ?? HomeGoals;

        public override int AwayGoalsAtRegularTime => AwayGoals - AwayGoalsAtOvertime ?? AwayGoals;

        public int? HomeGoalsAtOvertime { get; }

        public int? AwayGoalsAtOvertime { get; }
    }
}
