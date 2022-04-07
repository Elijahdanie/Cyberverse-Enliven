using Cyberverse.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cyberverse.Interactables.UI
{
    public class CyberButton : MonoBehaviour
    {
        public TMP_Text functionDescription;
        public Image Icon;
        public Button btn;

        public void Display(CyberbuttonData data)
        {
            functionDescription.text = data.functionDescription;
            Icon.sprite = data.Icon;
            btn.onClick.AddListener(data.action);
            btn.onClick.AddListener(()=>EventManager.main.CloseContextMenu());
        }
    }
}
