using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    private int NextLevelIndex => SceneManager.GetActiveScene().buildIndex + 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex.Equals(SceneManager.sceneCountInBuildSettings - 1))
            {
                StartCoroutine(GameComplete(1));
            }
            else
                StartCoroutine(GameComplete(NextLevelIndex));
            sprite.enabled = false;
        }
    }
    IEnumerator GameComplete(int _NextLevel)
    {
        AudioManager.Instance.PlayMusic(null);
        AudioManager.Instance.PlayGlobalSFX("Victory");
        Time.timeScale = 0;
        yield return new WaitWhile(() => AudioManager.Instance.GlobalSfxSource.isPlaying);
        Time.timeScale = 1;
        SceneManager.LoadScene(_NextLevel);
    }
}
