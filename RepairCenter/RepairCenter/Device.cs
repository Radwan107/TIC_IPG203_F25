using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepairCenter
{
    public abstract class Device : IDevice
    {
        // Delegate + Event inside the class
        public delegate void StatusChangedHandler(string serialNumber, string message);
        public event StatusChangedHandler StatusChanged;

        private static int counter = 0;
        private static double totalCost = 0;

        private string serialNumber;
        private string customerName;
        private string customerPhone;
        private double repairCost;
        private bool isRepaired;

        protected Device(string name, string phone, double cost)
        {
            if (Validator.IsValidName(name) == false)
                throw new ArgumentException("Invalid customer name");

            if (Validator.IsValidPhone(phone) == false)
                throw new ArgumentException("Invalid phone number");

            if (Validator.IsValidCost(cost) == false)
                throw new ArgumentException("Invalid cost");

            counter = counter + 1;
            serialNumber = "DEV-" + counter.ToString("D4");
            customerName = name;
            customerPhone = phone;
            repairCost = cost;
            isRepaired = false;

            totalCost = totalCost + cost;
        }

        // Read-only properties
        public string SerialNumber
        {
            get { return serialNumber; }
        }

        public bool IsRepaired
        {
            get { return isRepaired; }
        }

        // Editable properties with validation
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                if (Validator.IsValidName(value) == false)
                    throw new ArgumentException("Invalid customer name");
                customerName = value;
            }
        }




        public string CustomerPhone
        {
            get { return customerPhone; }
            set
            {
                if (Validator.IsValidPhone(value) == false)
                    throw new ArgumentException("Invalid phone number");
                customerPhone = value;
            }
        }

        public double RepairCost
        {
            get { return repairCost; }
            set
            {
                if (Validator.IsValidCost(value) == false)
                    throw new ArgumentException("Invalid cost");
                repairCost = value;
            }
        }

        // Static properties
        public static int TotalDevices
        {
            get { return counter; }
        }

        public static double TotalCost
        {
            get { return totalCost; }
        }

        // Abstract methods
        public abstract double CalculateRepairCost();
        public abstract string GetDeviceType();

        public void MarkAsRepaired()
        {
            if (isRepaired == true) return;

            isRepaired = true;

            if (StatusChanged != null)
                StatusChanged(serialNumber, "Device repaired successfully");
        }
    }







}
