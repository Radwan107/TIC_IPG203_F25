using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter
{
    public interface IDevice
    {
        string SerialNumber { get; }
        string CustomerName { get; }
        string CustomerPhone { get; }
        bool IsRepaired { get; }
        double RepairCost { get; }

        double CalculateRepairCost();
        string GetDeviceType();
    }
}
