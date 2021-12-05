﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public int[] GetIntArrayFromInput()
        {
            var inputStrArr = GetStringArrayFromInput();
            var data = new List<int>();
            foreach (var str in inputStrArr)
            {
                data.Add(int.Parse(str));
            }
            return data.ToArray();
        }

        public string[] GetStringArrayFromInput()
        {
            string dataStr = "";
            try
            {
                using (var reader = new StreamReader(FileName))
                {
                    dataStr = reader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading input file " + FileName);
                Console.WriteLine(ex.Message);
            }
            var data = dataStr.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return data;
        }

        public int[,] GetBinaryArrayFromInput()
        {
            var inputStrArr = GetStringArrayFromInput();

            //create two dimensional array to hold values for each binary column from the input
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
            var inputStrArr = GetStringArrayFromInput();

            //create jagged array to hold values for each binary column from the input
            int[][] binaryArr = new int[inputStrArr.Length][];

            for (int i = 0; i < inputStrArr.Length; i++)
            {
                binaryArr[i] = new int[inputStrArr[i].Length];
                var numberRow = inputStrArr[i].ToCharArray();
                for (int j = 0; j < numberRow.Length; j++)
                {
                    binaryArr[i][j] = (int)Char.GetNumericValue(numberRow[j]);
                }
            }
            return binaryArr;
        }

        public List<int> GetBingoNumbersFromInput()
        {
            string dataStr = "";
            try
            {
                using (var reader = new StreamReader(FileName))
                {
                    dataStr = reader.ReadLine();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading input file " + FileName);
                Console.WriteLine(ex.Message);
            }
            var data = dataStr.Split(',').Select(int.Parse).ToList();
            return data;
        }

        public List<int[][]> GetBingoBoardsFromInput()
        {
            string dataStr = "";
            try
            {
                using (var reader = new StreamReader(FileName))
                {
                    reader.ReadLine(); //skip the first line of bingo numbers to get to the boards
                    dataStr = reader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading input file " + FileName);
                Console.WriteLine(ex.Message);
            }
            dataStr = dataStr.Replace("\r\n", " ");
            var data = dataStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var bingoBoards = new List<int[][]>();

            for (int i = 0; i < data.Length; i+=25)
            {
                var bingoBoard = new int[25][];
                for (int j = 0; j < 25; j++)
                {
                    bingoBoard[j] = new int[1];
                    bingoBoard[j][0] = int.Parse(data[i + j]);
                }
                bingoBoards.Add(bingoBoard);
            }
            return bingoBoards;
        }
    }
}
