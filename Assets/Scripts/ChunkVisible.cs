using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChunkVisible : MonoBehaviour
{
    [SerializeField] private Chunk chunk;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private GridPosition gridPosition;

    private void Awake()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        transform.position = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public void SetChunk(Chunk chunk)
    {
        this.chunk = chunk;
        SetImage();
    }

    public Chunk GetChunk()
    {
        return chunk;
    }

    public void SetImage()
    {
        Camera mainCamera = FindObjectOfType<Camera>();
        Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 size = new Vector2(5f / PuzzleList.Instance.GetGridSize(), 5f / PuzzleList.Instance.GetGridSize());

        spriteRenderer.sprite = Sprite.Create(chunk.GetImage()
            , Rect.MinMaxRect(0, 0, chunk.GetImage().width, chunk.GetImage().height)
            , new Vector2 (0.5f, 0.5f));

        this.transform.localScale = new Vector2(2.4f, 2.4f);
        this.gameObject.GetComponent<BoxCollider2D>().size = size;
    }    
}
