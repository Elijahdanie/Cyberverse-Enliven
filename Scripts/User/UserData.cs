


using Cyberverse.AvatarConfiguration.Utility;
using Cyberverse.Peripherals.Data;

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
    public class UserData : IStorable
    {
        public string uuid;
        public Mic mic;
        public Speaker speaker;
        public AvatarConfig avatar;
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
