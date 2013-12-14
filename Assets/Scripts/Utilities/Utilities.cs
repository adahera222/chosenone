using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utilities
{

    public static Texture2D CreateBlankTexture(Color color, int size = 4)
    {
        // create empty texture
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGB24, false, true);

        // get all pixels as an array
        var cols = texture.GetPixels();
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i] = color;
        }

        // important steps to save changed pixel values
        texture.SetPixels(cols);
        texture.Apply();

        return texture;
    }
}