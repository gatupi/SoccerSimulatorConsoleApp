using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class SoccerException : ApplicationException {

        public SoccerException() : base() { }
        public SoccerException(string message) : base(message) { }
    }
}
