using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLogEntree : MonoBehaviour
{
    public TextMeshProUGUI versionText;
    public TextMeshProUGUI releaseText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI entreeInformation;

    public void SetInfo(ChangeLogAsset.ChangeLog ChangeLogEntree)
    {
        versionText.text = ChangeLogEntree.version;
        releaseText.text = ChangeLogEntree.releaseDate;
        descriptionText.text = ChangeLogEntree.updateDescription;
        entreeInformation.text = ChangeLogEntree.updateInformation;
    }
}
