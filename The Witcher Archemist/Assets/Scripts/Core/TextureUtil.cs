using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureUtil
{
    public static List<Texture2D> SliceTexture(Texture2D tex, int xSliceCount, int ySliceCount)
    {
        List<Texture2D> texList = new List<Texture2D>();

        int sizeX = tex.width / xSliceCount;
        int sizeY = tex.height / ySliceCount;

        for (int y = 0; y < ySliceCount; y++)
        {
            for (int x = 0; x < xSliceCount; x++)
            {
                Texture2D newTex = new Texture2D(sizeX, sizeY, tex.format, false);
                Graphics.CopyTexture(tex, 0, 0, sizeX * x, sizeY * y, sizeX, sizeY, newTex, 0, 0, 0, 0);

                texList.Add(newTex);
            }
        }

        return texList;
    }

    public static Texture2D SliceTexture2(Texture2D tex, int xSliceCount, int ySliceCount)
    {

        //int sizeX = tex.width / xSliceCount;
        //int sizeY = tex.height / ySliceCount;
        Texture2D newTex = new Texture2D(128, 128, tex.format, false);
        Graphics.CopyTexture(tex, 0, 0, 384, 384, 128 , 128 , newTex, 0, 0, 0, 0);
        

        return newTex;
    }

    public static List<Texture2D> SliceTexture3(Texture2D tex, int xSliceCount, int ySliceCount)
    {
        List<Texture2D> texlist = new List<Texture2D>();
        //Texture2D copytex = new Texture2D();

        //int sizeX = tex.width / xSliceCount;
        //int sizeY = tex.height / ySliceCount;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Texture2D newTex = new Texture2D(128, 128, tex.format, false);
                Graphics.CopyTexture(tex, 0, 0, 128 * i, 128 * j, 128, 128, newTex, 0, 0, 0, 0);

                texlist.Add(newTex);
            }

        }


        return texlist;
    }
}
