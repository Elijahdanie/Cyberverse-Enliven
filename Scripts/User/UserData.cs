

using Cyberverse.Audio_Objects;
using Cyberverse.Interactables;
using Cyberverse.Peripherals.Data;
using UnityEngine;

namespace Cyberverse.Users
{

    public interface IUser
    {
        void EngageInteractable();
    }

    public interface IStorable
    {
        void Save();
        void Load();
        void Delete();
    }

    [System.Serializable]
    public struct UserData : IStorable
    {
        public string uuid;
        public Mic mic;
        public Speaker speaker;
        public float moveSpeed;
        public float RotateSpeed;

        public void Delete()
        {

        }

        public void Load()
        {

        }

        public void Save()
        {

        }
    }
}
