using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void Start()
    {
        SoundManagerScript.SoundInstance.MainMenuMusic(true);
    }
    public void StartFun()
    {
        SceneManager.LoadScene("_Scene");
        SoundManagerScript.SoundInstance.MainMenuMusic(false);
    }
    public void QuitFun()
    {
        Application.Quit();
    }
}
