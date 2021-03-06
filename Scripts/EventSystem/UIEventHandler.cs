
using Cyberverse.Interactables;
using Cyberverse.Interactables.UI;
using Cyberverse.Users;
using UnityEngine;

namespace Cyberverse.EventSystem
{
    public class UIEventHandler : MonoBehaviour
    {
        public GameObject interactNotification;
        public ItemDisplay itemDisplay;
        public Transform parent;
        public UserClient userClient;
        IUser user;
        public GameObject Pausemenu;
        private void Start()
        {
            if (EventManager.main)
            {
                EventManager.main.OnFocusInteraction.AddListener(OnFocusInteraction);
                EventManager.main.OnExitInteraction.AddListener(OnExitInteraction);
                EventManager.main.OnUserInteract.AddListener(OnUserInteract);
                EventManager.main.OnAnnounceUser.AddListener(OnAnnounceUser);
                EventManager.main.OnCloseContextMenu.AddListener(CloseItemDisplay);
            }
        }

        public void OnAnnounceUser(IUser user)
        {
            this.user = user;
        }

        public void Interact() {
            if(interactNotification.activeInHierarchy)
                user.EngageInteractable();
        }

        public void CloseItemDisplay() {
            itemDisplay.Clean();
        }

        private void OnUserInteract(IUser arg0, IInteractable arg1)
        {
            interactNotification.SetActive(false);
            itemDisplay.Dispaly(parent, arg1);
        }

        private void OnExitInteraction(IInteractable arg0)
        {
            if (interactNotification) interactNotification.SetActive(false);
        }

        private void OnFocusInteraction(IInteractable arg0)
        {
            if (interactNotification) interactNotification.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Interact();
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Pausemenu.SetActive(!Pausemenu.activeInHierarchy);
            }
        }
    }
}
