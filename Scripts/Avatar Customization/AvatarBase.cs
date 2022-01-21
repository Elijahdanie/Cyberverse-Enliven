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
        public Dictionary<ComponentType, CyberNode> nodes = new Dictionary<ComponentType, CyberNode>();

        public void Update(CyberNodePrefab prefab)
        {
            if (nodes.Keys.Contains(prefab.type))
            {
                CyberNode node = nodes[prefab.type];
                node.Id = prefab.Id;
                nodes[prefab.type] = node;
            }
        }

        internal void SaveNew(CyberNodePrefab prefab)
        {
            if (!nodes.Keys.Contains(prefab.type))
            {
                nodes.Add(prefab.type, new CyberNode() {
                    Id = prefab.Id
                });
            }
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

    public struct CVector3 {
        public float x;
        public float y;
        public float z;

        public CVector3 Set (Vector3 vec) {
            this.x = vec.x;
            this.y = vec.y;
            this.z = vec.z;
            return this;
        }

        internal Vector3 Get()
        {
            return new Vector3(x,y,z);
        }
    }

    public struct CVector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public CVector4 Set(Color vec)
        {
            this.x = vec.r;
            this.y = vec.g;
            this.z = vec.b;
            this.w = vec.a;
            return this;
        }

        internal Color Get()
        {
            return new Color(x, y, z,w);
        }
    }

    [Serializable]
    public class CyberNode {
        public ComponentType type;
        public int Id;
        public CVector4 color;
        public CVector3 pivotPosition;
    }


    public enum ComponentType {
        Head, Body, LeftHand, RightHand, HeadSet, Telephone
    }
}
