using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private MenuGrid grid;

    public void SetSettings(Puzzle puzzle, MenuGrid grid)
    {
        this.puzzle = puzzle;
        this.grid = grid;
    }

    public Puzzle GetPuzzle() { return puzzle; }
    public MenuGrid GetGrid() { return grid; }
}
