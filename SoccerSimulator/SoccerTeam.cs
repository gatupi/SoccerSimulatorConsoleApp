using System;
using System.Collections.Generic;

namespace SoccerSimulator {
    class SoccerTeam : IComparable { // resolver o caso de poder jogar uma partida qualquer de um time que está em um campeonato

        private string _name;

        private const string _defaultName = "Soccer Team";
        private static int _count = 0;

        public SoccerTeam() : this(nextName()) { }

        public SoccerTeam(string name) {
            Name = name;
            Record = new DetaildMatchRecords();
            _count++;
        }

        public string Name {
            get => _name;
            set {
                _name = ValidateSoccerTeam.Name(value) ? value : nextName();
            }
        }
        public DetaildMatchRecords Record { get; private set; }

        public int[] TieBreaker {
            get => new int[] { Record.Points, Record.Won, Record.GoalDifference, Record.GoalsFor };
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


        private static string nextName() {
            return $"{_defaultName} {_count + 1}";
        }
    }
}
