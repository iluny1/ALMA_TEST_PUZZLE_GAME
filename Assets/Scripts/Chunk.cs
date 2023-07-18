using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk 
{
    private int id;
    private int xTarget;
    private int yTarget;
    private Texture2D image;

    public Chunk(int id, int xTarget, int yTarget, Texture2D image)
    {
        this.id = id;
        this.xTarget = xTarget;
        this.yTarget = yTarget;
        this.image = image;
    }

    public Tuple<int, int> GetCoordinates()
    {
        return Tuple.Create(xTarget, yTarget);
    }

    public int GetID()
    {
        return id;
    }

    public Texture2D GetImage()
    {
        return image;
    }
}
