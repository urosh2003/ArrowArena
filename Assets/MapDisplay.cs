using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public Tilemap kamenje;
    public Tilemap vode;
    public Tilemap burad;
    public Tile kamen;
    public Tile voda;
    public Tile bure;
    int[,] putanja;

    public bool drawMap(float[,] noiseMap, float kamenUslov, float vodaUslov, float bureUslovDole, float bureUslovGore)
    {
        putanja = new int[20, 10];
        int width = noiseMap.GetLength(0) / 10;
        int height = noiseMap.GetLength(1) / 10;

        Vector3Int pozicija;

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];


        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                float sum = 0;
                for(int k = 0; k < 10; k++)
                {
                    for( int l = 0; l < 10; l++)
                    {
                        sum += noiseMap[i*10+l, j*10+k];
                    }
                }
                sum = sum / 100;
                pozicija = new Vector3Int(i-10, j-5, 0);
                if (sum >= kamenUslov)
                {
                    colorMap[j * width + i] = Color.black;
                    putanja[i, j] = 1;
                    kamenje.SetTile(pozicija, kamen);
                    vode.SetTile(pozicija, null);
                    burad.SetTile(pozicija, null);
                }
                else if (sum <= vodaUslov)
                {
                    colorMap[j * width + i] = Color.white;
                    putanja[i, j] = 1;
                    vode.SetTile(pozicija, voda);
                    kamenje.SetTile(pozicija, null);
                    burad.SetTile(pozicija, null);
                }
                else if (sum <= bureUslovGore && sum >= bureUslovDole)
                {
                    burad.SetTile(pozicija, bure);
                    kamenje.SetTile(pozicija, null);
                    vode.SetTile(pozicija, null);
                }
                else
                {
                    colorMap[j*width + i] = Color.gray;
                    kamenje.SetTile(pozicija, null);
                    vode.SetTile(pozicija, null);
                    burad.SetTile(pozicija, null);
                }
            }
        }
        putanja[15, 5] = 2;
        putanja[4, 5] = 3;

        pozicija = new Vector3Int(5, 0, 0);

        kamenje.SetTile(pozicija, null);
        vode.SetTile(pozicija, null);
        burad.SetTile(pozicija, null);

        pozicija = new Vector3Int(-6, 0, 0);

        kamenje.SetTile(pozicija, null);
        vode.SetTile(pozicija, null);
        burad.SetTile(pozicija, null);

        texture.SetPixels(colorMap);
        texture.Apply();


        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width,1,height);

        return checkValid(putanja);
    }

    public void drawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        
        for(int j = 0; j < height; j++)
        {
            for(int i = 0; i < width; i++)
            {
                colorMap[j * width + i] = Color.Lerp(Color.black, Color.white, noiseMap[i,j]);
            }
        }
        

        texture.SetPixels(colorMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);
    }

    bool checkValid(int[,] mapa)
    {
        for (int k = 0; k < 200; k++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (mapa[i, j] == 2)
                    {
                        if (i != 0)
                        {
                            if (mapa[i-1,j] == 3)
                            {
                                return true;
                            }
                            else if (mapa[i - 1, j] == 0)
                            {
                                mapa[i - 1, j] = 2;
                            }
                        }
                        if (i != 19)
                        {
                            if (mapa[i + 1, j] == 3)
                            {
                                return true;
                            }
                            else if (mapa[i + 1, j] == 0)
                            {
                                mapa[i + 1, j] = 2;
                            }
                        }
                        if (j != 0)
                        {
                            if (mapa[i, j-1] == 3)
                            {
                                return true;
                            }
                            else if (mapa[i , j-1] == 0)
                            {
                                mapa[i , j-1] = 2;
                            }
                        }
                        if (j != 9)
                        {
                            if (mapa[i, j + 1] == 3)
                            {
                                return true;
                            }
                            else if (mapa[i, j + 1] == 0)
                            {
                                mapa[i, j + 1] = 2;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
}
