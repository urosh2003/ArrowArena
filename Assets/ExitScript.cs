using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{   public void doExitGame()
    {
        Application.Quit();
    }
    public void restart()
    {
        Debug.Log("Cao");
        SceneManager.LoadScene("SampleScene");
    }
}
