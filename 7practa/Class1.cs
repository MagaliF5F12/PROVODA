using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace practa7daga
{
    public class Component
    {
        public Component(string name, string type, string path)
        {
            this.name = name;
            this.path = path;
            this.type = type;
        }
        public string name;
        public string type;
        public string path;
    }
    public static class Menu
    {
        public static void ShowComponents(List<Component> components)
        {
            foreach (Component component in components)
                Console.WriteLine("   " + component.name);
        }
    }
    public class Cursor
    {
        public int max, min, pos;
        public Cursor(int max = 0, int min = 0, int pos = 0)
        {
            this.max = max;
            this.min = min;
            this.pos = pos;
        }
        public void ArrowUp()
        {
            ClearCursor();
            if (pos == min)
                pos = max;
            else
                pos--;
            ShowCursor();
        }
        public void ArrowDown()
        {
            ClearCursor();
            if (pos == max)
                pos = min;
            else
                pos++;
            ShowCursor();
        }
        public void ClearCursor()
        {
            Console.Write("  ");
        }
        public void ShowCursor()
        {
            Console.SetCursorPosition(0, pos);
            Console.Write("->");
            Console.SetCursorPosition(0, 0);
        }
    }
    public static class Files
    {
        public static void OpenFile(string path)
        {
            try
            {
                Process.Start(path);
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Error while opening file");
                Thread.Sleep(5000);
            }

        }
        private static double PrettyBytes(long bytes)
        {
            return (bytes / 1);
        }
        public static List<Component> getDrivers()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<Component> drivers = new List<Component>();
            foreach (DriveInfo drive in allDrives)
            {
                double totalSpace = PrettyBytes(drive.TotalSize);
                double availableSpace = PrettyBytes(drive.AvailableFreeSpace);
                Component elem = new Component($"Диск {drive.Name} | Размер {totalSpace} гб | Доступно {availableSpace} гб", "drive", drive.Name);
                drivers.Add(elem);
            }
            return drivers;
        }
        public static List<Component> GetFiles(string path)
        {
            List<Component> elems = new List<Component>();
            DirectoryInfo parent = Directory.GetParent(path);
            if (parent != null)
                elems.Add(new Component("На уровень выше", "dir", parent.FullName));
            else
                elems.Add(new Component("К выбору диска", "drive", "\\"));
            List<Component> elements = new List<Component>();

            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            foreach (string dir in dirs)
            {
                Component elem = new Component(dir.Split("\\")[^1] + '\\', "dir", dir);
                elems.Add(elem);
            }
            foreach (string file in files)
            {
                Component elem = new Component(file.Split("\\")[^1], "file", file);
                elems.Add(elem);
            }
            return elems;
        }
    }
}