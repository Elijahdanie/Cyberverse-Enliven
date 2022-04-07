
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
        private GameObject Target;
        public Rigidbody rb;
        public Animator anim;
        public Vector3 Jumpforce;
        public Transform footPoint;
        public Vector3 raydirection;
        public float maxRayDistance;
        public float speedMultiplier;
        public Ray ray;

        public void Init()
        {
            micBehaviour = GetComponentInChildren<MicBehaviour>();
            speakerBehaviour = GetComponentInChildren<SpeakerBehaviour>();
            interactHelper = GetComponentInChildren<InteractHelper>();
            interactHelper.Init();
            speakerBehaviour.Init(userData.speaker);
            micBehaviour.Init(userData.mic);
            Target = Camera.main.transform.GetChild(0).gameObject;
            rb = GetComponent<Rigidbody>();
            ray = new Ray(transform.position, Vector3.down);
            anim = GetComponent<Animator>();
        }

        public Vector2 Move(Vector2 moveVector)
        {
            anim.SetBool("move", moveVector != Vector2.zero);
            Vector3 moveVec = transform.right * moveVector.x * Time.deltaTime * userData.moveSpeed;
            Vector3 trans = transform.InverseTransformDirection((transform.forward * moveVector.y) + moveVec);
            transform.Translate(-1f * trans);
            // += trans * speedMultiplier;
            return moveVector;
        }

        public bool Jump() {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Jumpforce * Input.GetAxis("Vertical") + Vector3.forward, ForceMode.Impulse);
                return true;
            }
            anim.SetBool("grounded", Physics.Raycast(ray, maxRayDistance) ? true : false);
            return false;
        }



        public void Rotate(Vector2 rotateeVector)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                Quaternion qt = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.transform.position - transform.position), userData.RotateSpeed * Time.deltaTime);
                float y = qt.eulerAngles.y;
                transform.rotation = Quaternion.Euler(Vector3.up * y);
            }
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
            if (Cursor.lockState == CursorLockMode.Locked) return;
            Jump();
            Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            //{
                Rotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
            //}
        }

        public void EngageInteractable()
        {
            interactHelper.Interact();
        }
    }
}
