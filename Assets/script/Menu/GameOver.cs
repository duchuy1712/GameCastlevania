using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Sprite gameOver;
    [SerializeField] private Sprite gameComplete;
    [SerializeField] private Text Score;
    [SerializeField] private Image UI;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    private void OnEnable()
    {
        continueButton.onClick.AddListener(Continue);
        quitButton.onClick.AddListener(Quit);
        Score.text = PlayerManager.Instance.Stat.score.ToString();
        if(PlayerManager.Instance.Stat.live <= 0)
        {
            UI.sprite = gameOver;
        }
        else
        {
            UI.sprite = gameComplete;
        }
        if (PlayerManager.Instance.Stat.score > DataGame.Instance.userdata.highscore)
            DataGame.Instance.userdata.highscore = PlayerManager.Instance.Stat.score;
    }

    private void Continue()
    {
        AudioManager.Instance.PlayGlobalSFX(null);
        Time.timeScale = 1;
        SceneManager.LoadScene(DataGame.Instance.globaldata.Level);
    }
    private void Quit()
    {
        AudioManager.Instance.PlayGlobalSFX(null);
        UIGame.Instance.GameOver.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
