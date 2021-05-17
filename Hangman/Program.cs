using System;
using System.IO;

namespace StartingCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman");

            string filePath = @"TextFile1.txt";

            string word = RandomWord(GetWords(filePath)).Trim();

            string displayWord = ReplaceChars(word);

            int tries = 5;

            while (tries > 0) {
                Console.WriteLine($"Guess a letter in the word {displayWord}");
                string guess = Console.ReadLine();
                bool correct = CheckCorrectness(word, displayWord, guess);
                Console.WriteLine(correct);
                if (correct == true)
                {
                    displayWord = CorrectReplacement(word, displayWord, guess);
                }
                else { 
                    tries--; 
                }
                Figure(tries);
                

                if (displayWord == word) { Console.WriteLine("You Guessed the word: "); break; }
            }

            Console.WriteLine(word);
        }

        public static string[] GetWords(string filename)
        {
            filename = Path.GetFullPath(filename);
            using FileStream fileStream = File.OpenRead(filename);
            using StreamReader streamReader = new(fileStream);
            string words = streamReader.ReadToEnd();
            return words.Split("\n");
        }

        public static string RandomWord(string[] words) 
        {
            Random random = new();

            string randomWord = words[random.Next(words.Length)];

            return randomWord;
        }

        public static string ReplaceChars(string word) 
        {
            Random random = new();
            char letterToShow = word[random.Next(word.Length-1)];
            string newString = "";

            foreach (char a in word) {
                if (!a.Equals(letterToShow))
                {
                    newString = newString + "_";
                }
                else { newString += letterToShow; }
            }
            return newString;
        }

        public static bool CheckCorrectness(string word, string hidden, string guess)
        {
            int i = 0;
            while (i < word.Length) {
                if (word[i].ToString()==guess && hidden[i].ToString()=="_") {
                    return true;
                }
                i++;
            }
            return false;
        }

        public static string CorrectReplacement(string word, string hidden, string guess) 
        {
            int i = 0;
            string newHidden = "";
            while (i < word.Length)
            {
                if (word[i].ToString() == guess && hidden[i].ToString() == "_")
                {
                    newHidden += guess;
                }
                else 
                {
                    newHidden += hidden[i];
                }
                i++;
            }
            return newHidden;
        }

        public static void Figure(int i)
        {
            string figure;
            switch (i) 
            {
                case 1:
                    figure = @"/-----
|    0 
|   /|\
|    |
|   / \";
                    Console.WriteLine(figure);
                    break;
                case 2:
                    figure = @"/-----
|    0 
|   /|\
|    |
|   ";
                    Console.WriteLine(figure);
                    break;
                case 3:
                    figure = @"/-----
|    0 
|   /|\
|    
|   ";
                    Console.WriteLine(figure);
                    break;
                case 4:
                    figure = @"/-----
|    0 
|   
|    
|   ";
                    Console.WriteLine(figure);
                    break;
            }
        }
    }
}