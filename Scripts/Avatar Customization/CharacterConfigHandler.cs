
using Cyberverse.AvatarConfiguration.Utility;
using Cyberverse.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cyberverse.AvatarConfiguration
{
    public class CharacterConfigHandler : MonoBehaviour
    {
        public AvatarConfig avatar;
        public List<Pivot> pivots = new List<Pivot>();
        public Dictionary<ComponentType, Transform> nodePivots = new Dictionary<ComponentType, Transform>();
        public Dictionary<ComponentType, CyberNodePrefab> pool = new Dictionary<ComponentType, CyberNodePrefab>();
        public float rotSpeed;
        public Transform conatainer;

        public void LoadUser()
        {
            //get avater
            nodePivots = pivots.ToDictionary(x => x.type, x => x.transform);
            var data = EventManager.main.GetData();
            if (data != null)
            {
                avatar = data.avatar;
            }
            else
            {
                avatar = GenerateDefault();
            }
            Build(AvatarConfigurationManager.main.GetNodes(avatar));
        }

        private AvatarConfig GenerateDefault()
        {
            List<CyberNode> nodes = new List<CyberNode>();
            for (int i = 0; i < AvatarConfigurationManager.main.nodeList.Count; i++)
            {
                var type = ((ComponentType)i);
                if (nodePivots.Keys.Contains(type))
                {
                    nodes.Add(new CyberNode()
                    {
                        type = (ComponentType)i,
                        Id = 0,
                        pivotPosition = nodePivots[type].position
                    });
                }
            }
            return (new AvatarConfig()
            {
                components = nodes
            }).Init();
        }

        public void Build(Dictionary<ComponentType, CyberNodePrefab> map)
        {
            foreach (var item in map)
            {
                var node = Instantiate(item.Value, conatainer);
                node.transform.localPosition = nodePivots[item.Key].transform.localPosition;
                pool.Add(item.Key, node);
            }
        }

        internal void Change(CyberNodePrefab arg0)
        {
            avatar.Save(arg0);
            var oldone = pool[arg0.type];
            Destroy(oldone.gameObject);
            var node = Instantiate(arg0, transform);
            node.transform.localPosition = nodePivots[arg0.type].transform.localPosition;
            pool[arg0.type] = node;
        }

        void Update() {
            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }
    }
}
