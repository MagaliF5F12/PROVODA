using System.ComponentModel;

namespace practa7daga
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDir = "Выбор диска";
            Cursor cursor = new Cursor(pos: 1, min: 1);
            bool working = true;
            List<Component> components = new List<Component>();
            while (working)
            {
                if (currentDir == "Выбор диска")
                    components = Files.getDrivers();
                else if (currentDir == "\\")
                {
                    components = Files.getDrivers();
                    currentDir = "Выбор диска";
                }
                else
                {
                    try
                    {
                        components = Files.GetFiles(currentDir);
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Error while reading files");
                        Thread.Sleep(5000);
                    }
                }
                Console.WriteLine(currentDir);
                cursor.max = components.Count;
                Menu.ShowComponents(components);
                cursor.ShowCursor();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor.ArrowUp();
                        break;
                    case ConsoleKey.DownArrow:
                        cursor.ArrowDown();
                        break;
                    case ConsoleKey.Enter:
                        Component component = components[cursor.pos - cursor.min];
                        if (component.type == "dir" | component.type == "drive")
                            currentDir = component.path;
                        else
                            Files.OpenFile(component.path);
                        Console.Clear();
                        cursor.pos = 1;
                        break;
                    case ConsoleKey.Escape:
                        working = false;
                        break;
                }
            }
        }
    }
}