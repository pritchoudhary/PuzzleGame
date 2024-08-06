using UnityEngine;

public class LevelPanelRegistrar : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject[] levelPanels;
    public Transform[] gridParents;

    void Start()
    {
        for (int i = 0; i < levelPanels.Length; i++)
        {
            levelManager.RegisterLevelPanel(i + 1, levelPanels[i], gridParents[i]);
        }
    }
}
