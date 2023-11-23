using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public bool immortal { get; private set; }
    public bool stun { get; private set; }
    public bool Reset { get; private set; }
    public float immortalTime;
    private float immortalTimeCount;
    public float stunTime;
    private float stunTimeCount;
    public float ResetTime;
    private float ResetTimeCount;
    //dữ liệu lấy từ prefab
    public int mana { get; private set; }
    public int score { get; private set; }
    private int highScore;
    private int damage;
    //dữ liệu được lấy từ scriptable object
    public int live { get; private set; }
    public int hp { get; private set; }
    public float speed { get; private set; }
    public float maxJumpHeight { get; private set; }
    public float maxJumpTime { get; private set; }
    public float SliceForce { get; private set; }
    public PlayerScriptAble baseData;

    private void OnEnable()
    {
        Setupdata();
    }
    private void Update()
    {
        if (Reset.Equals(true))
            ResetGame();
        if (immortal.Equals(true))
            getHit();
    }
    private void ResetGame()
    {
        if (ResetTimeCount > 0)
        {
            ResetTimeCount -= Time.deltaTime;
        }
        else
        {
            Reset = false;
            Death();
        }
    }
    private void Death()
    {
        Physics2D.IgnoreLayerCollision(7, 9, false);
        live -= 1;
        if (live > 0)
        {
           SceneManager.LoadScene(DataGame.Instance.globaldata.Level);
           DataGame.Instance.userdata.score = score;
           PlayerPrefs.DeleteKey(UserDataKey.mainWeapon);
           PlayerPrefs.DeleteKey(UserDataKey.subWeapon);
           DataGame.Instance.userdata.Live = live;
        }
        else if(live <= 0)
        {
            PlayerPrefs.DeleteKey(UserDataKey.score);
            PlayerPrefs.DeleteKey(UserDataKey.Live);
            GameOver();
        }
    }
    private void Setupdata()
    {
        hp = baseData.maxHp;
        live = DataGame.Instance.userdata.Live;
        mana = DataGame.Instance.userdata.mana;
        score = DataGame.Instance.userdata.score;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            AudioManager.Instance.PlayUserSFX("GetItem");
            switch(collision.gameObject.GetComponent<PowerUp>().ItemType)
            {
                case "Health":
                    Heal(collision.gameObject.GetComponent<PowerUp>().value);
                    break;
                case "Mana":
                    GetMana(collision.gameObject.GetComponent<PowerUp>().value);
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
    private void getHit()
    {
        if (stunTimeCount > 0)
        {
            stunTimeCount -= Time.deltaTime;
        }
        else
        {
            stun = false;
        }
        if (immortalTimeCount > 0)
        {
            immortalTimeCount -= Time.deltaTime;
        }
        else
        {
            Physics2D.IgnoreLayerCollision(7, 9, false);
            immortal = false;
        }
    }
    public void hurt(int _amount)
    {
        hp -= _amount;
        Physics2D.IgnoreLayerCollision(7, 9, true);
        if (hp <= 0)
        {
            Reset = true;
            ResetTimeCount = ResetTime;
            hp = 0;
            AudioManager.Instance.PlayMusic(null);
            AudioManager.Instance.PlayGlobalSFX("Death");
            PlayerManager.Instance.Animation.DeathAnimation();
        }
        else if(immortal.Equals(false))
        {
            stunTimeCount = stunTime;
            immortalTimeCount = immortalTime;
            immortal = true;
            stun = true;
        }
    }
    private void GameOver()
    {
        AudioManager.Instance.PlayMusic(null);
        AudioManager.Instance.PlayGlobalSFX("GameOver");
        Time.timeScale = 0;
        UIGame.Instance.GameOver.SetActive(true);
    }
    private void Heal(int _amount)
    {
        hp += _amount;
        if (hp >= baseData.maxHp)
            hp = baseData.maxHp;
    }
    public void GetMana(int _amount)
    {
        mana += _amount;
        if (mana >= baseData.maxMana)
           mana = baseData.maxMana;
    }
    public void UseMana(int _amount)
    {
        if(mana >= _amount)
            mana -= _amount;
    }
    public void GetPoint(int _amount)
    {
        score += _amount;
    }
}