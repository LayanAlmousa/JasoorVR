using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadJasoor : MonoBehaviour
{
     public void Playnext()
    {
        SceneManager.LoadSceneAsync("RacingCity"); //play Jasoor environment
    }
}
