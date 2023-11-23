using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIGame : Singleton<UIGame>
{
    [SerializeField] private Text live;
    [SerializeField] private Text mana;
    [SerializeField] private Image CurrentSupWeaponIcon;
    [SerializeField] private Sprite[] SupWeaponIconList;
    [SerializeField] private Text Score;
    [SerializeField] private Text HightScore;
    [SerializeField] private Slider HealthBar;
    public GameObject GameOver;
    [SerializeField] private GameObject PauseMenu;
    private void UpdateUi()
    {
        live.text = PlayerManager.Instance.Stat.live.ToString();
        mana.text = PlayerManager.Instance.Stat.mana.ToString();
        Score.text = "Score - " + PlayerManager.Instance.Stat.score;
        HightScore.text = "HiScore - " + DataGame.Instance.userdata.highscore;
        HealthBar.value = PlayerManager.Instance.Stat.hp;
        CurrentSupWeaponIcon.sprite = SupWeaponIconList[PlayerManager.Instance.AttackControl.CurrentSubWeapon];
    }
    private void Update()
    {
        UpdateUi();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale.Equals(0))
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }
        }
    }
}
