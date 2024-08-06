using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public GridManager gridManager;
    public PuzzleLayoutUpdater puzzleLayoutUpdater; // Reference to the PuzzleLayoutUpdater
    public GameObject levelsMenu; // Reference to the levels menu UI
    public GameObject gameUI; // Reference to the game UI
    public LineDrawingManager lineDrawingManager;

    private readonly Dictionary<int, GameObject> levelPanels = new();
    private readonly Dictionary<int, Transform> gridParents = new();
    private LevelConfiguration currentLevelConfiguration;
    private int currentLevelNumber;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ShowLevelsMenu();
    }

    public void RegisterLevelPanel(int levelNumber, GameObject panel, Transform gridParent)
    {
        if (!levelPanels.ContainsKey(levelNumber))
        {
            levelPanels.Add(levelNumber, panel);
            gridParents.Add(levelNumber, gridParent);
            panel.SetActive(false);
        }
    }

    public void LoadLevel(int levelNumber)
    {
        currentLevelNumber = levelNumber;
        int gridSize = 5 + levelNumber; // Increase grid size with each level

        if (gridParents.TryGetValue(levelNumber, out Transform gridParent))
        {
            gridManager.SetGridParent(gridParent); // Set the grid parent dynamically
            currentLevelConfiguration = gridManager.GenerateGrid(levelNumber, gridSize);
            lineDrawingManager.SetGridParent(gridParent); // Set the grid parent in LineDrawingManager
        }

        LineRenderer layoutLineRenderer = gridManager.GetLayoutLineRenderer();
        puzzleLayoutUpdater.UpdatePuzzleLayout(layoutLineRenderer);

        levelsMenu.SetActive(false);
        gameUI.SetActive(true);

        foreach (var panel in levelPanels.Values)
        {
            panel.SetActive(false);
        }

        if (levelPanels.TryGetValue(levelNumber, out GameObject levelPanel))
        {
            levelPanel.SetActive(true);
        }
    }

    public LevelConfiguration GetCurrentLevelConfiguration()
    {
        return currentLevelConfiguration;
    }

    public void SaveProgress(int level)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (level >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", level + 1);
        }
    }

    public void LoadNextLevel()
    {
        SaveProgress(currentLevelNumber);
        int nextLevel = currentLevelNumber + 1;

        if (levelPanels.ContainsKey(nextLevel))
        {
            LoadLevel(nextLevel);
        }
        else
        {
            ShowLevelsMenu();
        }
    }

    public void ShowLevelsMenu()
    {
        levelsMenu.SetActive(true);
        gameUI.SetActive(false);

        foreach (var panel in levelPanels.Values)
        {
            panel.SetActive(false);
        }
    }
}
