

using Cyberverse.Peripherals.Data;
using UnityEngine;

namespace Cyberverse.Peripherals
{
    public class PeripheralBehavour<T> : MonoBehaviour where T : IPeripheral
    {
        public Material micMat;
        public Color isOnColor;
        public Color isOffColor;
        public bool isOn => data.isOn;
        [SerializeField] public T data;
        public virtual void Init(T data)
        {
            micMat.color = data.isOn ? isOnColor : isOffColor;
        }

        public virtual void On()
        {
            micMat.color = isOnColor;
            data.isOn = true;
        }

        public void Tog()
        {
            if (data.isOn)
            {
                Off();
            }
            else
            {
                On();
            }
        }

        public virtual void Off()
        {
            micMat.color = isOffColor;
            data.isOn = false;
        }
    }
}
