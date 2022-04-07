
using Cyberverse.AvatarConfiguration;
using Cyberverse.AvatarConfiguration.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Cyberverse.AvatarConfiguration
{
    [CreateAssetMenu(fileName = "new config list", menuName = "cerate new config list")]
    public class ConfigurablesList : ScriptableObject
    {
        public List<CyberNodePrefab> prefabs = new List<CyberNodePrefab>();
        public Sprite icon;
        public string Name;

        public void Init() {
            int num = 0;
            prefabs.ForEach(x =>
            {
                x.Id = num;
                num++;
            });
        }

        public CyberNodePrefab GetNode(int id, out bool isIn)
        {
            if (prefabs.Count > id)
            {
                isIn = true;
                return prefabs[id];
            }
            isIn = false;
            return null;
        }
    }
}
