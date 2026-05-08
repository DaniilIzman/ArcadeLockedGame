using System;
using TMPro;
using UnityEngine;

public class DialogueCF : MonoBehaviour
{
    [SerializeField] String[] timelineNextLine;
    [SerializeField] TMP_Text dialogueText;
    int currentLine = 0;

    public void nextDialogueLine()
    {
        currentLine ++;
        dialogueText.text = timelineNextLine[currentLine];
    }
}
