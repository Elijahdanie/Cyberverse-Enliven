

using System;
using Cyberverse.Audio_Objects;
using Cyberverse.EventSystem;
using Cyberverse.Interactables.UI;
using Cyberverse.Users;
using UnityEngine;
using UnityEngine.UI;

namespace Cyberverse.Interactables
{

    public interface IInteractable
    {
        IDisplay display { get; }
        /// <summary>
        /// Should this interactable be locked on
        /// </summary>
        bool isLockedOn { get; }

        /// <summary>
        /// The current tag of this interactable
        /// </summary>
        string tag { get; set; }

        /// <summary>
        /// This Initiates the process of interaction
        /// </summary>
        /// <param name="user"></param>
        void Interact(IUser user);

        DisplayInfo Format();

        void Exit();
    }

    public interface IDisplay {
        void Dispaly(Transform parent, IInteractable interactable);
    }

    public class InteractHelper : MonoBehaviour
    {
        IUser user;
        public string musicTag;
        internal IInteractable currentInteractable;
        internal IInteractable previewInteractable;

        public void Init()
        {
            user = GetComponentInParent<IUser>();
            if (user == null)
            {
                Debug.LogError($"Please add the user client script to {name}");
            }
            if (EventManager.main)
            {
                EventManager.main.OnUserInteract.AddListener(OnUserInteract);
                EventManager.main.OnExitInteraction.AddListener(OnExitInteraction);
            }
        }

        internal void Interact()
        {
            EventManager.main.OnUserInteract.Invoke(user, previewInteractable);
        }

        private void OnExitInteraction(IInteractable arg0)
        {
            //if (previewInteractable.Equals(currentInteractable))
            previewInteractable = null;
            currentInteractable = null;
        }

        private void OnUserInteract(IUser user, IInteractable interactable)
        {
            if (user.Equals(this.user))
            {
                currentInteractable = interactable;
                currentInteractable.Interact(user);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!EventManager.main) return;
            if (currentInteractable != null || previewInteractable != null) return;
            if (other.CompareTag(musicTag))
            {
                previewInteractable = other.GetComponent<IInteractable>();
                EventManager.main.OnFocusInteraction.Invoke(previewInteractable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (previewInteractable != null && currentInteractable == null)
            {
                if (!EventManager.main) return;
                if (other.CompareTag(previewInteractable.tag))
                {
                    EventManager.main.OnExitInteraction.Invoke(previewInteractable);
                    previewInteractable = null;
                }
            }
            else if (currentInteractable != null)
            {
                if (!currentInteractable.isLockedOn)
                {
                    EventManager.main.OnExitInteraction.Invoke(currentInteractable);
                }
            }
        }
    }

}
