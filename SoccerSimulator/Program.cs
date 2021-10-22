using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator {
    class Program {
        static void Main(string[] args) {
            try {
                SoccerChampionship c = new SoccerChampionship(6);

                MatchRecords.PlayMatch(c, c.GetTeam(0), c.GetTeam(1), 2, 0);
                MatchRecords.PlayMatch(c, c.GetTeam(2), c.GetTeam(3), 1, 1);
                MatchRecords.PlayMatch(c, c.GetTeam(4), c.GetTeam(5), 3, 1);

                MatchRecords.PlayMatch(c, c.GetTeam(3), c.GetTeam(0), 3, 1);
                MatchRecords.PlayMatch(c, c.GetTeam(1), c.GetTeam(4), 3, 1);
                MatchRecords.PlayMatch(c, c.GetTeam(5), c.GetTeam(2), 3, 1);

                MatchRecords.PlayMatch(c, c.GetTeam(0), c.GetTeam(2), 4, 0);
                MatchRecords.PlayMatch(c, c.GetTeam(1), c.GetTeam(5), 2, 3);
                MatchRecords.PlayMatch(c, c.GetTeam(4), c.GetTeam(3), 3, 1);

                MatchRecords.PlayMatch(c, c.GetTeam(5), c.GetTeam(0), 2, 2);
                MatchRecords.PlayMatch(c, c.GetTeam(3), c.GetTeam(1), 2, 3);
                MatchRecords.PlayMatch(c, c.GetTeam(2), c.GetTeam(4), 3, 1);

                MatchRecords.PlayMatch(c, c.GetTeam(4), c.GetTeam(0), 1, 1);
                MatchRecords.PlayMatch(c, c.GetTeam(1), c.GetTeam(2), 2, 2);
                MatchRecords.PlayMatch(c, c.GetTeam(3), c.GetTeam(5), 3, 1);
                // MatchRecords.SoccerMatch(c, c.GetTeam(2), new SoccerTeam(), 20, 0); // resolver isso aqui! -> jogos externos ao campeonato influenciando na classificação do mesmo
                c.Sort();
                Console.WriteLine(c.Table());

                for (int i = 0; i < c.NumberOfTeams; i++)
                    Console.WriteLine(c.GetTeam(i));

            }
            catch (SoccerException e) {
                Console.WriteLine(e.Message);
            }
        }

        static void printArray<T>(List<T> list) {
            if (list != null) {
                T[] array = new T[list.Count];
                for (int i = 0; i < list.Count; i++)
                    array[i] = list[i];
                printArray(array);
            }
        }

        static void printArray<T>(T[] array) {
            Console.Write("{");
            if (array != null) {
                for (int i = 0; i < array.Length - 1; i++)
                    Console.Write($"{array[i]}, ");
                Console.Write(array[array.Length - 1]);
            }
            Console.WriteLine("}");
        }
    }
}
