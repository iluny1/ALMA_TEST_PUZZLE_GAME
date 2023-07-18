 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleChooseScreen : MonoBehaviour
{
    [SerializeField] private GameObject buttonPuzzlePrefab;
    [SerializeField] private GameObject buttonDifficultyPrefab;

    [SerializeField] private GameObject gridPuzzles;
    [SerializeField] private GameObject gridDifficulties;

    [SerializeField] private GameObject menuImage;
    [SerializeField] private GameObject menuDifficulty;

    [SerializeField] private GameObject settings;

    private Puzzle choosenPuzzle;
    private MenuGrid choosenGrid;
    private List<Puzzle> puzzleList;
    private List<MenuGrid> gridList;

    private void Start()
    {
        SetPuzzleMenu();
    }

    private void SetPuzzleMenu()
    {
        puzzleList = Resources.LoadAll<Puzzle>("Puzzles_SO/").ToList<Puzzle>();

        if (puzzleList.Count == 0)
        {
            Debug.LogError("There are no puzzles! See if this any puzzles in 'Puzzles_SO' folder!");
            Application.Quit(-10);
        }

        foreach (Puzzle puzzle in puzzleList)
        {
            GameObject button = Instantiate(buttonPuzzlePrefab, gridPuzzles.transform);
            button.GetComponent<RawImage>().texture = puzzle.Image;
            button.GetComponent<PuzzleButton>().PuzzleInfo = puzzle;
            button.GetComponent<PuzzleButton>().MenuLogic = this.gameObject;
            button.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = puzzle.Name;
        }
    }
    
    private void SetDifficultyMenu()
    {
        gridList = Resources.LoadAll<MenuGrid>("Grid_SO/").ToList<MenuGrid>();

        if (gridList.Count == 0)
        {
            Debug.LogError("There are no grids! See if this any grids in 'Grid_SO' folder!");
            Application.Quit(-10);
        }

        foreach (MenuGrid grid in gridList)
        {
            GameObject button = Instantiate(buttonDifficultyPrefab, gridDifficulties.transform);
            button.GetComponent<RawImage>().texture = choosenPuzzle.Image;
            button.GetComponent<GridButton>().grid = grid;
            button.GetComponent<GridButton>().MenuLogic = this.gameObject;
            button.transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = grid.Image;
            button.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = grid.Name;

        }
    }

    public void SwitchMenu(Puzzle puzzleInfo)
    {
        choosenPuzzle = puzzleInfo;
        menuImage.SetActive(false);
        menuDifficulty.SetActive(true);
        SetDifficultyMenu();
    }

    public void LaunchPuzzle(MenuGrid choosenGrid)
    {
        settings.GetComponent<GameSettings>().SetSettings(choosenPuzzle, choosenGrid);
        DontDestroyOnLoad(settings);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
