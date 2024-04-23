using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class logika : MonoBehaviour
{
    public GameObject gameOverScreen;
    public MapGenerator mapGenerator;

    // Start is called before the first frame update
    void Awake()
    {
        mapGenerator.GenerateValidMap();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restart()
    {
        SceneManager.LoadScene("SampleScene");
        gameOverScreen.SetActive(false);

    }


    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
