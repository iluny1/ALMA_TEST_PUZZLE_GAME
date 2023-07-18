using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerLogic : MonoBehaviour
{
    public static WinnerLogic Instance;

    [SerializeField] private int goodChunks = 0;
    [SerializeField] private int target;
    [SerializeField] private GameObject RightUI; 
    [SerializeField] private GameObject EndUI; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        CheckTarget();
    }

    public void SetTarget(int target) { this.target = target; }

    public void PlusGood() { goodChunks++; }
    public void MinusGood() { goodChunks--; }

    private void CheckTarget()
    {
        if (goodChunks == target)
        {
            RightUI.SetActive(false);
            EndUI.SetActive(true);
            PuzzleList.Instance.Win();
        }
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnAgain()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
