using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    static class ValidateSoccerTeam {

        public static bool Name(string name) {
            return name.Length > 1 ? true : false;
        }
    }
}
