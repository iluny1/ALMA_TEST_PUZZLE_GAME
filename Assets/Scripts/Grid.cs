using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
