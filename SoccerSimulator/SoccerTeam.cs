using System;
using System.Text;
using SoccerSimulator.Utilities;

namespace SoccerSimulator {
    class SoccerTeam : IComparable { // resolver o caso de poder jogar uma partida qualquer de um time que está em um campeonato

        private string _name;

        private const string _defaultName = "Soccer Team";
        private static int _count = 0;

        public SoccerTeam() : this(NextName()) { }

        public SoccerTeam(string name) {
            Name = name;
            Record = new DetailedMatchRecords();
            _count++;
        }

        public string Name {
            get => _name;
            set {
                _name = ValidateSoccerTeam.Name(value) ? value : NextName();
            }
        }
        public DetailedMatchRecords Record { get; private set; }

        public int[] TieBreakerList {
            get => new int[] { Record.Points, Record.Won, Record.GoalDifference, Record.GoalsFor };
        }

        public int CompareTo(object team) {

            SoccerTeam _team = team as SoccerTeam;
            int[] tbThis = TieBreakerList;
            int[] tbTeam = _team.TieBreakerList;
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

        public override string ToString() {

            string tableField = "Playing as,Played,Won,Drawn,Lost,GF,GA,GD,Points";
            string width = "20,7,7,7,7,7,7,7,7";
            string line = ConsoleDataGrid.DivisionLine(width, true);
            StringBuilder table = new StringBuilder($"Records of {Name}\n");

            table.Append(
                line).Append(
                ConsoleDataGrid.GridRow(tableField, width, true)).Append(
                line).Append(
                ConsoleDataGrid.GridRow("Home team," + Record.AsHome.Csv, width, true)).Append(
                ConsoleDataGrid.GridRow("Away team," + Record.AsAway.Csv, width, true)).Append(
                ConsoleDataGrid.GridRow("Total," + Record.Csv, width, true)).Append(
                line);

            return $"{table}";
        }

        private static string NextName() {
            return $"{_defaultName} {_count + 1}";
        }
    }
}
