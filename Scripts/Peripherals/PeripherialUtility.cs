using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberverse.Peripherals.Data
{
    public interface IPeripheral
    {
        bool isOn { get; set; }
    }

    [System.Serializable]
    public struct Mic : IPeripheral
    {
        public int Id;
        public bool isOn;
        public int deviceId;

        bool IPeripheral.isOn { get => isOn; set => isOn = value; }
    }

    [System.Serializable]
    public struct Speaker : IPeripheral
    {
        public int Id;
        public bool isOn;
        public float volume;

        bool IPeripheral.isOn { get => isOn; set => isOn = value; }
    }
}
