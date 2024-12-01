using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadwelcomepae : MonoBehaviour
{
     public void Playnext()
    {
        SceneManager.LoadSceneAsync("WelcometoJasoor"); //backscene
    }
}
