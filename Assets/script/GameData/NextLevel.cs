using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextLevelIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Save();
            DataGame.Instance.globaldata.Level = nextLevelIndex;
            GameCompelete();
        }
    }
    private void Save()
    {
        DataGame.Instance.userdata.Live = PlayerManager.Instance.Stat.live;
        DataGame.Instance.userdata.mainWeaponLv = PlayerManager.Instance.AttackControl.CurrentMainWeapon;
        DataGame.Instance.userdata.subWeapon = PlayerManager.Instance.AttackControl.CurrentSubWeapon;
        DataGame.Instance.userdata.score = PlayerManager.Instance.Stat.score;
    }
    private void GameCompelete()
    {
        AudioManager.Instance.PlayMusic(null);
        AudioManager.Instance.PlayGlobalSFX("Victory");
        Time.timeScale = 0;
        UIGame.Instance.GameOver.SetActive(true);
    }
}
