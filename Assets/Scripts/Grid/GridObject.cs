using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem<GridObject> gridSystem;
    private GridPosition gridPosition;
    private List<Chunk> ChunkList;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        ChunkList = new List<Chunk>();
    }

    public override string ToString()
    {
        string chunkString = "";
        foreach (Chunk chunk in ChunkList)
        {
            chunkString += chunk + "\n";
        }

        return gridPosition.ToString() + "\n" + chunkString;
    }

    public void AddChunk(Chunk chunk)
    {
        ChunkList.Add(chunk);
    }

    public void RemoveChunk(Chunk chunk)
    {
        ChunkList.Remove(chunk);
    }

    public List<Chunk> GetChunkList()
    {
        return ChunkList;
    }

    public bool HasAnyChunk()
    {
        return ChunkList.Count > 0;
    }

    public Chunk GetChunk()
    {
        if (HasAnyChunk())
        {
            return ChunkList[0];
        }
        else
        {
            return null;
        }
    }
}
