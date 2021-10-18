using System;

namespace SoccerSimulator {
    class SoccerTeam : IComparable { // resolver o caso de poder jogar uma partida qualquer de um time que está em um campeonato

        private string _name;
        private const string _defaultName = "Soccer Team";
        private static int _count = 0;

        public SoccerTeam() : this(nextName()) { }

        public SoccerTeam(string name) {
            Name = name;
            AsHome = new SoccerMatch();
            AsAway = new SoccerMatch();
            Total = new SoccerMatch();
            _count++;
        }

        public string Name {
            get => _name;
            set {
                _name = ValidateSoccerTeam.Name(value) ? value : nextName();
            }
        }

        public SoccerMatch AsHome { get; private set; }

        public SoccerMatch AsAway { get; private set; }

        public SoccerMatch Total { get; private set; }

        public int[] TieBreaker {
            get => new int[] { Total.Points, Total.Won, Total.GoalDifference, Total.GoalsFor };
        }

        public override string ToString() {
            return
                $"{Name}\n" + tableHeader() + recordsString(AsHome) + recordsString(AsAway) + recordsString(Total);
        }

        public int CompareTo(object team) {

            SoccerTeam _team = team as SoccerTeam;
            int[] tbThis = TieBreaker;
            int[] tbTeam = _team.TieBreaker;
            int vThis = 0;
            int vTeam = 0;
            int r = 0;

            for (int i = 0; i < tbThis.Length && r == 0; i++) {
                vThis += tbThis[i];
                vTeam += tbTeam[i];
                r = vTeam.CompareTo(vThis);
            }

            return r;
        }

        private string recordsString(SoccerMatch r) {

            string row =
                (r == AsHome ? "Home team" : (r == AsAway ? "Away team" : "Total")).PadRight(20) + " | ";
            int[] attr =
                { r.Played, r.Won, r.Drawn, r.Lost, r.GoalsFor, r.GoalsAgainst, r.GoalDifference, r.Points };

            for (int i = 0; i < attr.Length; i++) {
                row += attr[i].ToString().PadLeft(7);
                row += i < attr.Length - 1 ? " | " : "\n";
            }

            return row;
        }

        private string tableHeader() {

            string[] tableField = {
                "Playing as", "Played", "Won", "Drawn", "Lost", "GF", "GA", "GD", "Points"
            };

            string header = string.Empty;
            int length = tableField.Length;

            for (int i = 0; i < length; i++) {
                header += tableField[i].PadRight(i > 0 ? 7 : 20);
                header += i < length - 1 ? " | " : "\n";
            }
            for (int i = 0; i < length; i++) {
                header += string.Empty.PadRight(i > 0 ? 7 : 20, '-');
                header += i < length - 1 ? "-+-" : "\n";
            }

            return header;
        }


        private static string nextName() {
            return $"{_defaultName} {_count + 1}";
        }
    }
}
