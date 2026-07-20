# Bereza Shop

> Console application for managing a shop bill — KhAI summer practice, group **611п**.

Repository: [GITHUB_USERNAME/BerezaShop](https://github.com/GITHUB_USERNAME/BerezaShop)

## Features

| # | Menu item | Validation |
|---|-----------|------------|
| 1 | Add Item | description 3–20 chars, price > 0, max **5** items |
| 2 | Remove Item | number within range, `0` to cancel |
| 3 | Add Tip | percentage, fixed amount, or no tip |
| 4 | Display Bill | Net, Tip, GST (5%), Total |
| 5 | Clear All | resets items and tip |
| 6 | Save to file | file name 1–30 chars |
| 7 | Load from file | restores items **and** tip; missing file → message |

The application **never crashes** on invalid input — every error produces
a clear text message (`decimal.TryParse` / `int.TryParse` / `try-catch` on every level, including a top-level guard around the whole program).

Decimal input accepts both `.` and `,` as the fractional separator.

## Architecture

Single-file console application (`Program.cs`, top-level statements):

- `RunApplication()` — main menu loop and `switch` dispatch (kept minimal in `Main`)
- `AddItem`, `RemoveItem`, `AddTip`, `DisplayBill`, `ClearAll`, `SaveToFile`, `LoadFromFile` — one function per menu action
- `GetStringInput`, `GetDecimalInput`, `GetIntInput` — generic, reusable input-validation helpers independent of the bill domain
- `MenuItem` — simple data class (`Description`, `Price`)

Two layers of exception handling: an outer `try/catch` around the whole application (last-resort guard against any unhandled exception) and an inner `try/catch` inside the menu loop (keeps the program running after a runtime error in a single action).

## How to run

```bash
dotnet run
```

Example session:

```text
Enter your choice: 1
Enter description: Ceramic Mug
Enter price: 90.00
Add item was successful.
```

## Git workflow

- [x] main → develop → my-feature branching (GitFlow)
- [x] one commit per implemented menu item
- [x] merge & branch cleanup, clone & pull demonstrated

## Author

**Bereza Vladyslav Olehovych**, group 611п, KhAI
