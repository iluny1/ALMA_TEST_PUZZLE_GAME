using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] private GameObject gameSettings;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    private GridSystem<GridObject> gridSystem;

    public event EventHandler OnAnyChunkMovedGridPosition;
    public event EventHandler OnAnyChunkGetRemoved;

    public static LevelGrid Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameSettings = GameObject.Find("GameSettings");
        width = gameSettings.GetComponent<GameSettings>().GetGrid().DivX;
        height = gameSettings.GetComponent<GameSettings>().GetGrid().DivY;
        cellSize = 12f / width;
        gridSystem = new GridSystem<GridObject>(width, height, cellSize,
                (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
    }

    public void AddChunkAtGridPosition(GridPosition gridPosition, Chunk chunk)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddChunk(chunk);
    }

    public List<Chunk> GetChunkListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetChunkList();
    }

    public void RemoveChunkAtGridPosition(GridPosition gridPosition, Chunk chunk)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveChunk(chunk);
        OnAnyChunkGetRemoved?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public void ChunkMoveGridPosition(Chunk chunk, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveChunkAtGridPosition(fromGridPosition, chunk);
        AddChunkAtGridPosition(toGridPosition, chunk);
        OnAnyChunkMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public bool HasAnyChunkOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyChunk();
    }

    public Chunk GetChunkAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetChunk();
    }

    public float GetCellSize()
    {
        return cellSize;
    }
}
