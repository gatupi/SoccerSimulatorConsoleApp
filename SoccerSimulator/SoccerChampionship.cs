using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class SoccerChampionship {

        private List<SoccerTeam> _teams;
        private List<int> _standings;

        public SoccerChampionship(int numberOfTeams) {

            if (numberOfTeams < 2)
                throw new SoccerException("A championship must have at least two teams!");

            _teams = new List<SoccerTeam>();
            _standings = new List<int>();
            for (int i = 0; i < numberOfTeams; i++) {
                _teams.Add(new SoccerTeam());
                _standings.Add(i);
            }                
        }

        public int NumberOfTeams => _teams.Count;

        public string Table() {

            string[] tableField = {
                "Team", "Played", "Won", "Drawn", "Lost", "GF", "GA", "GD", "Points"
            };
            int[] attr;
            string table = string.Empty;
            int length = tableField.Length;
            SoccerTeam r;

            for (int i = 0; i < length; i++) {
                table += tableField[i].PadRight(i > 0 ? 7 : 20);
                table += i < length - 1 ? " | " : "\n";
            }
            for (int i = 0; i < length; i++) {
                table += string.Empty.PadRight(i > 0 ? 7 : 20, '-');
                table += i < length - 1 ? "-+-" : "\n";
            }
            for (int i = 0; i < _teams.Count; i++) {
                r = _teams[_standings[i]];
                attr = r.Record.TableOrder;
                table += r.Name.PadRight(20) + " | ";
                for (int j = 0; j < attr.Length; j++) {
                    table += attr[j].ToString().PadLeft(7);
                    table += j < attr.Length - 1 ? " | " : "\n";
                }
            }

            return table;
        }

        public void Sort() {
            _standings.Sort((x, y) => _teams[x].CompareTo(_teams[y]));
        }

        public SoccerTeam GetTeam(int index) {

            index = index < 0 ? 0 : (index >= _teams.Count ? _teams.Count - 1 : index);

            return _teams[index];
        }

        public SoccerTeam GetTeam(string name) {

            return _teams.Find(t => t.Name == name);
        }
    }
}
