using System;
using System.Collections.Generic;

namespace SoccerSimulator {
    class Program {
        static void Main(string[] args) {
            try {
                SoccerChampionship c = new SoccerChampionship(6);

                Console.WriteLine(c.Table());
                SoccerMatch.Play(c.GetTeam(0), c.GetTeam(1), 2, 0);
                SoccerMatch.Play(c.GetTeam(2), c.GetTeam(3), 1, 1);
                SoccerMatch.Play(c.GetTeam(4), c.GetTeam(5), 3, 1);
                c.Sort();
                Console.WriteLine(c.Table());

                SoccerMatch.Play(c.GetTeam(3), c.GetTeam(0), 3, 1);
                SoccerMatch.Play(c.GetTeam(1), c.GetTeam(4), 3, 1);
                SoccerMatch.Play(c.GetTeam(5), c.GetTeam(2), 3, 1);
                c.Sort();
                Console.WriteLine(c.Table());

                SoccerMatch.Play(c.GetTeam(0), c.GetTeam(2), 4, 0);
                SoccerMatch.Play(c.GetTeam(1), c.GetTeam(5), 2, 3);
                SoccerMatch.Play(c.GetTeam(4), c.GetTeam(3), 3, 1);
                c.Sort();
                Console.WriteLine(c.Table());

                SoccerMatch.Play(c.GetTeam(5), c.GetTeam(0), 2, 2);
                SoccerMatch.Play(c.GetTeam(3), c.GetTeam(1), 2, 3);
                SoccerMatch.Play(c.GetTeam(2), c.GetTeam(4), 3, 1);
                c.Sort();
                Console.WriteLine(c.Table());

                SoccerMatch.Play(c.GetTeam(4), c.GetTeam(0), 1, 1);
                SoccerMatch.Play(c.GetTeam(1), c.GetTeam(2), 2, 2);
                SoccerMatch.Play(c.GetTeam(3), c.GetTeam(5), 3, 1);
                SoccerMatch.Play(c.GetTeam(2), new SoccerTeam(), 20, 0); // resolver isso aqui! -> jogos externos ao campeonato influenciando na classificação do mesmo
                c.Sort();
                Console.WriteLine(c.Table());

                Console.WriteLine(c.GetTeam(1));

                List<int> l = new List<int> { 1, 3, 10, 6, 0, 8 };
                printArray(l);
                l.Sort((x, y) => y.CompareTo(x));
                printArray(l);

            }
            catch (SoccerException e) {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Teste!!");
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
