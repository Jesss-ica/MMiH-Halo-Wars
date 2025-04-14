using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ChangeLogAsset : ScriptableObject
{
    public ChangeLog[] changeLogEntree;

    [System.Serializable]
    public struct ChangeLog
    {
        public string version;
        public string releaseDate;
        [TextArea]
        public string updateDescription;
        [TextArea]
        public string updateInformation;
    }
}

