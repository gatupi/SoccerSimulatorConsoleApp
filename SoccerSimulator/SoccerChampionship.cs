using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class SoccerChampionship {

        private readonly List<int> _standings;

        public SoccerChampionship(params SoccerTeam[] teams) {
            if (teams == null)
                throw new ArgumentNullException($"Parameter '{nameof(teams)}' cannot be null!");

            if (teams.Length < 2)
                throw new SoccerException("A championship must have at least two teams!");

            Teams = new List<SoccerTeam>(teams);

            foreach (SoccerTeam t in Teams)
                t.AddChampionshipRecord(this);
        }

        public SoccerChampionship(int numberOfTeams) {

            if (numberOfTeams < 2)
                throw new SoccerException("A championship must have at least two teams!");

            Teams = new List<SoccerTeam>();
            _standings = new List<int>();
            for (int i = 0; i < numberOfTeams; i++) {
                Teams.Add(new SoccerTeam());
                _standings.Add(i);
                Teams[i].AddChampionshipRecord(this);
            }
        }

        public List<SoccerTeam> Teams { get; private set; }

        public int NumberOfTeams => Teams.Count;

        public string TableString() {

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
            for (int i = 0; i < Teams.Count; i++) {
                r = Teams[_standings[i]];
                attr = r.Records.TableOrder;
                table += r.Name.PadRight(20) + " | ";
                for (int j = 0; j < attr.Length; j++) {
                    table += attr[j].ToString().PadLeft(7);
                    table += j < attr.Length - 1 ? " | " : "\n";
                }
            }

            return table;
        }

        public void Sort() {
            _standings.Sort((a, b) => Teams[a].CompareTo(Teams[b]));
        }

        public SoccerTeam GetTeam(int index) {

            index = index < 0 ? 0 : (index >= Teams.Count ? Teams.Count - 1 : index);

            return Teams[index];
        }

        public SoccerTeam GetTeam(string name) {

            return Teams.Find(t => t.Name == name);
        }

        public bool HasRegistered(SoccerTeam team) {
            return Teams.Find(t => t == team) != null;
        }
    }
}
