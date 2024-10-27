using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Setting : MonoBehaviour
{
    [Header("The UI")]
    public GameObject SettingUI;
    public GameObject PreviousUI;
    public GameObject ButtonConfig;
    [Header("First UI Button")]
    public GameObject SettingFirstButton;
    public GameObject PreviousFirstButton;
    public GameObject ButtonConfigFirstButton;
    public void BackToPauseUI()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        PreviousUI.SetActive(true);
        SettingUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(PreviousFirstButton);
    }
    public void ButtonConfigUI()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        ButtonConfig.SetActive(true);
        SettingUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(ButtonConfigFirstButton);
    }
}
