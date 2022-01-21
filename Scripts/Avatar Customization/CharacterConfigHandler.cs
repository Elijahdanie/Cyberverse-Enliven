
using Cyberverse.AvatarConfiguration.Utility;
using Cyberverse.EventSystem;
using Cyberverse.Users;
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
            bool isnew = false;
            if (data != null)
            {
                avatar = data.avatar;
            }
            else
            {
                avatar = GenerateDefault();
                isnew = true;
            }
            Build(AvatarConfigurationManager.main.GetNodes(avatar), isnew);
        }

        private AvatarConfig GenerateDefault()
        {
            Dictionary<ComponentType, CyberNode> nodes = new Dictionary<ComponentType, CyberNode>();
            for (int i = 0; i < AvatarConfigurationManager.main.nodeList.Count; i++)
            {
                var type = ((ComponentType)i);
                if (nodePivots.Keys.Contains(type))
                {
                    nodes.Add(type, new CyberNode()
                    {
                        type = (ComponentType)i,
                        Id = 0,
                        pivotPosition = new CVector3().Set(nodePivots[type].position)
                    });
                }
            }
            return (new AvatarConfig()
            {
                nodes = nodes
            });
        }

        public void Build(Dictionary<ComponentType, CyberNodePrefab> map, bool isnew)
        {
            foreach (var item in map)
            {
                var nodeprefab = Instantiate(item.Value, conatainer);
                nodeprefab.transform.localPosition = nodePivots[item.Key].transform.localPosition;
                    //avatar.nodes[item.Value.type].pivotPosition.Get();
                pool.Add(item.Key, nodeprefab);
                if (isnew)
                {
                    avatar.SaveNew(nodeprefab);
                    EventManager.main.SetDefaultData(avatar);
                }
            }
        }

        internal void Change(CyberNodePrefab arg0)
        {
            avatar.Update(arg0);
            var oldone = pool[arg0.type];
            Destroy(oldone.gameObject);
            var node = Instantiate(arg0, transform);
            node.transform.localPosition = nodePivots[arg0.type].transform.localPosition;
            pool[arg0.type] = node;
            EventManager.main.Save(avatar);
        }

        void Update() {
            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }
    }
}
