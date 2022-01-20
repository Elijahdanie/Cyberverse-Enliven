

using Cyberverse.EventSystem;
using Cyberverse.Interactables;
using Cyberverse.Interactables.UI;
using Cyberverse.Users;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cyberverse.Audio_Objects
{
    /// <summary>
    /// This object enables interaction
    /// between user and audio files
    /// </summary>
    public class MusicFileBehaviour : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// The audio file to play
        /// </summary>
        public IMusicFile musicFile;

        public CyberMp3 mp3;

        public bool isLockedOn { get; set; }

        public IDisplay display { get; }

        IUser user;

        public Sprite icon;

        private void Start()
        {
            musicFile = mp3;
        }

        public void Exit()
        {

        }

        public void Interact(IUser user)
        {
            this.user = user;
        }

        public DisplayInfo Format()
        {
            string formatdata = $"Author: {musicFile.metaData.Owner}";
            formatdata += $"Length: {musicFile.metaData.Length}";
            formatdata += $"fileSize: {musicFile.metaData.fileSize}";
            formatdata += $"created Date: {musicFile.metaData.createdDate}";
            return new DisplayInfo()
            {
                FormattedText = formatdata,
                Title = $"{musicFile.metaData.Title}",
                functions = new List<CyberbuttonData>() {
                    new CyberbuttonData(){
                        functionDescription = "Play a song",
                        Icon = this.icon,
                        action = ()=>{
                            (this.user as UserClient).speakerBehaviour.Play(musicFile);
                        }
                    },
                }
            };
        }
    }

    [System.Serializable]
    public class AudioMetaData {
        public string Title;
        public string Owner;
        public string createdDate;
        public string Length;
        public string fileSize;
        public string CoverPhotoUrl;
    }
}
