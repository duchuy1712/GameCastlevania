using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainmenu;
    public void StartGame()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        SceneManager.LoadScene("Level1");
    }
    public void Setting()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        mainmenu.GameSetting.SetActive(true);
        mainmenu.MenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainmenu.GameSettingFirstButton);
    }
    public void Exit()
    {
        Application.Quit();
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
    }


}
