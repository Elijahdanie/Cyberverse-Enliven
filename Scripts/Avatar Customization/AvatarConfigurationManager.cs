
using Cyberverse.AvatarConfiguration.UI;
using Cyberverse.AvatarConfiguration.Utility;
using Cyberverse.EventSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cyberverse.AvatarConfiguration
{
    public class AvatarConfigurationManager : MonoBehaviour
    {
        public NodeUI prefabUI;
        public NodeUI ComponentTypeUI;
        public Transform ComponentTypeUIparent;
        public Transform nodeUIparent;
        public int MaxTypes;
        public static AvatarConfigurationManager main;
        public List<ConfigurablesList> nodeList = new List<ConfigurablesList>();
        public Dictionary<ComponentType, ConfigurablesList> nodeListmap = new Dictionary<ComponentType, ConfigurablesList>();
        public CharacterConfigHandler currentCharacterConfigured;
        public CharacterConfigHandler prefabContainer;
        public Transform previewPosition;

        public void Awake() {
            if (!main)
            {
                main = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            EventManager.main.OnSelectConfiguration.AddListener(OnSelectConfiguration);
            nodeListmap = nodeList.ToDictionary(x => x.type, x => x);
            DisplayComponents();
            currentCharacterConfigured = Instantiate(prefabContainer);
            currentCharacterConfigured.transform.position = previewPosition.position;
            currentCharacterConfigured.transform.localScale = previewPosition.localScale;
            currentCharacterConfigured.LoadUser();
        }

        private void SetCurrentNodeList(ComponentType type)
        {
            var tmp = nodeListmap[type];
            for (int i = 0; i < nodeUIparent.childCount; i++)
            {
                Destroy(nodeUIparent.GetChild(i).gameObject);
            }
            for (int i = 0; i < tmp.prefabs.Count; i++)
            {
                Instantiate(prefabUI, nodeUIparent).Display(tmp.prefabs[i]);
            }
        }

        public void DisplayComponents()
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                var tmp = Instantiate(ComponentTypeUI, ComponentTypeUIparent);
                tmp.description.text = $"{nodeList[i].type}";
                tmp.id = nodeList[i].type;
                tmp.btn.onClick.AddListener(() => { SetCurrentNodeList(tmp.id); });
                tmp.preview.sprite = nodeList[i].icon;
            }
            SetCurrentNodeList(ComponentType.Head);
        }

        public CyberNodePrefab GetPrefab(AvatarConfig avatar, ComponentType type, out bool isIn)
        {
            var tmp = nodeListmap[type].GetNode(avatar.nodes[type].Id, out bool check);
            if (check)
            {
                isIn = true;
                return tmp;
            }
            else
            {
                isIn = false;
                return null;
            }
        }

        public Dictionary<ComponentType, CyberNodePrefab> GetNodes(AvatarConfig avatar)
        {
            var tmp = new Dictionary<ComponentType, CyberNodePrefab>();
            for (int i = 0; i < avatar.components.Count; i++)
            {
                var tmp2 = GetPrefab(avatar, avatar.components[i].type, out bool isIn);
                if (isIn)
                {
                    tmp.Add(tmp2.type, tmp2);
                }
            }
            return tmp;
        }

        private void OnSelectConfiguration(CyberNodePrefab arg0)
        {
            currentCharacterConfigured.Change(arg0);
        }

    }
}
