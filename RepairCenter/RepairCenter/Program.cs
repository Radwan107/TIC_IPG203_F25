using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RepairCenter
{
    class Program
    {
        static List<Device> devices = new List<Device>();

        static void Main(string[] args)
        {
            bool running = true;

            while (running == true)
            {
                DrawMenu();

                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "1")
                {
                    ShowRepairingDevices();
                }
                else if (choice == "2")
                {
                    ShowRepairedDevices();
                }
                else if (choice == "3")
                {
                    ShowAllDevices();
                }
                else if (choice == "4")
                {
                    AddDevice();
                }
                else if (choice == "5")
                {
                    ChangeDeviceStatus();
                }
                else if (choice == "6")
                {
                    ShowStatistics();
                }
                else if (choice == "7")
                {
                    running = false;
                }
                else
                {
                    PrintColor("Invalid choice!", ConsoleColor.Red);
                }

                if (running == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                }
            }
        }

        static void DrawMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("~_~_~__~_~_~_~~~_~___~_~_~__~_~_~_~~~_~___~_~_~__~_~_~_~~~_~___");
            Console.WriteLine();
            Console.WriteLine("       HOME APPLIANCE REPAIR CENTER SYSTEM        ");

            Console.WriteLine();
            Console.WriteLine("   [1] Show Devices Under Repair");
            Console.WriteLine("   [2] Show Repaired Devices");
            Console.WriteLine("   [3] Show All Devices");
            Console.WriteLine("   [4] Add New Device");
            Console.WriteLine("   [5] Change Device Status");
            Console.WriteLine("   [6] Show Statistics");
            Console.WriteLine("   [7] Exit");
            Console.WriteLine();
            Console.WriteLine("~_~_~__~_~_~_~~~_~___~_~_~__~_~_~_~~~_~___~_~_~__~_~_~_~~~_~___");

            Console.ResetColor();
            Console.Write("Choose an option: ");
        }

        static void ShowRepairingDevices()
        {
            PrintHeader("DEVICES UNDER REPAIR");

            List<Device> list = new List<Device>();

            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].IsRepaired == false)
                    list.Add(devices[i]);
            }

            if (list.Count == 0)
            {
                PrintColor("No devices under repair.", ConsoleColor.Yellow);
                return;
            }

            PrintDeviceTable(list);
        }

        static void ShowRepairedDevices()
        {
            PrintHeader("REPAIRED DEVICES");

            List<Device> list = new List<Device>();

            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].IsRepaired == true)
                    list.Add(devices[i]);
            }

            if (list.Count == 0)
            {
                PrintColor("No repaired devices yet.", ConsoleColor.Yellow);
                return;
            }

            PrintDeviceTable(list);
        }

        static void ShowAllDevices()
        {
            PrintHeader("ALL DEVICES");

            if (devices.Count == 0)
            {
                PrintColor("No devices registered.", ConsoleColor.Yellow);
                return;
            }

            PrintDeviceTable(devices);
        }

        static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("~_~_~__~_~_~_~~~_~___~_~_~__~_~_~_~~~_~___~_~_~__~_~_~_~~~_~___");
            Console.WriteLine("   " + title);
            Console.WriteLine("~_~_~__~_~_~_~~~_~___~_~_~__~_~_~_~~~_~___~_~_~__~_~_~_~~~_~___");
            Console.ResetColor();
        }

        static void PrintDeviceTable(List<Device> list)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.WriteLine("Serial     | Type             | Customer       | Phone        | Cost     | Status");
            Console.WriteLine("--------------------------------------------------------------------------------------");

            for (int i = 0; i < list.Count; i++)
            {
                Device d = list[i];
                string status = "In Repair";

                if (d.IsRepaired == true)
                    status = "Repaired";

                Console.ForegroundColor = d.IsRepaired ? ConsoleColor.Green : ConsoleColor.Yellow;

                string cost = d.CalculateRepairCost().ToString("F2");

                Console.WriteLine(
                    Pad(d.SerialNumber, 10) + " | " +
                    Pad(d.GetDeviceType(), 16) + " | " +
                    Pad(d.CustomerName, 14) + " | " +
                    Pad(d.CustomerPhone, 12) + " | " +
                    Pad(cost, 8) + " | " +
                    status
                );
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.ResetColor();
        }

        static string Pad(string text, int length)
        {
            if (text == null) text = "";
            if (text.Length > length) text = text.Substring(0, length);
            while (text.Length < length) text = text + " ";
            return text;
        }

        static void AddDevice()
        {
            PrintHeader("ADD NEW DEVICE");

            Console.WriteLine("Select device type:");
            Console.WriteLine("  [1] Refrigerator");
            Console.WriteLine("  [2] Washing Machine");
            Console.WriteLine("  [3] Air Conditioner");
            Console.Write("Choice: ");
            string type = Console.ReadLine();

            if (type != "1" && type != "2" && type != "3")
            {
                PrintColor("Invalid device type!", ConsoleColor.Red);
                return;
            }

            Console.Write("Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Customer Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Base Repair Cost: ");
            string costStr = Console.ReadLine();

            double cost;

            if (double.TryParse(costStr, out cost) == false)
            {
                PrintColor("Invalid cost!", ConsoleColor.Red);
                return;
            }

            Device newDevice = null;

            try
            {
                if (type == "1")
                    newDevice = new Refrigerator(name, phone, cost);
                else if (type == "2")
                    newDevice = new WashingMachine(name, phone, cost);
                else if (type == "3")
                    newDevice = new AirConditioner(name, phone, cost);
            }
            catch (ArgumentException ex)
            {
                PrintColor("Error: " + ex.Message, ConsoleColor.Red);
                return;
            }

            if (newDevice == null)
            {
                PrintColor("Could not create device.", ConsoleColor.Red);
                return;
            }

            newDevice.StatusChanged += OnDeviceStatusChanged;

            devices.Add(newDevice);
            PrintColor("Device added successfully. Serial: " + newDevice.SerialNumber, ConsoleColor.Green);
        }

        static void OnDeviceStatusChanged(string serialNumber, string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine();
            Console.WriteLine("  >> EVENT: [" + serialNumber + "] " + message);
            Console.ResetColor();
        }

        static void ChangeDeviceStatus()
        {
            PrintHeader("CHANGE DEVICE STATUS");

            if (devices.Count == 0)
            {
                PrintColor("No devices available.", ConsoleColor.Yellow);
                return;
            }

            PrintDeviceTable(devices);

            Console.WriteLine();
            Console.Write("Enter Serial Number (example: DEV-0001): ");
            string serial = Console.ReadLine();

            Device device = null;

            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].SerialNumber == serial)
                {
                    device = devices[i];
                    break;
                }
            }

            if (device == null)
            {
                PrintColor("Device not found!", ConsoleColor.Red);
                return;
            }

            if (device.IsRepaired == true)
            {
                PrintColor("This device is already repaired.", ConsoleColor.Yellow);
                return;
            }

            device.MarkAsRepaired();
            PrintColor("Device status changed to Repaired.", ConsoleColor.Green);
        }

        static void ShowStatistics()
        {
            PrintHeader("STATISTICS");

            int total = devices.Count;
            int repaired = 0;

            for (int i = 0; i < devices.Count; i++)
            {
                if (devices[i].IsRepaired == true)
                    repaired = repaired + 1;
            }

            int repairing = total - repaired;

            double totalCost = 0;

            for (int i = 0; i < devices.Count; i++)
            {
                totalCost = totalCost + devices[i].CalculateRepairCost();
            }

            Console.WriteLine("  Total Devices        : " + total);
            Console.WriteLine("  Devices Under Repair : " + repairing);
            Console.WriteLine("  Repaired Devices     : " + repaired);
            Console.WriteLine("  Total Repair Cost    : " + totalCost.ToString("F2"));
        }

        static void PrintColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}