using Cyberverse.Peripherals.Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Cyberverse.Peripherals
{
    [RequireComponent(typeof(AudioSource))]
    public class MicBehaviour : PeripheralBehavour<Mic>
    {
        [SerializeField]
        private AudioSource _audioSource;
        public Toggle tog;
        public string selectedDevice;

        public override void Init(Mic data)
        {
            base.Init(data);
            if (Microphone.devices.Length > 0)
            {
                selectedDevice = Microphone.devices[data.deviceId].ToString();
                _audioSource.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate);
                _audioSource.Play();
            }
            else
            {
                Off();
            }
        }

        public override void On()
        {
            _audioSource.enabled = true;
            base.On();
        }

        public override void Off()
        {
            base.Off();
            _audioSource.enabled = false;
        }
    }
}
