using Cyberverse.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cyberverse.AvatarConfiguration.Utility
{
    [System.Serializable]
    public class AvatarConfig
    {
        public List<CyberNode> components = new List<CyberNode>();

        public Dictionary<ComponentType, CyberNode> nodes = new Dictionary<ComponentType, CyberNode>();

        public void Save(CyberNodePrefab prefab)
        {
            if (nodes.Keys.Contains(prefab.type))
            {
                CyberNode node = nodes[prefab.type];
            }
        }

        public AvatarConfig Init()
        {
            nodes = components.ToDictionary(x => x.type, x => x);
            return this;
        }
        //select a character
        // the character is made up of peripherals
        // or detachables and atachables
        //spawn a list of items
        //select one to preview

    }

    [System.Serializable]
    public struct Pivot {
        public ComponentType type;
        public Transform transform;
    }

    [Serializable]
    public class CyberNode {
        public ComponentType type;
        public int Id;
        public Color color;
        public Vector3 pivotPosition;
    }


    public enum ComponentType {
        Head, Body, LeftHand, RightHand, HeadSet, Telephone
    }
}
