using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialogueBox;

    public SaveDataJSON save;

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;
    AudioSource npcVoice;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartDialogue(DialogueAsset.Dialogue[] dialogue, int startPosition, string name, int index, AudioSource audioSource)
    {
        npcVoice = audioSource;
        Time.timeScale = 0;
        nameText.text = name + "...";
        dialogueBox.gameObject.SetActive(true);
        Discover(index);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue, startPosition));
    }

    IEnumerator RunDialogue(DialogueAsset.Dialogue[] dialogue, int startPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for (int i = startPosition; i < dialogue.Length; i++)
        {
            dialogueText.text = dialogue[i].text;
            PlayVoice(dialogue[i].voice);
            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            npcVoice.Stop();
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialogueBox.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void Discover(int index)
    {
        switch (index)
        {
            case 0:
                SaveData.Instance.MariG = true;
                break;
            case 1:
                SaveData.Instance.Pilot = true;
                break;
            case 2:
                SaveData.Instance.CChic = true;
                break;
            case 3:
                SaveData.Instance.CliSci = true;
                break;
            default:
                break;
        }
        save.StoreData();
    }

    private void PlayVoice(AudioClip clip)
    {
        npcVoice.clip = clip;
        npcVoice.Play();
    }
}