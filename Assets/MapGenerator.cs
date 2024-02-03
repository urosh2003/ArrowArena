using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapHeight = 100;
    public int mapWidth = 200;

    public int ppj = 50;

    public float kamenUslov = 0.65f;
    public float vodaUslov = 0.35f;
    public float bureUslovDole = 0.495f;
    public float bureUslovGore = 0.505f;

    public void GenerateValidMap()
    {
        while (true)
        {
            float[,] noiseMap = PerlinNoise.GenerisiNoiseMap(mapWidth, mapHeight, ppj);

            MapDisplay display = FindObjectOfType<MapDisplay>();

            bool valid = display.drawMap(noiseMap, kamenUslov, vodaUslov, bureUslovDole, bureUslovGore);

            if (valid)
            {
                break;
            }
        }
    }

    public void GenerateMap()
    {
        float[,] noiseMap = PerlinNoise.GenerisiNoiseMap(mapWidth, mapHeight, ppj);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.drawMap(noiseMap, kamenUslov, vodaUslov, bureUslovDole,bureUslovGore);

    }

    public void GenerateNoiseMap()
    {
        float[,] noiseMap = PerlinNoise.GenerisiNoiseMap(mapWidth, mapHeight, ppj);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.drawNoiseMap(noiseMap);

    }


}
