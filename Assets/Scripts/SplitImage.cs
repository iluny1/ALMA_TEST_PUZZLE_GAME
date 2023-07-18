using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitImage : MonoBehaviour
{
    
    [SerializeField] private List<Texture2D> imageTiles;
    private Texture2D image;

    public enum Divider : int
    {       
        FourByFour = 4,
        FiveByFive = 5,
        SixBySix = 6
    }

    public void Split(Texture2D image, int divX, int divY)
    {
        int height = image.height / divY;
        int width = image.width / divX;

        bool perfectWidth = image.width % divX == 0;
        bool perfectHeight = image.height % divY == 0;

        int lastWidth = width;
        if (!perfectWidth)
        {
            lastWidth = image.width - ((image.width / width) * width);
        }

        int lastHeight = height;
        if (!perfectHeight)
        {
            lastHeight = image.height - ((image.height / height) * height);
        }

        int widthPartsCount = image.width / width + (perfectWidth ? 0 : 1);
        int heightPartsCount = image.height / height + (perfectHeight ? 0 : 1);

        for (int i = 0; i < widthPartsCount; i++)
        {
            for (int j = 0; j < heightPartsCount; j++)
            {
                int tileWidth = i == widthPartsCount - 1 ? lastWidth : width;
                int tileHeight = j == heightPartsCount - 1 ? lastHeight : height;

                Texture2D g = new Texture2D(tileWidth, tileHeight);
                g.SetPixels(image.GetPixels(i * width, j * height, tileWidth, tileHeight));
                g.Apply();
                imageTiles.Add(g);
            }
        }
    }

    public void SetOriginalImage(Texture2D image)
    {
        this.image = image;
    }

    public List<Texture2D> GetImages()
    {
        return imageTiles;
    }
}
