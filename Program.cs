using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

try
{
    RunApplication();
}
catch (Exception ex)
{
    Console.WriteLine($"\n[КРИТИЧНА ПОМИЛКА]: {ex.Message}");
    Console.WriteLine("Програму буде безпечно завершено. Натисніть будь-яку клавішу...");
    Console.ReadKey();
}   

static void RunApplication()
{
    List<MenuItem> items = new List<MenuItem>();
    decimal tipAmount = 0m;
    bool isRunning = true;
    while (isRunning)
{
    try
    {
        DisplayMainMenu();
        int choice = GetIntInput("Enter your choice: ", 0, 7);
        Console.WriteLine();

        switch (choice)
        {
            case 0:
                Console.WriteLine("Good-bye and thanks for using this program.");
                isRunning = false;
                break;
        }
        if (isRunning) Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[ПОМИЛКА ВИКОНАННЯ]: {ex.Message}. Спробуйте ще раз.");
    }
}
}
static void DisplayMainMenu()
{
    Console.WriteLine(" __________________________ ");
    Console.WriteLine("|                          |");
    Console.WriteLine("| |     Bereza Shop      | |");
    Console.WriteLine("| | -------------------- | |");
    Console.WriteLine("| | 1. Add Item          | |");
    Console.WriteLine("| | 2. Remove Item       | |");
    Console.WriteLine("| | 3. Add Tip           | |");
    Console.WriteLine("| | 4. Display Bill      | |");
    Console.WriteLine("| | 5. Clear All         | |");
    Console.WriteLine("| | 6. Save to file      | |");
    Console.WriteLine("| | 7. Load from file    | |");
    Console.WriteLine("| | 0. Exit              | |");
    Console.WriteLine("| |______________________| |");
    Console.WriteLine("|__________________________|");
}

static string GetStringInput(string prompt, int minLength, int maxLength)
{
    while (true)
    {
        try
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (input != null)
            {
                input = input.Trim();
                if (input.Length >= minLength && input.Length <= maxLength)
                {
                    return input;
                }
            }
            Console.WriteLine($"Invalid input. Please enter between {minLength} and {maxLength} characters.");
        }
        catch (Exception)
        {
            Console.WriteLine("Помилка вводу. Будь ласка, повторіть спробу.");
        }
    }
}

static decimal GetDecimalInput(string prompt, decimal minValue)
{
    while (true)
    {
        try
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (decimal.TryParse(input?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result) && result >= minValue)
            {
                return result;
            }
            Console.WriteLine($"Invalid input. Please enter a valid positive number >= {minValue.ToString(CultureInfo.InvariantCulture)}.");
        }
        catch (Exception)
        {
            Console.WriteLine("Помилка вводу числа. Будь ласка, повторіть спробу.");
        }
    }
}

static int GetIntInput(string prompt, int minValue, int maxValue)
{
    while (true)
    {
        try
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result) && result >= minValue && result <= maxValue)
            {
                return result;
            }
            Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
        }
        catch (Exception)
        {
            Console.WriteLine("Помилка вводу цілого числа. Будь ласка, повторіть спробу.");
        }
    }
}

class MenuItem
{
    public string Description { get; set; }
    public decimal Price { get; set; }
}