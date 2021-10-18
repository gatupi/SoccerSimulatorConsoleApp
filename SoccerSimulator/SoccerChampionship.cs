using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class SoccerChampionship {

        private List<SoccerTeam> _teams;

        public SoccerChampionship(int numberOfTeams) {

            if (numberOfTeams < 2)
                throw new SoccerException("A championship must have at least two teams!");

            _teams = new List<SoccerTeam>();
            for (int i = 0; i < numberOfTeams; i++)
                _teams.Add(new SoccerTeam());
        }

        public SoccerTeam GetTeam(int index) {

            SoccerTeam team = null;

            try {
                team = _teams[index];
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            return team;
        }

        public SoccerTeam GetTeam(string name) {

            return _teams.Find(t => t.Name == name);
        }
    }
}
