using System;
using TMPro;
using UnityEngine;

public class DialogueCF : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] string[] timelineNextLine;

    [Header("UI")]
    [SerializeField] TMP_Text dialogueText;

    int currentLine = 0;

    public void NextDialogueLine()
    {
        dialogueText.text = timelineNextLine[currentLine];
        currentLine++;
    }
}