using System.Text;
using BurrowsWheelerTransform;

Options choice = Options.Transform;
Console.OutputEncoding = Encoding.UTF8;

while (choice != Options.Exit)
{
    Console.WriteLine("Выберите действие:");
    Console.WriteLine("0 - Выход");
    Console.WriteLine("1 - Преобразовать строку по Барроузу-Уилеру");
    Console.WriteLine("2 - Восстановить строку по Барроузу-Уилеру");
    string? input = Console.ReadLine();

    if (input == null)
    {
        Console.WriteLine("Некорректный ввод!");
        continue;
    }

    try
    {
        choice = (Options)int.Parse(input);
    }
    catch (FormatException)
    {
        Console.WriteLine("Некорректный ввод!");
        continue;
    }

    if (choice == Options.Exit)
    {
        return;
    }

    if (choice == Options.Transform)
    {
        Console.WriteLine("Введите строку, которую хотите преобразовать");
        string? str = Console.ReadLine();
        if (str == null || str == string.Empty)
        {
            Console.WriteLine("Некорректный ввод!");
            continue;
        }

        var (transformed, position) = BurrowsWheeller.Transform(str);
        Console.WriteLine($"Строка: {transformed}\nПозиция, в которой стоит исходная строка {position}");
    }

    if (choice == Options.Invert)
    {
        Console.WriteLine("Введите строку, которую нужно вернуть к исходному виду");
        string? str = Console.ReadLine();
        if (str == null || str == string.Empty)
        {
            Console.WriteLine("Некорректный ввод!");
            continue;
        }

        Console.WriteLine("Введите позицию, в которой стояла строка");
        string? positionStr = Console.ReadLine();
        if (positionStr == null || positionStr == string.Empty)
        {
            Console.WriteLine("Некорректный ввод!");
            continue;
        }

        try
        {
            int position = int.Parse(positionStr);
            string result = BurrowsWheeller.InverseTransform(str, position);
            Console.WriteLine(result);
        }
        catch(FormatException)
        {
            Console.WriteLine("Некорректный ввод!");
        }
    }
}