

using Cyberverse.Interactables;
using Cyberverse.Users;
using UnityEngine;
using UnityEngine.Events;

namespace Cyberverse.EventSystem
{
    public class InteractableEvent : UnityEvent<IInteractable> { }
    public class InteractEvent : UnityEvent<IUser, IInteractable> { }

    public class EventManager : MonoBehaviour
    {
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

        public static EventManager main;
        public InteractableEvent OnFocusInteraction = new InteractableEvent();
        public InteractableEvent OnExitInteraction = new InteractableEvent();
        public InteractEvent OnUserInteract = new InteractEvent();
    }
}
