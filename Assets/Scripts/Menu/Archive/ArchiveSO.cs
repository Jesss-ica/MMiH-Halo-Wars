using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ArchiveSO : ScriptableObject
{
    public Entry[] entry;

    [System.Serializable]
    public struct Entry
    {
        public Sprite image;
        public string title;
        [TextArea]
        public string description;
    }
}
