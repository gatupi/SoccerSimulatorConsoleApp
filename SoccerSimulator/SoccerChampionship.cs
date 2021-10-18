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

            index = index < 0 ? 0 : (index >= _teams.Count ? _teams.Count - 1 : index);

            return _teams[index];
        }

        public SoccerTeam GetTeam(string name) {

            return _teams.Find(t => t.Name == name);
        }
    }
}
