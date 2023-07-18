using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChunkUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Chunk chunk;
    [SerializeField] private Transform contentView;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        contentView = GameObject.Find("ContentScrollView").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BEGIN DRAG!");
        this.rectTransform.SetParent(canvas.transform, true);
        canvasGroup.alpha = 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        OverBoard();
    }

    public void OverBoard()
    {
        Camera mainCamera = FindObjectOfType<Camera>();
        Vector2 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (LevelGrid.Instance.GetChunkListAtGridPosition(LevelGrid.Instance.GetGridPosition(pos)).Count > 0)
        {
            this.transform.SetParent(contentView.transform, true);
            return;
        }

        PuzzleList.Instance.CreateChunkWorld(pos, chunk);
        Destroy(this.gameObject, 0.001f);       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void SetChunk(Chunk chunk)
    {
        this.chunk = chunk;
    }

    public Chunk GetChunk() { return this.chunk; }
}
