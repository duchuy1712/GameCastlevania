using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Button startButton;
    public Button continueButton;
    public Button exitButton;
    private int Level;

    private void Awake()
    {
        Level = DataGame.Instance.globaldata.Level;
        if(Level == 0 || Level == 1 )
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }
    private void Start()
    {
        startButton.onClick.AddListener(NewGame);
        continueButton.onClick.AddListener(Continue);
        exitButton.onClick.AddListener(Exit);
    }
    private void NewGame()
    {
        resetLevel();
        SceneManager.LoadScene(Level);
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
    }
    private void Continue()
    {
       SceneManager.LoadScene(Level);
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
    }
    private void Exit()
    {
        Application.Quit();
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
    }
    private void resetLevel()
    {
        DataGame.Instance.resetLevel();
        Level = DataGame.Instance.globaldata.Level;
    }    
}
