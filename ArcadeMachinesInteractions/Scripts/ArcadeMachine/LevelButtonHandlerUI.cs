using UnityEngine;

public class LevelButtonHandlerUI : MonoBehaviour
{
    int levelIndex;
    ArcadeGameMenuUI menuUI;

    public void Setup(int index, ArcadeGameMenuUI ui)
    {
        levelIndex = index;
        menuUI = ui;
    }

    public void OnClick()
    {
        menuUI.LoadLevel(levelIndex);
    }
}