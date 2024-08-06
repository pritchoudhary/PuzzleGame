using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelMenu : MonoBehaviour
{
    public List<Button> levelButtons;

    void Start()
    {
        InitializeLevelButtons();
    }

    private void InitializeLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].interactable = i < unlockedLevel;
        }
    }

    public void LoadLevel(int level)
    {
        LevelManager.Instance.LoadLevel(level);
    }
}
