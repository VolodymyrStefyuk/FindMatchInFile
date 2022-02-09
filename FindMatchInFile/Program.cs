using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FindMatchInFile
{
    class Program
    {
        static void Main(string[] args)
        {
            bool complete = false;
            while (!complete)
            {
                Console.WriteLine("Enter the desired word,mode and file path" +
                "\n example: зуб, 1, test.txt");
                string[] splitEnterValue = Console.ReadLine().Split(new char[] { ',' });

                try
                {
                    Console.WriteLine("The word matched: " + CountMatches(splitEnterValue[0].Trim(), 
                        Convert.ToInt32(splitEnterValue[1].Trim()), splitEnterValue[2].Trim()));
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong mode selected");
                    continue;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File not found");
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrectly entered data");
                    continue;
                }
                Console.WriteLine("\n Want to find another world?");
                if (Console.ReadLine() == "yes") continue;
                else break;
            }
        }

        static string LoadFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            try
            {
                string[] contentFile = File.ReadAllLines(path);
                string result = string.Empty;
                foreach (string x in contentFile)
                {
                    result += x;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static int CountMatches(string word,int operatingMode,string pathToFile)
        {
            string[] paterns = { $@"{word}", $@"\b{word}\b" };
            MatchCollection matches;
            try
            {
                Regex rx = new Regex(paterns[operatingMode-1],
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
                matches = rx.Matches(LoadFile(pathToFile).ToLower());
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
            catch (FieldAccessException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return matches.Count;
        }
    }
}
