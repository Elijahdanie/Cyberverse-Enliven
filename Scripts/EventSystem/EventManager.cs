

using System;
using Cyberverse.AvatarConfiguration;
using Cyberverse.AvatarConfiguration.Utility;
using Cyberverse.Interactables;
using Cyberverse.Peripherals.Data;
using Cyberverse.Users;
using UnityEngine;
using UnityEngine.Events;

namespace Cyberverse.EventSystem
{
    public class InteractableEvent : UnityEvent<IInteractable> { }
    public class InteractEvent : UnityEvent<IUser, IInteractable> { }
    public class ComponentEvent : UnityEvent<CyberNodePrefab> { }
    public class UserEvent : UnityEvent<IUser> { }
    public class ConfigurationEvent : UnityEvent<CharacterConfigHandler> { }
    public class EventManager : MonoBehaviour
    {
        public bool isNull;
        public UserData userData;
        internal UserData GetData()
        {
            userData = UserData.Load(DataKey);
            return userData;
        }

        private void Awake()
        {
            if (!main)
            {
                main = this;
            }
            else {
                Destroy(this);
            }
        }
        public string DataKey;
        public static EventManager main;
        public InteractableEvent OnFocusInteraction = new InteractableEvent();
        public InteractableEvent OnExitInteraction = new InteractableEvent();
        public InteractEvent OnUserInteract = new InteractEvent();
        public UserEvent OnAnnounceUser = new UserEvent();
        public ConfigurationEvent OnAnnounceSkeleton = new ConfigurationEvent();
        public UnityEvent OnCloseContextMenu = new UnityEvent();

        internal void SetDefaultData(AvatarConfig avatar)
        {
            userData = new UserData()
            {
                moveSpeed = 10,
                RotateSpeed = 50,
                avatar = avatar,
                mic = new Mic()
                {
                    isOn = true,
                    deviceId = 0,
                    Id = 0
                },
                speaker = new Speaker()
                {
                    isOn = true,
                    Id = 0
                },
                uuid = "Elijah"
            };
            UserData.Save(DataKey, userData);
        }

        public ComponentEvent OnSelectConfiguration = new ComponentEvent();

        public GameObject configurationPanel;
        public void StartGame() {
            //if (GetData() == null)
            //{
            //    configurationPanel.SetActive(true);
            //    AvatarConfigurationManager.main.OpenConfiguration();
            //}
            //else
            //{

            //}
            configurationPanel.SetActive(true);
            AvatarConfigurationManager.main.OpenConfiguration();
        }

        public void Save(AvatarConfig avatar)
        {
            userData.avatar = avatar;
            UserData.Save(DataKey, userData);
        }

        public void CloseContextMenu()
        {
            OnCloseContextMenu.Invoke();
        }
    }
}
