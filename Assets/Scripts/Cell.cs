using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    [SerializeField] private Texture2D cellGridImage;

    private int x;
    private int y;
    private int id;    

    public void SetId(int id)
    {
        this.id = id;
    }

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int getId()
    {
        return id;
    }

    public Tuple<int, int> GetId()
    {
        return Tuple.Create(x, y);
    }
}

