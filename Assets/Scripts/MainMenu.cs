using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject shade;
    public GameObject credits;
    public GameObject archive;
    public GameObject changeLog;
    public GameObject buttonPanel;
    public ArchiveManager archiveManager;

    private GameObject cacheGameObj;

    public Camera mainCamera;

    private bool archivePanelState = false;
    private bool creditPanelState = false;
    private bool changeLogPanelState = false;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ReadAchivment();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSubMenus();
        }
    }
    private void Start()
    {
        archiveManager.SetArchivePosMarkers();
        //changeLog.transform.GetComponent<ChangeLogHandler>().SetChangeLog();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void ReadAchivment()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "Achivment")
            {
                if(shade.activeSelf == false)
                    {
                    if(cacheGameObj != null)
                    {
                        cacheGameObj.GetComponent<AudioSource>().Stop();
                    }
                    cacheGameObj = objectHit.gameObject;
                    objectHit.gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }

    }

    #region SubMenu
    public void CloseSubMenus()
    {
        if(archivePanelState == true)
        {
            SetArchive();
        }
        if(creditPanelState == true)
        {
            SetCredit();
        }
        if(changeLogPanelState == true)
        {
            SetChangeLog();
        }
    }

    public void SetArchive()
    {
        arcState();
        archivePanelState = !archivePanelState;
    }

    public void SetChangeLog()
    {
        clogState();
        changeLogPanelState = !changeLogPanelState;
    }

    private void clogState()
    {
        buttonPanel.SetActive(changeLogPanelState);
        changeLog.SetActive(!changeLogPanelState);
        shade.SetActive(!changeLogPanelState);
    }

    private void arcState()
    {
        buttonPanel.SetActive(archivePanelState);
        archive.SetActive(!archivePanelState);
        shade.SetActive(!archivePanelState);
    }

    public void SetCredit()
    {
        creditPanelState = !creditPanelState;
        buttonPanel.SetActive(!creditPanelState);
        credits.SetActive(creditPanelState);
        shade.SetActive(creditPanelState);

    }
    public void ExitApplication()
    {
        Application.Quit();
    }
    #endregion
}
