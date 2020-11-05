using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            string optionA = "A";
            string descriptionA = "Making change";
            string optionB = "B";
            string descriptionB = "Counting letters";
            string optionC = "C";
            string descriptionC = "Number guessing game";
            string optionQ = "Q";
            string descriptionQ = "Quit";

            //Creating string for 'choice' 
            string choice = string.Empty;

            //Switch statement for the option; This statement will trigger respective method
            while (choice != "Q")
            {
                //Present user with options:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Please chose an activity: ");
                Console.WriteLine($"{optionA.PadLeft(3, ' ')}. {descriptionA.PadRight(21,' ')}");
                Console.WriteLine($"{optionB.PadLeft(3, ' ')}. {descriptionB.PadRight(21, ' ')}");
                Console.WriteLine($"{optionC.PadLeft(3, ' ')}. {descriptionC.PadRight(21, ' ')}");
                Console.WriteLine($"{optionQ.PadLeft(3, ' ')}. {descriptionQ.PadRight(21, ' ')}");

                //Capture selection; change color of output:
                Console.Write("Your choice: ");
                Console.ForegroundColor = ConsoleColor.Green;
                choice = Console.ReadLine().ToUpper();
                //Make sure that the next output is in 'correct' color
                Console.ForegroundColor = ConsoleColor.Magenta;

                switch (choice) 
                {
                    //Calls the MakingChange() method
                    case "A":
                        MakingChange();
                         choice = string.Empty;
                        continue;

                    case "B":
                        CountingLetters();
                        choice = string.Empty;
                        continue;

                    case "C":
                        NumberGuessingGame();
                        choice = string.Empty;
                        continue;
                }
            }
        }

        private static void MakingChange()
        {
            #region Variables
            string due_str = string.Empty;
            string paid_str = string.Empty;
            decimal? due = null;
            decimal? paid = null;
            decimal? change = null;
            decimal? change_cpy = null; 
            #endregion



            #region //Infinite loop for 'due'
            do
            {
                //Make output for each method yellow
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Please enter an amount due: ");
                Console.ForegroundColor = ConsoleColor.Green;
                due_str = Console.ReadLine();

                //Parse input for 'due' or go to next iteration of the 'while' loop
                if (!string.IsNullOrEmpty(due_str) && decimal.TryParse(due_str, out decimal result_d) == true)
                {
                    due = result_d;
                    break;
                }
                else
                {
                    due_str = string.Empty;
                    continue;
                }

            } while (string.IsNullOrEmpty(due_str)); 
            #endregion

            #region //Infinite loop for 'paid'
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Please enter an amount paid: ");
                Console.ForegroundColor = ConsoleColor.Green;
                paid_str = string.Format($"{Console.ReadLine(),1}");
                //Parse input for 'paid'
                if (!string.IsNullOrEmpty(paid_str) && decimal.TryParse(paid_str, out decimal result_p) == true)
                {
                    paid = result_p;
                    break;
                }
                else
                {
                    paid_str = string.Empty;
                    continue;
                }
            } while (string.IsNullOrEmpty(paid_str));
            #endregion


            #region //Determine if change is due; if yes, then also determine denominations
            //If both 'due' and 'paid' are set to valid decimal values do:
            if (due>= 0 && paid >=0) 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (paid < due)
                {
                    Console.WriteLine("'Due' balance remains. No change to be given.");
                }
                else if (paid == due)
                {
                    Console.WriteLine("Amount 'due' has been fully-paid. No change to be given.");
                }
                else if (paid > due) 
                {
                    #region //Local variables
                    int index;
                    int updateCounts; //Needs to be int since denomination-counts will only be updated with whole-numbers
                    decimal placeHolder;
                    change = paid - due;
                    change_cpy = change;

                    //Creating 2D array where:
                    //First row of dimension[0] represents the [denominations]
                    //Second row of dimension[0] represents [counts]
                    decimal[,] storage_shelf = new decimal[2, 11]
                    {   {100M,  50M,    20M,    10M,    5M,     1M,    0.50M,   0.25M,  0.10M,  0.05M,  0.01M},
                        {  0M,   0M,     0M,     0M,    0M,     0M,     0M,     0M,     0M,     0M,        0M}
                    };
                    #endregion

                    #region //Calculate denominations due
                    while (change > 0)
                    {
                        for (index = 0; index <= 10;) 
                        {
                            //Check to see if change is more than or equal to the denomination at
                            if (change >= storage_shelf[0,index]) 
                            {
                                if (index < 6)
                                {
                                    updateCounts = (int)change / (int)storage_shelf[0, index];
                                    //Update [counts] which is stored in second row of 2D-array 
                                    storage_shelf[1, index] = updateCounts;
                                    change -= (storage_shelf[0, index] * updateCounts);
                                    index++;
                                }
                                else if (index >= 6)
                                {
                                    placeHolder = (decimal)change / (decimal)storage_shelf[0, index];
                                    //Update [counts] which is stored in second row of 2D-array 
                                    updateCounts = (int)placeHolder;
                                    storage_shelf[1, index] = updateCounts;
                                    change -= (storage_shelf[0, index] * updateCounts);
                                    index++;
                                }
                            }
                            else 
                            {
                                index++;
                            }                          
                        }                    
                    }
                    #endregion

                    #region//Print out the change and denominations due
                    if (change == 0)
                    {
                        //Make output for each method yellow
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        index = 0;
                        decimal denom;
                        Console.WriteLine($"The following change is due: {change_cpy:c}");

                        for (; index <= 10;)
                        {
                            if (storage_shelf[1, index] > 0) 
                            {
                                denom = storage_shelf[0, index];
                                switch (denom)
                                {
                                    case 100M:
                                        string val_0 = "$100.00";
                                        Console.WriteLine($"{val_0.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 50M:
                                        string val_1 = "$50.00";
                                        Console.WriteLine($"{val_1.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 20M:
                                        string val_2 = "$20.00";
                                        Console.WriteLine($"{val_2.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 10M:
                                        string val_3 = "$10.00";
                                        Console.WriteLine($"{val_3.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 5M:
                                        string val_4 = "$5.00";
                                        Console.WriteLine($"{val_4.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 1M:
                                        string val_5 = "$1.00";
                                        Console.WriteLine($"{val_5.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 0.50M:
                                        string val_6 = "$0.50";
                                        Console.WriteLine($"{val_6.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 0.25M:
                                        string val_7 = "$0.25";
                                        Console.WriteLine($"{val_7.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 0.10M:
                                        string val_8 = "$0.10";
                                        Console.WriteLine($"{val_8.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 0.05M:
                                        string val_9 = "$0.05";
                                        Console.WriteLine($"{val_9.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                    case 0.01M:
                                        string val_10 = "$0.01";
                                        Console.WriteLine($"{val_10.PadLeft(10, ' ')}: {storage_shelf[1, index], 5}");
                                        index++;
                                        continue;
                                }
                            } 
                            else 
                            {
                                index++;
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion
        }

        private static void CountingLetters()
        {
            #region //Variables
            string input = string.Empty;
            char[] vowels = { 'A', 'E', 'I','O','U' };
            bool isVowel = false;

            //categoryCounts[vals in first row] where 'vals' represents: 
            /*'0 - consonants'
             *'1 - vowels'
             *'2 - digits' 
             *'3 - punctuation'
             *'4 - symbols' 
             *'5 - whitespace' 
             *'6 - unknown chars' 
             */
            int[,] categoryCounts = new int[2, 7]
                {
                    { 0,  1,  2,  3,  4,  5,  6},
                    { 0,  0,  0,  0,  0,  0,  0}
                };
            #endregion

            //Make output for each method yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter text to analyze: ");
            Console.ForegroundColor = ConsoleColor.Green;
            input = Console.ReadLine(); 

            //If valid data, iterate through every char in 'input' and update it's respective [count]
            if (!string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                int strLength = input.Length;
                int i;

                //Iterate through every char in the string to determine it's 'category' and update counts
                for (i = 0; i <= strLength - 1;)
                {
                    char itemChar = (char)input[i];

                    //Condition to determine whether itemChar is a letter and if so, is it a vowel? consonant?
                    if (char.IsLetter(itemChar))
                    {
                        //Reset the 'isVowel' flag to 'false'
                        isVowel = false;

                        foreach (char ltr in vowels)
                        {
                            if (char.ToUpper(itemChar) == ltr)
                            {
                                //Set 'isVowel' flag to 'true'
                                isVowel = true;
                                //Increment vowels by '1'
                                categoryCounts[1, 1]++;
                                i++;
                                //This 'break' exits out of the 'foreach' loop
                                break;
                            }

                        }
                        if (isVowel == false) 
                        {
                            //Increment consonants by '1'
                            categoryCounts[1, 0]++;
                            i++;
                            //This 'continue' executes the next iteration of the 'for' loop
                            //Essentially, this 'continue' tells the prog. to evaluate the next 'char' in input
                            continue;                     
                        }
                        //This 'continue' short-circuits the 'if..else' and executes the next iteration of the 'for' loop
                        //Without this 'continue', the remaining 6 conditions would be evaluated which
                        //isn't necessary since it's already been determined that 'itemChar' is a letter
                        continue;
            
                    }
                    //Condition to determine whether itemChar is number
                    else if (char.IsNumber(itemChar))
                    {
                        //Increment number by '1'
                        categoryCounts[1, 2]++;
                        i++;
                        continue;
                    }
                    //Condition to determine whether itemChar is punctuation
                    else if (char.IsPunctuation(itemChar))
                    {
                        //Increment punctuation by '1'
                        categoryCounts[1, 3]++;
                        i++;
                        continue;
                    }
                    //Condition to determine whether itemChar is a symbol
                    else if (char.IsSymbol(itemChar))
                    {
                        //Increment symbol by '1'
                        categoryCounts[1, 4]++;
                        i++;
                        continue;
                    }
                    //Condition to determine whether itemChar is whitespace
                    else if (char.IsWhiteSpace(itemChar))
                    {
                        //Increment punctuation by '1'
                        categoryCounts[1, 5]++;
                        i++;
                        continue;
                    }
                    //Condition to determine whether itemChar is unknown characters
                    else
                    {
                        //Increment unknown characters by '1'
                        categoryCounts[1, 6]++;
                        i++;
                        continue;
                    }
                }

                #region //Print out the counts for each category
                if (i == strLength )
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"The text contains {strLength} characters with the following characteristics: ");
                    for (int y = 0; y <= 6;)
                    {
                        switch (y)
                        {
                            case 0:
                                Console.WriteLine($"{categoryCounts[1, y], 4} consonants");
                                break;
                            case 1:
                                Console.WriteLine($"{categoryCounts[1, y], 4} vowels");
                                break;
                            case 2:
                                Console.WriteLine($"{categoryCounts[1, y], 4} digits");
                                break;
                            case 3:
                                Console.WriteLine($"{categoryCounts[1, y], 4} punctuation");
                                break;
                            case 4:
                                Console.WriteLine($"{categoryCounts[1, y], 4} symbols");
                                break;
                            case 5:
                                Console.WriteLine($"{categoryCounts[1, y], 4} whitespace");
                                break;
                            case 6:
                                Console.WriteLine($"{categoryCounts[1, y], 4} unknown characters");
                                break;
                        }
                        //Increment the iterator
                        y++;
                    }
                }
                #endregion
            }
        }

        private static void NumberGuessingGame() 
        {
            #region Variables
            Random rdn = new Random();
            int target;
            int guess = 0;
            string guessStr = default;
            int count = 0;
            #endregion

            #region //Set the value of 'target'
            target = rdn.Next(100) + 1;
            #endregion

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter a number between 1 and 100: ");
                Console.ForegroundColor = ConsoleColor.Green;
                guessStr = Console.ReadLine();
                guessStr = guessStr.Trim(); 
                if ( !string.IsNullOrEmpty(guessStr) && int.TryParse(guessStr,out guess))
                {
                    //If the 'guess' is not within the specified range, ask the user for a valid 'guess'
                    if (guess> 100 || guess <= 0)
                    {
                        continue;
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    //Increment count by '1' for each new 'valid' guessInt
                    count++;
                    if (guess == target) 
                    {
                        Console.WriteLine($"You got it in {count} guesses!");
                    }
                    else if (guess > target) 
                    {
                        Console.WriteLine("Too high.");
                    }
                    else if (guess < target) 
                    {
                        Console.WriteLine("Too low.");
                    }
                }

            }while (guess != target);
        }

    }
}
