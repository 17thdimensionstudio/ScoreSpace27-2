using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOeverManagerScript : MonoBehaviour
{
    public void MainMenu()
    {
        StartCoroutine(FindObjectOfType<SceneTransitioner>().LoadScene(0));

    }
    public void PlayAgain()
    {
        StartCoroutine(FindObjectOfType<SceneTransitioner>().LoadScene(1));

    }
}
