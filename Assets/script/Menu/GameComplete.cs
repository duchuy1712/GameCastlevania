using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameComplete : MonoBehaviour
{
    [SerializeField] private Button Quit;
    [SerializeField] private Text Score;
    private void OnEnable()
    {
        Quit.onClick.AddListener(Exit);
        if (DataGame.Instance.userdata.score > DataGame.Instance.userdata.highscore)
        { Score.text = DataGame.Instance.userdata.score.ToString() + " (new highscore!)";
             DataGame.Instance.userdata.highscore = DataGame.Instance.userdata.score;
        }
        else
            Score.text = DataGame.Instance.userdata.score.ToString();
    }

    private void Exit()
    {
        DataGame.Instance.resetLevel();
        SceneManager.LoadScene(0);
    }
}
