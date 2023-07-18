using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridType", menuName = "Puzzles/GridType")]

public class MenuGrid : ScriptableObject
{
    public int DivX;
    public int DivY;
    public Texture2D Image;
    public string Name;

    [SerializeField]public enum GridType
    {    
        FourByFour,
        FiveByFive,
        SixBySix
    }
}
