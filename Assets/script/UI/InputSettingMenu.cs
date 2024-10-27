using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InputSettingMenu : MonoBehaviour
{
    public InputActionAsset Control;
    [Header("The UI")]
    public GameObject SettingUI;
    public GameObject ButtonConfigUI;
    [Header("TheUI")]
    public GameObject ButtonConfigFirstButton;
    public GameObject SettingUIFirstButton;
    public void BackToSetting()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        SettingUI.SetActive(true);
        ButtonConfigUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(SettingUIFirstButton);
    }
    public void ResetAllBinding()
    {
        AudioManager.Instance.PlayGlobalSFX("ButtonClick");
        foreach (InputActionMap map in Control.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
    }
}
