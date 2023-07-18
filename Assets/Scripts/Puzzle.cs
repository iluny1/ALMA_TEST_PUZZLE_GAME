using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle", menuName = "Puzzles/Puzzle")]

public class Puzzle : ScriptableObject
{
    public string Name;
    public string devName;
    public Texture2D Image;
}