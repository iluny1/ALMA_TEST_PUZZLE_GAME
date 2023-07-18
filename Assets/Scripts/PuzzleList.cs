using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleList : MonoBehaviour
{
    public static PuzzleList Instance;

    [SerializeField] private List<Chunk> chunks = new List<Chunk>();

    [SerializeField] private GameObject prefabChunk;
    [SerializeField] private GameObject prefabChunkUI;
    [SerializeField] private GameObject content;

    private int gridSize;
    private bool isOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void Update()
    {
        DeleteChunkWorld();
    }

    public List<Chunk> GetChunks() { return chunks; }
    public int GetGridSize() { return gridSize; }

    public void AddChunk(Chunk chunk)
    {
        chunks.Add(chunk);
    }    

    public void SetGridSize(int grids)
    {
        gridSize = grids;
    }

    public Chunk GetChunk(int id)
    {
        foreach (Chunk chunk in chunks)
        {
            if (chunk.GetID() == id)
                return chunk;
            else
                continue;
        }

        Debug.LogError($"No chunk with this ID: {id}");
        return null;
    }

    public void CreateChunkWorld(Vector2 position, Chunk chunk)
    {
        GameObject chunkObject = Instantiate(prefabChunk, position, Quaternion.identity);
        chunkObject.GetComponent<ChunkVisible>().SetChunk(chunk);
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(chunkObject.transform.position);
        chunkObject.transform.position = LevelGrid.Instance.GetWorldPosition(gridPosition);
        LevelGrid.Instance.AddChunkAtGridPosition(gridPosition, chunk);

        if (gridPosition.x == chunk.GetCoordinates().Item1
            && gridPosition.y == chunk.GetCoordinates().Item2)
            WinnerLogic.Instance.PlusGood();
    }

    public void CreateChunkUI(Chunk chunk)
    {
        GameObject chunkUI = Instantiate(prefabChunkUI, content.transform);
        chunkUI.GetComponent<ChunkUI>().SetChunk(chunk);
        chunkUI.GetComponent<RawImage>().texture = chunk.GetImage();
    }

    public void DeleteChunkWorld()
    {
        if (!isOver && Input.GetMouseButtonDown(0))
        {
            Camera mainCamera = FindObjectOfType<Camera>();
            Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null
                && LevelGrid.Instance.GetChunkListAtGridPosition(LevelGrid.Instance.GetGridPosition(pos)).Count > 0)
            {
                GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(pos);
                Chunk chunk = LevelGrid.Instance.GetChunkAtGridPosition(gridPosition);
                PuzzleList.Instance.CreateChunkUI(chunk);

                if (gridPosition.x == chunk.GetCoordinates().Item1
                    && gridPosition.y == chunk.GetCoordinates().Item2)
                    WinnerLogic.Instance.MinusGood();


                foreach (GameObject chunkObject in GameObject.FindGameObjectsWithTag("Chunk"))
                {
                    if (chunkObject.GetComponent<ChunkVisible>().GetChunk() == chunk)
                    {
                        Destroy(chunkObject);
                        break;
                    }
                }
                LevelGrid.Instance.RemoveChunkAtGridPosition(gridPosition, chunk);
            }
        }
    }

    public void Win()
    {
        isOver = true;
    }

    public List<Chunk> GenerateRandomList(List<Chunk> listToShuffle)
    {
        listToShuffle = chunks;
        System.Random rand = new System.Random();

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            var k = rand.Next(i + 1);
            var value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }
}
