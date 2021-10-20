using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSimulator.Utilities {
    static class ConsoleDataGrid {

        public static string DivisionLine(string csvWidth) {

            string line = null;

            if (csvWidth != null)
                line = DivisionLine(CsvToIntArray(csvWidth));

            return line;
        }

        public static string DivisionLine(int[] width) {

            StringBuilder line = null;

            if (width != null) {
                line = new StringBuilder();
                for (int i = 0; i < width.Length; i++) {
                    line.Append(string.Empty.PadRight(width[i], '-'));
                    line.Append(i < width.Length - 1 ? "-+-" : "\n");
                }
            }

            return $"{line}";
        }

        public static string GridRow(string csvContent, string csvWidth) {

            string row = null;

            if (csvContent != null && csvWidth != null) {
                try {
                    row = GridRow(csvContent.Split(','), CsvToIntArray(csvWidth));
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }

            return row;
        }

        public static string GridRow(string[] content, int[] width) {

            StringBuilder row = null;

            if (content != null && width != null) {
                try {
                    row = new StringBuilder();
                    for (int i = 0; i < width.Length; i++) {
                        row.Append(content[i].PadRight(width[i]));
                        row.Append(i < width.Length - 1 ? " | " : "\n");
                    }
                }
                catch (IndexOutOfRangeException ior) {
                    Console.WriteLine(ior.Message);
                }
            }

            return $"{row}";
        }

        public static int[] CsvToIntArray(string csv) {

            int[] array = null;

            if (csv != null) {
                try {
                    string[] value = csv.Split(',');
                    array = new int[value.Length];
                    for (int i = 0; i < array.Length; i++)
                        array[i] = int.Parse(value[i]);
                }
                catch (FormatException f) {
                    Console.WriteLine(f.Message);
                }
            }

            return array;
        }
    }
}
