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
            case 1:
                AddItem(items);
                break;
            case 2:
                RemoveItem(items, ref tipAmount);
                break;
            case 4:
                DisplayBill(items, tipAmount);
                break;
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

static void AddItem(List<MenuItem> items)
{
    if (items.Count >= 5)
    {
        Console.WriteLine("Maximum number of items (5) reached.");
        return;
    }

    string description = GetStringInput("Enter description: ", 3, 20);
    decimal price = GetDecimalInput("Enter price: ", 0.01m);

    items.Add(new MenuItem { Description = description, Price = price });
    Console.WriteLine("Add item was successful.");
}

static void RemoveItem(List<MenuItem> items, ref decimal tipAmount)
{
    if (items.Count == 0)
    {
        Console.WriteLine("There are no items to remove.");
        return;
    }

    Console.WriteLine($"{"ItemNo",-6} {"Description",-20} {"Price",8}");
    Console.WriteLine($"{"------",-6} {"--------------------",-20} {"--------",8}");
    for (int i = 0; i < items.Count; i++)
    {
        Console.WriteLine($"{i + 1,6} {items[i].Description,-20} ${items[i].Price,7:F2}");
    }

    int itemNo = GetIntInput("Enter the item number to remove or 0 to cancel: ", 0, items.Count);
    if (itemNo > 0)
    {
        items.RemoveAt(itemNo - 1);
        Console.WriteLine("Remove item was successful.");
        if (items.Count == 0) tipAmount = 0m;
    }
}

static void DisplayBill(List<MenuItem> items, decimal tipAmount)
{
    if (items.Count == 0)
    {
        Console.WriteLine("There are no items in the bill to display.");
        return;
    }

    Console.WriteLine($"{"Description",-20} {"Price",8}");
    Console.WriteLine($"{"--------------------",-20} {"--------",8}");
    foreach (var item in items)
    {
        Console.WriteLine($"{item.Description,-20} ${item.Price,7:F2}");
    }
    Console.WriteLine($"{"--------------------",-20} {"--------",8}");

    decimal netTotal = items.Sum(i => i.Price);
    decimal gstAmount = netTotal * 0.05m;
    decimal totalAmount = netTotal + tipAmount + gstAmount;

    Console.WriteLine($"{"Net Total",20} ${netTotal,7:F2}");
    Console.WriteLine($"{"Tip Amount",20} ${tipAmount,7:F2}");
    Console.WriteLine($"{"GST Amount",20} ${gstAmount,7:F2}");
    Console.WriteLine($"{"Total Amount",20} ${totalAmount,7:F2}");
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