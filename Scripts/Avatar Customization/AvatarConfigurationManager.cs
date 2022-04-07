
using Cyberverse.AvatarConfiguration.UI;
using Cyberverse.AvatarConfiguration.Utility;
using Cyberverse.EventSystem;
using Cyberverse.Interactables;
using Cyberverse.Peripherals;
using Cyberverse.Users;
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
        public Dictionary<string, ConfigurablesList> nodeListmap = new Dictionary<string, ConfigurablesList>();
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

        public void OpenConfiguration()
        {
            EventManager.main.OnSelectConfiguration.AddListener(OnSelectConfiguration);
            nodeListmap = nodeList.ToDictionary(x => x.Name, x => x);
            DisplayComponents();
            currentCharacterConfigured = Instantiate(prefabContainer);
            currentCharacterConfigured.transform.position = previewPosition.position;
            currentCharacterConfigured.transform.localScale = previewPosition.localScale;
            currentCharacterConfigured.LoadUser();
            currentCharacterConfigured.configEnabled = true;
            EventManager.main.OnAnnounceSkeleton.Invoke(currentCharacterConfigured);
        }

        private void SetCurrentNodeList(string type)
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
                tmp.description.text = $"{nodeList[i].Name}";
                tmp.id = nodeList[i].Name;
                tmp.btn.onClick.AddListener(() => { SetCurrentNodeList(tmp.id); });
                tmp.preview.sprite = nodeList[i].icon;
            }
            //SetCurrentNodeList();
        }

        public CyberNodePrefab GetPrefab(AvatarConfig avatar, string type, out bool isIn)
        {
            nodeListmap[type].Init();
            Debug.Log(avatar.nodes);
            for (int i = 0; i < avatar.nodes.Count; i++)
            {
                Debug.Log(avatar.nodes.ElementAt(i));
            }
            var tmp = nodeListmap[type].GetNode(avatar.nodes[type].Id, out bool check);
            Debug.Log(type);
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

        public Dictionary<string, CyberNodePrefab> GetNodes(AvatarConfig avatar)
        {
            var tmp = new Dictionary<string, CyberNodePrefab>();
            for (int i = 0; i < avatar.nodes.Count; i++)
            {
                var tmp2 = GetPrefab(avatar, avatar.nodes.ElementAt(i).Value.type, out bool isIn);
                if (isIn)
                {
                    tmp.Add(tmp2.Name, tmp2);
                }
            }
            return tmp;
        }

        private void OnSelectConfiguration(CyberNodePrefab arg0)
        {
            currentCharacterConfigured.Change(arg0);
        }

        public void EnableConfiguration() {
            currentCharacterConfigured.configEnabled = true;
            EventManager.main.OnAnnounceSkeleton.Invoke(currentCharacterConfigured);
        }

        public void Deploy() {
            var player = currentCharacterConfigured.GetComponent<UserClient>();
            if (!player)
            {
                player = currentCharacterConfigured.gameObject.AddComponent<UserClient>();
                player.userData = EventManager.main.GetData();
                var rb = player.gameObject.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            //var allnodes = player.GetComponentsInChildren<CyberNodePrefab>();
            //foreach (var item in allnodes)
            //{
            //    Destroy(item);
            //}
            //Destroy(currentCharacterConfigured);
            currentCharacterConfigured.configEnabled = false;
            player.Init();
            EventManager.main.OnAnnounceUser.Invoke(player);
        }

    }
}
