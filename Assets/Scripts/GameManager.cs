using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    #region Unity_function
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_transition

    public void StartGame()
    {
        Debug.Log("started game");
        SceneManager.LoadScene("SampleScene");
    }
     public void LoseGame()
    {
        SceneManager.LoadScene("LoseScene");
    }
     public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }
     public void MainMenue()
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}