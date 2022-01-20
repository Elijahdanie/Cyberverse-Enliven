using Cyberverse.Audio_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cyberverse.Interactables.UI
{
    public class ItemDisplay : MonoBehaviour, IDisplay
    {
        public TMP_Text title;
        public TMP_Text otherData;
        public CyberButton prefab;
        public Transform buttonParent;

        public void Dispaly(Transform parent, IInteractable interactable)
        {
            var info = interactable.Format();
            title.text = info.Title;
            otherData.text = info.FormattedText;
            info.functions.ForEach(x =>
            {
                Instantiate(prefab, buttonParent).Display(x);
            });
            gameObject.SetActive(true);
        }
    }

    public struct DisplayInfo
    {
        public string Title;
        public string FormattedText;
        public List<CyberbuttonData> functions;
    }

    public struct CyberbuttonData
    {
        public string functionDescription;
        public string ImageUrl;
        public Sprite Icon;
        public UnityAction action;
    }
}
