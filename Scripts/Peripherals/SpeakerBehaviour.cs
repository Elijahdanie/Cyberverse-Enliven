
using Cyberverse.Audio_Objects;
using Cyberverse.Peripherals.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cyberverse.Peripherals
{
    [RequireComponent(typeof(AudioSource))]
    public class SpeakerBehaviour : PeripheralBehavour<Speaker>, ISoundInitAgent
    {
        float prevValue = 0f;
        public AudioSource _speaker;
        public bool isPlaying { get; set; }

        public override void On()
        {
            prevValue = AudioListener.volume;
            AudioListener.volume = 0f;
            base.On();
        }

        public override void Init(Speaker data)
        {
            _speaker = GetComponent<AudioSource>();
            //AudioListener.volume = data.volume;
            //if (data.isOn)
            //{
            //    On();
            //}
            //else
            //{
            //    Off();
            //}
            //base.Init(data);
        }

        public override void Off()
        {
            AudioListener.volume = prevValue;
            base.Off();
        }

        public void Play(IMusicFile musicFile)
        {
            _speaker.clip = musicFile.clip;
            _speaker.Play();
        }

        public void Pause()
        {

        }

        public void Stop()
        {

        }
    }

}
