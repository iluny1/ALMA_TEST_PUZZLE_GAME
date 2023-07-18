using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public Puzzle PuzzleInfo;
    public GameObject MenuLogic;
    
    public void Click()
    {
        MenuLogic.GetComponent<PuzzleChooseScreen>().SwitchMenu(PuzzleInfo);
    }
}
