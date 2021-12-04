using AdventOfCode2021.Utilities;
using System;
using System.Linq;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Day 1
            //Day 1 
            var numOfIncreases = GetNumberOfDepthIncreases("Day1-Input.txt");
            Console.WriteLine("number of increases is - " + numOfIncreases);

            var numOfSumIncreases = GetNumberOfSumIncreases("Day1-Input.txt");
            Console.WriteLine("number of sum increases is - " + numOfSumIncreases);
            #endregion

            #region Day 2
            //Day 2
            var submarinePosition = GetSubmarinePosition("Day2-Input.txt");
            Console.WriteLine("Submarine position is " + submarinePosition);
            #endregion

            #region Day 3
            //Day 3
            var powerConsumption = GetSubmarinePowerConsumption("Day3-Input.txt");
            Console.WriteLine("Submarine power consumption is " + powerConsumption);

            var lifeSupportRating = GetSubmarineLifeSupportRating("Day3-Input.txt");
            Console.WriteLine("Submarine life support rating is " + lifeSupportRating);
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
                    currentSum = currentSum = depthsArr[i] + depthsArr[i + 1] + depthsArr[i + 2];
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
                    gammaRateStr+='1';
                    epsilonRateStr+='0';
                }
                else
                {
                    gammaRateStr+='0';
                    epsilonRateStr+='1';
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

                var numberOnes = from x in nums
                                 where x[cursor] == 1
                                 select x;

                var numberZeroes = from x in nums
                                   where x[cursor] == 0
                                   select x;

                if (numberOnes.ToArray().Length >= numberZeroes.ToArray().Length)
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

                var numberOnes = from x in nums
                                 where x[cursor] == 1
                                 select x;

                var numberZeroes = from x in nums
                                   where x[cursor] == 0
                                   select x;

                if (numberOnes.ToArray().Length < numberZeroes.ToArray().Length)

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

        }
}
