using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberverse.Audio_Objects
{
    public class MusicFileUtility
    {
    }


    public interface ISoundInitAgent
    {
        bool isPlaying { get; }
        void Play(IMusicFile musicFile);
        void Pause();
        void Stop();

    }

    public interface IMusicFile
    {
        string streamURL { get; }
        bool isDownloadable { get; }
        string DownloadURL { get; }
        string localFilecache { get; }
        AudioClip clip { get; }
        AudioMetaData metaData { get; }
    }

    [System.Serializable]
    public struct CyberMp3 : IMusicFile
    {
        public string _streamURL;

        public bool _isDownloadable;

        public string _DownloadURL;

        public string _localFilecache;

        public AudioClip _clip;
        public AudioMetaData _metaData;

        public string streamURL { get=>_streamURL; }

        public bool isDownloadable { get =>_isDownloadable; }

        public string DownloadURL { get =>_DownloadURL; }

        public string localFilecache { get =>_localFilecache; }

        public AudioClip clip { get =>_clip; }

        public AudioMetaData metaData => _metaData;
    }
}
