using System;

namespace SoccerSimulator {
    class Program {
        static void Main(string[] args) {
            try {
                SoccerChampionship c = new SoccerChampionship(2);

                SoccerMatch.Play(c.GetTeam(0), c.GetTeam(1), 2, 1);
                Console.WriteLine(c.GetTeam(0) + "\n" + c.GetTeam(1));
                Console.WriteLine(c.GetTeam("Soccer Team 10") == null);
                Console.WriteLine(c.GetTeam(3));

                Console.WriteLine(c.Table());
            }
            catch(SoccerException e) {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Teste!!");
        }
    }
}
