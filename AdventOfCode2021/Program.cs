using AdventOfCode2021.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Day 1
            //Day 1 
            //var numOfIncreases = GetNumberOfDepthIncreases("Day1-Input.txt");
            //Console.WriteLine("number of increases is " + numOfIncreases);

            //var numOfSumIncreases = GetNumberOfSumIncreases("Day1-Input.txt");
            //Console.WriteLine("number of sum increases is " + numOfSumIncreases);
            #endregion

            #region Day 2
            //Day 2
            //var submarinePosition = GetSubmarinePosition("Day2-Input.txt");
            //Console.WriteLine("Submarine position is " + submarinePosition);
            #endregion

            #region Day 3
            //Day 3
            //var powerConsumption = GetSubmarinePowerConsumption("Day3-Input.txt");
            //Console.WriteLine("Submarine power consumption is " + powerConsumption);

            //var lifeSupportRating = GetSubmarineLifeSupportRating("Day3-Input.txt");
            //Console.WriteLine("Submarine life support rating is " + lifeSupportRating);
            #endregion

            #region Day 4
            //Day 4 Bingo Game
            //var firstwinningBoardScore = GetFirstWinningBoardScore("Day4-Input.txt");
            //var lastWinningBoardScore = GetLastWinningBoardScore("Day4-Input.txt");

            #endregion

            #region Day 5
            var numberOfLinesIntersecting = GetNumberOfPointsLinesOverlap("Day5-Input.txt");
            Console.WriteLine(numberOfLinesIntersecting);
            #endregion
            Console.ReadLine();
        }

        #region Day 1
        //part 1 number of times a depth measurement increases
        public static int GetNumberOfDepthIncreases(string inputFile)
        {
            var reader = new InputReader(inputFile);
            var depthsArr = reader.GetIntArrayFromInput();
            int result = 0;
            for (int i = 1; i < depthsArr.Length; i++)
            {
                if (depthsArr[i] > depthsArr[i - 1])
                    result++;
            }
            return result;
        }

        //part 2
        public static int GetNumberOfSumIncreases(string inputFile)
        {
            var reader = new InputReader(inputFile);
            var depthsArr = reader.GetIntArrayFromInput();
            int result = 0;
            int previousSum = 0;
            int currentSum = 0;

            for (int i = 0; i < depthsArr.Length; i++)
            {
                if (i <= 0)
                {
                    currentSum = depthsArr[i] + depthsArr[i + 1] + depthsArr[i + 2];
                }
                else if (i + 3 > depthsArr.Length)
                {
                    break;
                }
                else
                {
                    previousSum = currentSum;
                    currentSum = depthsArr[i] + depthsArr[i + 1] + depthsArr[i + 2];
                    if (currentSum > previousSum)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Day 2
        public static int GetSubmarinePosition(string inputFile)
        {
            var reader = new InputReader(inputFile);
            var inputStrArr = reader.GetStringArrayFromInput();

            int depthIncrease = 0;
            int depthDecrease = 0;
            int horizontalIncrease = 0;
            int aim = 0;

            foreach (string input in inputStrArr)
            {
                if (input.ToLower().StartsWith("up"))
                {
                    var numericValue = int.Parse(input.Substring(2));
                    aim -= numericValue;
                }
                else if (input.ToLower().StartsWith("down"))
                {
                    var numericValue = int.Parse(input.Substring(4));
                    aim += numericValue;
                }
                else if (input.ToLower().StartsWith("forward"))
                {
                    var numericValue = int.Parse(input.Substring(7));
                    horizontalIncrease += numericValue;
                    depthIncrease += (aim * numericValue);
                }
            }

            int totalDepthChange = depthIncrease - depthDecrease;
            int result = totalDepthChange * horizontalIncrease;

            return result;
        }
        #endregion

        #region Day 3
        public static decimal GetSubmarinePowerConsumption(string inputFile)
        {
            var reader = new InputReader(inputFile);
            var binaryArr = reader.GetBinaryArrayFromInput();

            var arrayHelper = new ArrayHelper();

            var gammaRateStr = "";
            var epsilonRateStr = "";

            for (int i = 0; i <= 11; i++)
            {
                int columnSum = arrayHelper.GetColumn(binaryArr, i).Sum();
                if (columnSum >= 500)
                {
                    gammaRateStr += '1';
                    epsilonRateStr += '0';
                }
                else
                {
                    gammaRateStr += '0';
                    epsilonRateStr += '1';
                }
            }

            var gammaRate = Convert.ToInt32(gammaRateStr, 2);
            var epsilonRate = Convert.ToInt32(epsilonRateStr, 2);

            decimal result = gammaRate * epsilonRate;
            return result;
        }

        public static decimal GetSubmarineLifeSupportRating(string inputFile)
        {
            var reader = new InputReader(inputFile);
            var binaryArr = reader.GetJaggedArrayFromInput();

            var arrayHelper = new ArrayHelper();

            var mostCommonNumber = FilterToMostCommonNumber(binaryArr, 0, arrayHelper);
            var leastCommonNumber = FilterToLeastCommonNumber(binaryArr, 0, arrayHelper);

            var mostCommonNumStr = "";
            var leastCommonNumStr = "";

            for (int i = 0; i < 12; i++)
            {
                mostCommonNumStr += mostCommonNumber[0][i];
                leastCommonNumStr += leastCommonNumber[0][i];
            }

            var oxygenGeneratorRating = Convert.ToInt32(mostCommonNumStr, 2);
            var co2ScrubberRating = Convert.ToInt32(leastCommonNumStr, 2);

            decimal result = oxygenGeneratorRating * co2ScrubberRating;
            return result;
        }

        static int[][] FilterToMostCommonNumber(int[][] nums, int cursor, ArrayHelper arrayHelper)
        {
            if (cursor == 12 || nums.Length <= 1)
            {
                return nums;
            }

            else
            {
                int columnSum = arrayHelper.GetColumn(nums, cursor).Sum();
                int mostCommonNumber = 0;

                var ones = from x in nums
                           where x[cursor] == 1
                           select x;

                var zeros = from x in nums
                            where x[cursor] == 0
                            select x;

                if (ones.ToArray().Length >= zeros.ToArray().Length)
                {
                    mostCommonNumber = 1;
                }
                else
                {
                    mostCommonNumber = 0;
                }

                var temp = from x in nums
                           where x[cursor] == mostCommonNumber
                           select x;

                nums = temp.ToArray();
                cursor++;
                return FilterToMostCommonNumber(nums, cursor, arrayHelper);
            }
        }

        static int[][] FilterToLeastCommonNumber(int[][] nums, int cursor, ArrayHelper arrayHelper)
        {
            if (cursor == 12 || nums.Length <= 1)
            {
                return nums;
            }

            else
            {
                int columnSum = arrayHelper.GetColumn(nums, cursor).Sum();
                int leastCommonNumber = 0;

                var ones = from x in nums
                           where x[cursor] == 1
                           select x;

                var zeros = from x in nums
                            where x[cursor] == 0
                            select x;

                if (ones.ToArray().Length < zeros.ToArray().Length)

                {
                    leastCommonNumber = 1;
                }
                else
                {
                    leastCommonNumber = 0;
                }

                var temp = from x in nums
                           where x[cursor] == leastCommonNumber
                           select x;

                nums = temp.ToArray();
                cursor++;
                return FilterToLeastCommonNumber(nums, cursor, arrayHelper);
            }
        }
        #endregion

        #region Day 4
        public static int GetFirstWinningBoardScore(string inputFile)
        {
            var reader = new InputReader(inputFile);
            var bingoNumbers = reader.GetBingoNumbersFromInput();
            var bingoBoards = reader.GetBingoBoardsFromInput();
            var numbersDrawn = new List<int>();
            int winningScore = 0;
            List<int> winningBoard = null;

            for (int i = 0; i < bingoNumbers.Count; i++)
            {
                if (i < 4)
                {
                    //draw five numbers to start
                    numbersDrawn.Add(bingoNumbers[0]);
                    numbersDrawn.Add(bingoNumbers[1]);
                    numbersDrawn.Add(bingoNumbers[2]);
                    numbersDrawn.Add(bingoNumbers[3]);
                    numbersDrawn.Add(bingoNumbers[4]);
                    i = 4;
                    winningBoard = GetFirstWinner(numbersDrawn, bingoBoards);
                    if (winningBoard != null)
                    {
                        var unmarkedNums = winningBoard.Except(numbersDrawn).ToList();
                        winningScore = CalculateWinningScore(unmarkedNums, numbersDrawn);
                        break;
                    }
                }
                else
                {
                    numbersDrawn.Add(bingoNumbers[i]);
                    winningBoard = GetFirstWinner(numbersDrawn, bingoBoards);
                    if (winningBoard != null)
                    {
                        var unmarkedNums = winningBoard.Except(numbersDrawn).ToList();
                        winningScore = CalculateWinningScore(unmarkedNums, numbersDrawn);
                        break;
                    }
                }
            }
            return winningScore;
        }

        public static int GetLastWinningBoardScore(string inputFile)
        {
            var reader = new InputReader(inputFile);

            var bingoNumbers = reader.GetBingoNumbersFromInput();
            var bingoBoards = reader.GetBingoBoardsFromInput();
            var numbersDrawn = new List<int>();
            var numbersDrawnWhenBoardWon = new List<int[]>();
            var winningBoards = new List<int[]>();

            int[] winningBoard = new int[25];

            for (int i = 0; i < bingoNumbers.Count; i++)
            {
                if (i == 0)
                {
                    //draw five numbers to start
                    numbersDrawn.Add(bingoNumbers[0]);
                    numbersDrawn.Add(bingoNumbers[1]);
                    numbersDrawn.Add(bingoNumbers[2]);
                    numbersDrawn.Add(bingoNumbers[3]);
                    numbersDrawn.Add(bingoNumbers[4]);
                    i = 4;
                    FilterWinningBoards(numbersDrawn, ref bingoBoards, ref winningBoards, ref numbersDrawnWhenBoardWon);
                }
                else
                {
                    numbersDrawn.Add(bingoNumbers[i]);
                    FilterWinningBoards(numbersDrawn, ref bingoBoards, ref winningBoards, ref numbersDrawnWhenBoardWon);
                }
            }

            var lastWinningBoard = winningBoards.Last();
            var numbersDrawnSnapshot = numbersDrawnWhenBoardWon.Last();
            var unMarkedBoardNums = lastWinningBoard.Except(numbersDrawnSnapshot).ToList();
            var winningScore = CalculateWinningScore(unMarkedBoardNums, numbersDrawnSnapshot.ToList());
            return winningScore;
        }

        public static List<int> GetFirstWinner(List<int> numbersDrawn, List<string[][]> bingoBoards)
        {
            foreach (string[][] board in bingoBoards)
            {
                var boardRows = GetBingoBoardRows(board);
                var boardNumbers = new List<int>();
                bool boardWon = false;

                foreach (int[] row in boardRows)
                {
                    boardNumbers.AddRange(row.ToList());
                }

                if (!boardRows[0].Except(numbersDrawn).Any() || !boardRows[1].Except(numbersDrawn).Any() || !boardRows[2].Except(numbersDrawn).Any() || !boardRows[3].Except(numbersDrawn).Any() || !boardRows[4].Except(numbersDrawn).Any())
                {
                    var unmarkedNums = boardNumbers.Except(numbersDrawn).ToList();
                    var winningScore = CalculateWinningScore(unmarkedNums, numbersDrawn);
                    boardWon = true;
                    return boardNumbers;
                }

                //if no row matches check columns
                if (boardWon == false)
                {
                    var boardColumns = GetBingoBoardColumns(boardRows);

                    if (!boardColumns[0].Except(numbersDrawn).Any() || !boardColumns[1].Except(numbersDrawn).Any() || !boardColumns[2].Except(numbersDrawn).Any() || !boardColumns[3].Except(numbersDrawn).Any() || !boardColumns[4].Except(numbersDrawn).Any())
                    {
                        return boardNumbers;
                    }
                }
            }
            return null;
        }

        #region BingoHelperMethods 
        public static List<int[]> GetBingoBoardRows(string[][] board)
        {
            var rows = new List<int[]>();

            var row1 = board[0][0].Split(" ");
            row1 = row1.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var row2 = board[1][0].Split(" ");
            row2 = row2.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var row3 = board[2][0].Split(" ");
            row3 = row3.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var row4 = board[3][0].Split(" ");
            row4 = row4.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var row5 = board[4][0].Split(" ");
            row5 = row5.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var boardNumbers = new List<int>();

            var row1Nums = new int[5];
            var row2Nums = new int[5];
            var row3Nums = new int[5];
            var row4Nums = new int[5];
            var row5Nums = new int[5];

            for (int i = 0; i < 5; i++)
            {
                row1Nums[i] = int.Parse(row1[i]);
                row2Nums[i] = int.Parse(row2[i]);
                row3Nums[i] = int.Parse(row3[i]);
                row4Nums[i] = int.Parse(row4[i]);
                row5Nums[i] = int.Parse(row5[i]);
            }

            rows.Add(row1Nums);
            rows.Add(row2Nums);
            rows.Add(row3Nums);
            rows.Add(row4Nums);
            rows.Add(row5Nums);

            return rows;
        }

        public static List<int[]> GetBingoBoardColumns(List<int[]> boardRows)
        {
            var columns = new List<int[]>();

            var column1 = new int[5];
            var column2 = new int[5];
            var column3 = new int[5];
            var column4 = new int[5];
            var column5 = new int[5];

            for (int i = 0; i < boardRows.Count; i++)
            {
                var row = boardRows.ElementAt(i);
                column1[i] = row[0];
                column2[i] = row[1];
                column3[i] = row[2];
                column4[i] = row[3];
                column5[i] = row[4];

            }

            columns.Add(column1);
            columns.Add(column2);
            columns.Add(column3);
            columns.Add(column4);
            columns.Add(column5);

            return columns;
        }

        public static int CalculateWinningScore(List<int> unmarkedBoardNums, List<int> numbersDrawn)
        {
            int lastNumDrawn = numbersDrawn.Last();
            int sumOfUnmarkedNums = unmarkedBoardNums.Sum();
            int score = sumOfUnmarkedNums * lastNumDrawn;
            return score;
        }

        public static void FilterWinningBoards(List<int> numbersDrawn, ref List<string[][]> bingoBoards, ref List<int[]> winningBoards, ref List<int[]> numbersDrawnWhenBoardWon)
        {

            for (int x = 0; x < bingoBoards.Count; x++)
            {
                var board = bingoBoards.ElementAt(x);
                bool boardWon = false;

                var boardRows = GetBingoBoardRows(board);

                var boardNumbers = new List<int>();
                foreach (int[] row in boardRows)
                {
                    boardNumbers.AddRange(row.ToList());
                }

                if ((!boardRows[0].Except(numbersDrawn).Any() || !boardRows[1].Except(numbersDrawn).Any() || !boardRows[2].Except(numbersDrawn).Any() || !boardRows[3].Except(numbersDrawn).Any() || !boardRows[4].Except(numbersDrawn).Any()))
                {
                    var winningBoard = boardNumbers.ToArray();
                    winningBoards.Add(winningBoard);
                    bingoBoards.Remove(board);
                    numbersDrawnWhenBoardWon.Add(numbersDrawn.ToArray());
                    boardWon = true;
                }

                //if no match on rows, check board columns
                if (boardWon == false)
                {
                    var boardColumns = GetBingoBoardColumns(boardRows);

                    if ((!boardColumns[0].Except(numbersDrawn).Any() || !boardColumns[1].Except(numbersDrawn).Any() || !boardColumns[2].Except(numbersDrawn).Any() || !boardColumns[3].Except(numbersDrawn).Any() || !boardColumns[4].Except(numbersDrawn).Any()))
                    {
                        var winningBoard = boardNumbers.ToArray();
                        winningBoards.Add(winningBoard);
                        bingoBoards.Remove(board);
                        numbersDrawnWhenBoardWon.Add(numbersDrawn.ToArray());
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Day 5
        public static int GetNumberOfPointsLinesOverlap(string inputFile)
        {
            var reader = new InputReader(inputFile);

            var data = reader.GetLinePoints();
            var xAxisCoordinates = data.ElementAt(0);
            var yAxisCoordinates = data.ElementAt(1);

            var maxHorizontal = xAxisCoordinates.Max();
            var minHorizontal = xAxisCoordinates.Min();

            var maxVertical = yAxisCoordinates.Max();
            var minVertical = yAxisCoordinates.Min();
            var board = new Dictionary<(int, int), int>();

            //initialize dictionary
            for (int y = minVertical; y <= maxVertical; y++)
            {
                int coordinate = y;
                for (int x = minHorizontal; x <= maxHorizontal; x++)
                {
                    int xCoordinate = x;
                    board.Add((xCoordinate, y), 0);
                }
            }

            for (int i = 1; i < xAxisCoordinates.Length; i += 2)
            {
                int xPoint1 = xAxisCoordinates[i - 1];
                int xPoint2 = xAxisCoordinates[i];
                int yPoint1 = yAxisCoordinates[i - 1];
                int yPoint2 = yAxisCoordinates[i];

                if (xPoint1 > xPoint2)
                {
                    int temp = xPoint1;
                    xPoint1 = xPoint2;
                    xPoint2 = temp;

                    temp = yPoint1;
                    yPoint1 = yPoint2;
                    yPoint2 = temp;
                }

                if (xPoint1 == xPoint2)
                {
                    if (yPoint1 > yPoint2)
                    {
                        int temp = xPoint1;
                        xPoint1 = xPoint2;
                        xPoint2 = temp;

                        temp = yPoint1;
                        yPoint1 = yPoint2;
                        yPoint2 = temp;
                    }

                    //insert vertical line
                    for (int j = yPoint1; j <= yPoint2; j++)
                    {
                        board[(xPoint1, j)] = board[(xPoint1, j)] + 1;
                    }

                }
                else if (yPoint1 == yPoint2)
                {
                    //insert horizontal line
                    for (int j = xPoint1; j <= xPoint2; j++)
                    {
                        board[(j, yPoint1)] = board[(j, yPoint1)] + 1;
                    }

                }
                else
                {
                    //insert diagonal line
                    var xPoints = new List<int>();
                    var yPoints = new List<int>();

                    for (int x = xPoint1; x <= xPoint2; x++)
                    {
                        xPoints.Add(x);
                    }

                    if (yPoint1 < yPoint2)
                    {
                        //ascending diagonal line
                        for (int y = yPoint1; y <= yPoint2; y++)
                        {
                            yPoints.Add(y);
                        }
                    }
                    else
                    {
                        //descending diagonal line
                        for (int y = yPoint1; y >= yPoint2; y--)
                        {
                            yPoints.Add(y);
                        }
                    }

                    for (int j = 0; j < xPoints.Count; j++)
                    {
                        int x = xPoints.ElementAt(j);
                        int y = yPoints.ElementAt(j);
                        board[(x, y)] = board[(x, y)] + 1;
                    }
                }
            }

            var result = board.Where(x => x.Value > 1).Count();
            return result;
        }
        #endregion
    }
}
