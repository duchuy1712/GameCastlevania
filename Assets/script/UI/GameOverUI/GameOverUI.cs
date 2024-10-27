using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(DataGame.Instance.globaldata.CurrentLevel);        
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
