using Cyberverse.AvatarConfiguration.Utility;
using Cyberverse.EventSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cyberverse.AvatarConfiguration.UI
{

    [RequireComponent(typeof(Button))]
    public class NodeUI : MonoBehaviour
    {
        public TMP_Text description;
        public Button btn;
        public Image preview;
        internal ComponentType id;
        CyberNodePrefab cyber;

        public void Init(string text, UnityAction action, Sprite icon)
        {
            description.text = text;
            btn.onClick.AddListener(action);
            preview.sprite = icon;
        }

        internal void Display(CyberNodePrefab cyberNodePrefab)
        {
            this.cyber = cyberNodePrefab;
            Init(cyberNodePrefab.description, () =>
                EventManager.main.OnSelectConfiguration.Invoke(this.cyber),
                cyberNodePrefab.iconPreview);
        }
    }
}
