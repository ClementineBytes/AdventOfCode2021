using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2021.Utilities
{
    public class InputReader
    {
        public string FileDirectory = Path.Combine(Environment.CurrentDirectory, @"Input\");
        public string FileName { get; set; }
        public InputReader(string fileName)
        {
            FileName = FileDirectory + fileName;
        }
        public int[] GetInputAsIntArray()
        {
            string inputStr = GetInputFileAsString();
            var inputStrArr = inputStr.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var data = new List<int>();
            foreach (var str in inputStrArr)
            {
                data.Add(int.Parse(str));
            }
            return data.ToArray();
        }

        public string GetInputFileAsString()
        {
            string data = "";
            try
            {
                using (var reader = new StreamReader(FileName))
                {
                    data = reader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading input file " + FileName);
                Console.WriteLine(ex.Message);
            }
            return data;
        }

        public int[,] GetBinaryArrayFromInput()
        {
            string inputStr = GetInputFileAsString();
            var inputStrArr = inputStr.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            //create two dimensional array to hold values for each binary "column" from the input
            int[,] binaryArr = new int[inputStrArr.Length, inputStrArr[0].Length];

            for (int i = 0; i < inputStrArr.Length; i++)
            {
                var numberRow = inputStrArr[i].ToCharArray();
                for (int j = 0; j < numberRow.Length; j++)
                {
                    binaryArr[i, j] = (int)Char.GetNumericValue(numberRow[j]);
                }
            }

            return binaryArr;
        }

        public int[][] GetJaggedArrayFromInput()
        {
            string inputStr = GetInputFileAsString();
            var inputStrArr = inputStr.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            //create two dimensional array to hold values for each binary "column" from the input
            int[][] binaryArr = new int[1000][];

            for (int i = 0; i < inputStrArr.Length; i++)
            {
                binaryArr[i] = new int[12];
                var numberRow = inputStrArr[i].ToCharArray();
                for (int j = 0; j < numberRow.Length; j++)
                {
                    binaryArr[i][j] = (int)Char.GetNumericValue(numberRow[j]);
                }
            }
            return binaryArr;
        }
    }
}
