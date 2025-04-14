using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO.Compression;
using TMPro;
using UnityEngine;

public class ChangeLogHandler : MonoBehaviour
{
    public ChangeLogAsset changeLogAsset;
    public GameObject entreePrefab;
    private GameObject entree;
    public GameObject contents;
    
    public void ActivateChangeLog()
    {
        for (int i = 0; i < changeLogAsset.changeLogEntree.Length; i++)
        {
            contents.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void SetChangeLog()
    {
        for(int i = 0; i < changeLogAsset.changeLogEntree.Length; i++)
        {
            entree = Instantiate(entreePrefab, contents.transform);
            entree.GetComponent<ChangeLogEntree>().SetInfo(changeLogAsset.changeLogEntree[i]);
        }
    }
}
