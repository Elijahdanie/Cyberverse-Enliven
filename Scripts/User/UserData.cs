


using Cyberverse.AvatarConfiguration.Utility;
using Cyberverse.Peripherals.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Cyberverse.Users
{

    public interface IUser
    {
        void EngageInteractable();
    }


    [System.Serializable]
    public class UserData
    {
        public string uuid;
        public Mic mic;
        public Speaker speaker;
        public AvatarConfig avatar;
        public float moveSpeed;
        public float RotateSpeed;


        public static void Delete(string datakey, UserData data)
        {

        }

        public static UserData Load(string datakey)
        {
            var data = PlayerPrefs.GetString(datakey);
            Debug.Log(data);
            UserData d = JsonConvert.DeserializeObject<UserData>(data);
            return d;
        }

        public static void Save(string dataKey, UserData data)
        {
            var d = JsonConvert.SerializeObject(data);
            Debug.Log(d);
            PlayerPrefs.SetString(dataKey, d);
        }
    }
}
