using System;

namespace SoccerSimulator {
    public class SoccerTeam { // resolver o caso de poder jogar uma partida qualquer de um time que está em um campeonato

        private string _name;
        private const string _defaultName = "Soccer Team";
        private static readonly string[] _tableField = {
            "Playing as", "Played", "Won", "Drawn", "Lost", "GF", "GA", "GD", "Points"
        };
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

        public override string ToString() {
            return
                $"{Name}\n" + tableFields() + recordsString(AsHome) + recordsString(AsAway) + recordsString(Total);
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

        private string tableFields() {

            string row = string.Empty;
            int padding;
            int length = _tableField.Length;

            for (int i = 0; i < length; i++) {
                padding = i > 0 ? 7 : 20;
                row += _tableField[i].PadRight(padding);
                row += i < length - 1 ? " | " : "\n";
            }
            for (int i = 0; i < length; i++) {
                padding = i > 0 ? 7 : 20;
                row += string.Empty.PadRight(padding, '-');
                row += i < length - 1 ? "-+-" : "\n";
            }

            return row;
        }


        private static string nextName() {
            return $"{_defaultName} {_count + 1}";
        }
    }
}
