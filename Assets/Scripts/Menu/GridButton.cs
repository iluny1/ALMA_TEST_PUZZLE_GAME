using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButton : MonoBehaviour
{
    public MenuGrid grid;
    public GameObject MenuLogic;

    public void Click()
    {
        MenuLogic.GetComponent<PuzzleChooseScreen>().LaunchPuzzle(grid);
    }
}
