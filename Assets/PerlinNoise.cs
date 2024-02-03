using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinNoise
{

    public static Vector2[,] GenerisiGridVektora(int gridWidth, int gridHeight)
    {
        Vector2[,] grid = new Vector2[gridWidth, gridHeight];

        for (int j = 0; j < gridHeight; j++)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                int ugao = Random.Range(0, 360);
                Quaternion rotacija = Quaternion.AngleAxis(ugao, Vector3.forward);
                grid[i, j] = rotacija * new Vector2(1,1);
            }
        }

        return grid;
    }


    public static float[,] GenerisiNoiseMap(int width, int height, int ppj)
    {
        int gridWidth = (width / ppj)+1;
        int gridHeight = (height / ppj)+1;

        Vector2[,] grid = GenerisiGridVektora(gridWidth, gridHeight);
        float[,] NoiseMap = new float[width, height];

        for (int j = 0; j < height;j++)
        {
            for(int i = 0; i < width; i++)
            {
                float x = (float)i/ppj + 1f/(2f * ppj);
                float y = (float)j/ppj  + 1f/(2f * ppj);   
                NoiseMap[i,j] = GenerisiNoise(x,y, grid);
            }
        }

        return NoiseMap;
    }
    
    public static float GenerisiNoise(float  x, float y, Vector2[,] grid)
    {
        int xLevo = Mathf.FloorToInt(x);
        int xDesno = xLevo + 1;
        float weightX = x - xLevo;


        int yDole = Mathf.FloorToInt(y);
        int yGore = yDole + 1;
        float weightY = y - yDole;


        //Tacka dole levo
        Vector2 vektorDoleLevo = grid[xLevo,yDole];
        Vector2 pozicioniVektor = new Vector2(x-xLevo, y-yDole);
        float vrednostDoleLevo = Vector2.Dot(vektorDoleLevo, pozicioniVektor);

        //Tacka gore levo
        Vector2 vektorGoreLevo = grid[xLevo, yGore];
        pozicioniVektor = new Vector2(x-xLevo, y - yGore);
        float vrednostGoreLevo = Vector2.Dot(vektorGoreLevo, pozicioniVektor);

        //Tacka dole desno
        Vector2 vektorDoleDesno = grid[xDesno, yDole];
        pozicioniVektor = new Vector2(x-xDesno, y-yDole);
        float vrednostDoleDesno = Vector2.Dot(vektorDoleDesno, pozicioniVektor);

        //Tacka gore desno
        Vector2 vektorGoreDesno = grid[xDesno, yGore];
        pozicioniVektor = new Vector2(x - xDesno, y - yGore);
        float vrednostGoreDesno = Vector2.Dot(vektorGoreDesno, pozicioniVektor);

        float vrednostDole = interpoliraj(vrednostDoleLevo, vrednostDoleDesno, weightX);

        float vrednostGore = interpoliraj(vrednostGoreLevo, vrednostGoreDesno, weightX);

        float vrednost = interpoliraj(vrednostDole, vrednostGore, weightY);


        return (vrednost*0.5f + 0.5f);
    }

    public static float interpoliraj(float vrednostA, float vrednostB, float tezina)
    {
        return ( vrednostA + (3f - (2f * tezina)) * tezina * tezina * (vrednostB - vrednostA));
    }

}
