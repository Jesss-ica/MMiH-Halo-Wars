using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueAsset : ScriptableObject
{
    public int Index;
    public Dialogue[] dialogue;

    [System.Serializable]
    public struct Dialogue
    {
        [TextArea]
        public string text;
        public AudioClip voice;
    }
    
}
