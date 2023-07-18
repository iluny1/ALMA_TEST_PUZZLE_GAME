using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject puzzlesListGO;
    [SerializeField] private GameObject prefabChunk;
    [SerializeField] private GameObject prefabChunkUI;
    [SerializeField] private GameObject Field;
    [SerializeField] private GameObject gameSettings;
    [SerializeField] private GameObject content;
    [SerializeField] private PuzzleList puzzlesList;
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private MenuGrid menuGrid;
    
    void Awake()
    {
        SetVariables();
        SplitImage();
        puzzlesList.GenerateRandomList(null);
        SetChunksToUI();
        SetField();
        WinnerLogic.Instance.SetTarget(menuGrid.DivX * menuGrid.DivY);
    }

    private void SetVariables()
    {
        gameSettings = GameObject.Find("GameSettings");
        puzzlesList = puzzlesListGO.GetComponent<PuzzleList>();
        puzzle = gameSettings.GetComponent<GameSettings>().GetPuzzle();
        menuGrid = gameSettings.GetComponent<GameSettings>().GetGrid();
        PuzzleList.Instance.SetGridSize(menuGrid.DivX);
    }

    private void SplitImage()
    {
        SplitImage splitImage = gameObject.GetComponent<SplitImage>();
        splitImage.Split(puzzle.Image, menuGrid.DivX, menuGrid.DivY);

        int id = 1;
        List<Texture2D> imageList = splitImage.GetImages();

        for (int x = 0; x < menuGrid.DivX; x++)
        {
            for (int y = 0; y < menuGrid.DivY; y++)
            {
                Chunk chunk = new Chunk(id, x, y, imageList[id - 1]);
                puzzlesList.AddChunk(chunk);
                id++;
            }
        }
    }

    private void SetChunksToUI()
    {
        foreach (Chunk chunk in puzzlesList.GetChunks())
        {
            GameObject chunkUI = Instantiate(prefabChunkUI, content.transform);
            chunkUI.GetComponent<ChunkUI>().SetChunk(chunk);
            chunkUI.GetComponent<RawImage>().texture = chunk.GetImage();
        }
    }

    private void SetField() //Знаю, что очень плохо
    {
        switch (menuGrid.DivX)
        {
            case 4:
                Field.transform.position = new Vector2(4.5f, 4.5f);
                break;
            case 5:
                Field.transform.position = new Vector2(4.8f, 4.8f);
                break;
            case 6:
                Field.transform.position = new Vector2(5f, 5f);
                break;
        }
    }
}
