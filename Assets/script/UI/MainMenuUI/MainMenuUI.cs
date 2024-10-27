using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class MainMenuUI : MonoBehaviour
{
    private int Level;
    [Header("the UI")]
    public GameObject MenuUI;
    public GameObject GameSetting;
    public GameObject ButtonConfig;
    [Header("First Button")]
    public GameObject MenuUIFirstButton;
    public GameObject GameSettingFirstButton;
    public GameObject BUttonConfigFirstButton;
    public void Start()
    {
        MenuUI.SetActive(true);
        GameSetting.SetActive(false);
        ButtonConfig.SetActive(false);
        EventSystem.current.SetSelectedGameObject(MenuUIFirstButton);
    }
}
