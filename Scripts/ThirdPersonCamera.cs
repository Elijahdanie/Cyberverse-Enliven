using Cyberverse.EventSystem;
using Cyberverse.Users;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberverse.AvatarConfiguration;

public class ThirdPersonCamera : MonoBehaviour {

    public IUser TargetPlayer;
    public Vector2 Xminmax;
    public Vector2 YminMax;
    public float Distance;
    public float Height;
    public float MouseSensitivity;
    public float HorizontalOffset;
    public CharacterConfigHandler user;
    public float ConfigurationDistance;

    float yaw, pitch = 0;

    private void Start()
    {
        EventManager.main.OnAnnounceUser.AddListener(OnAnnonceUser);
        EventManager.main.OnAnnounceSkeleton.AddListener(OnAnnounceSkeleton);
    }

    private void OnAnnounceSkeleton(CharacterConfigHandler arg0)
    {
        user = arg0;
        TargetPlayer = null;
    }

    private void OnAnnonceUser(IUser arg0)
    {
        TargetPlayer = arg0;
        user = null;
    }
    public bool cursorDeactivate;
    private void Update()
    {
        if (user != null)
        {
            transform.position = user.transform.position - transform.forward * 50 + transform.up * 10 +
                transform.right * HorizontalOffset;
        }
        if (TargetPlayer != null && !Cursor.visible)
        {
            float mousx = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            yaw += mousx * MouseSensitivity;
            pitch -= mouseY * MouseSensitivity;
            pitch = Mathf.Clamp(pitch, Xminmax.x, Xminmax.y);
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            transform.position = TargetPlayer.transform.position - transform.forward * Distance + transform.up * Height +
                transform.right * HorizontalOffset;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            cursorDeactivate = !cursorDeactivate;
            if (cursorDeactivate)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
