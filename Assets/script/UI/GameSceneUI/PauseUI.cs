using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseUI : MonoBehaviour
{
    public InputActionAsset Control;
    public PlayerInput Input;
    [Header("TheUI")]
    public GameObject Pause;
    public GameObject Setting;
    public GameObject ButtonConfig;
    [Header("TheFirstButton")]
    public GameObject PauseFirstButton;
    public GameObject SettingFirstButton;
    public GameObject ButtonConfigFirstButton;
    public void OpenPauseMenu(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started)
        {
            Input.SwitchCurrentActionMap("UI");
            Pause.SetActive(true);
            EventSystem.current.SetSelectedGameObject(PauseFirstButton);
            Time.timeScale = 0;
        }
    }
    public void ClosePauseMenu(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started)
        {
            Input.SwitchCurrentActionMap("Player");
            Pause.SetActive(false);
            Setting.SetActive(false);
            ButtonConfig.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = 1;
        }
    }
}
