using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nexttoinstructions : MonoBehaviour
{
    public void Playnext()
    {
        SceneManager.LoadSceneAsync("scene after ready button"); //playnextscene
    }
}
