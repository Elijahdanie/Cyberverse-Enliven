

using Cyberverse.Peripherals.Data;
using UnityEngine;

namespace Cyberverse.Peripherals
{
    public class PeripheralBehavour<T> : MonoBehaviour where T : IPeripheral
    {
        public bool isOn => data.isOn;
        [SerializeField] public T data;
        public virtual void Init(T data)
        {
            gameObject.SetActive(data.isOn);
        }

        public virtual void On()
        {
            gameObject.SetActive(true);
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
            gameObject.SetActive(false);
            data.isOn = false;
        }
    }
}
