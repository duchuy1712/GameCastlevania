using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    public PauseUI _PauseUI;
    public void OpenSetting()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        _PauseUI.Pause.SetActive(false);
        _PauseUI.Setting.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_PauseUI.SettingFirstButton);
    }
    public void ClosePauseUI()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        _PauseUI.Input.SwitchCurrentActionMap("Player");
        _PauseUI.Pause.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1;
    }
    public void QuitToMenu()
    {
        ClosePauseUI();
        SceneManager.LoadScene(0);
    }
}
