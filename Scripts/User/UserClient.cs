
using Cyberverse.EventSystem;
using Cyberverse.Interactables;
using Cyberverse.Peripherals;
using UnityEngine;

namespace Cyberverse.Users
{
    public class UserClient : MonoBehaviour, IUser
    {
        public UserData userData;
        public InteractHelper interactHelper;
        public SpeakerBehaviour speakerBehaviour;
        public MicBehaviour micBehaviour;

        public void Init()
        {
            micBehaviour = GetComponentInChildren<MicBehaviour>();
            speakerBehaviour = GetComponentInChildren<SpeakerBehaviour>();
            interactHelper = GetComponentInChildren<InteractHelper>();
            interactHelper.Init();
            speakerBehaviour.Init(userData.speaker);
            micBehaviour.Init(userData.mic);
        }

        public void Move(Vector2 moveVector)
        {
            Vector3 moveVec = transform.right * moveVector.x * Time.deltaTime * userData.moveSpeed;
            Vector3 trans = transform.InverseTransformDirection((transform.forward * moveVector.y) + moveVec);
            transform.Translate(trans);
        }

        public void Rotate(Vector2 rotateeVector) {
            transform.rotation = Quaternion.Euler(transform.eulerAngles + (Vector3.up * rotateeVector.x * Time.deltaTime * userData.RotateSpeed));
        }

        public void TogMic(bool value) {
            micBehaviour.Tog();
        }

        public void TogSpeaker(bool value)
        {
            speakerBehaviour.Tog();
        }

        private void Update()
        {
            Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            Rotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }

        public void EngageInteractable()
        {
            interactHelper.Interact();
        }
    }
}
