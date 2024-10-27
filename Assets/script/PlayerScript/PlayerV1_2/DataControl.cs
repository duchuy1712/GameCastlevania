using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DataControl : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator animator;
    [SerializeField] PlayerInput input;
    [Header("Script")]
    [SerializeField] PlayerScriptAble baseStat;
    [Header("Player Save Point")]
    public Vector2 CheckPointPos;
    // mau va mana hien tai
    public int currentHP { private set; get; }
    public int currentMP { private set; get; }
    //
    private bool IsHurt;
    // dat lai mau va mana
    private void OnEnable()
    {
        ResetStat();
    }
    private void ResetStat()
    {
        currentHP = baseStat.maxHp;
        currentMP = baseStat.StartUpMana;
        GameUI.Instance.SetupHealthBar(baseStat.maxHp);
        GameUI.Instance.UpdateHealthBar(currentHP);
        GameUI.Instance.UpdateEnergy(currentHP);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // nhat vat pham
        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            AudioManager.Instance.PlayUserSFX("GetItem");
            switch (item.itemType)
            {
                case (Item.ItemType.Health):
                    Heal(item.value);
                    break;
                case (Item.ItemType.Mana):
                    GetMana(item.value);
                    break;
                case (Item.ItemType.Point):
                    GameUI.Instance.GetPoint(item.value);
                    break;
                default:
                    break;
            }
            collision.transform.parent.gameObject.SetActive(false);
        }
        // bi tan cong
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hurt(collision.gameObject.GetComponent<EnemyStat>().Stat.damge);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        // bi tan cong
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hurt(collision.gameObject.GetComponent<EnemyStat>().Stat.damge);
        }
        if (collision.gameObject.CompareTag("DeathBox"))
        {
            Hurt(11);
        }
    }
    // hồi máu
    public void Heal(int _amount)
    {
        currentHP += _amount;
        if (currentHP >= baseStat.maxHp)
            currentHP = baseStat.maxHp;
        GameUI.Instance.UpdateHealthBar(currentHP);
    }
    // hồi năng lượng
    public void GetMana(int _amount)
    {
        currentMP += _amount;
        if (currentMP >= baseStat.maxMana)
            currentMP = baseStat.maxMana;
        GameUI.Instance.UpdateEnergy(currentMP);
    }
    // Sử dụng năng lượng
    public void UseMana(int _amount)
    {
        if (currentMP >= _amount)
            currentMP -= _amount;
        GameUI.Instance.UpdateEnergy(currentMP);
    }
    //animation
    // bi tan cong
    private void Hurt(int _amount)
    {
        if (IsHurt)
            return;
        currentHP -= _amount;
        IsHurt = true;
        input.actions.Disable();
        GameUI.Instance.UpdateHealthBar(currentHP);
        if (currentHP <= 0) 
        {
            animator.SetTrigger("Death");
        }
        else
            animator.SetTrigger("Hurt");
    }
    // HP > 0
    // nhap nhay sau stun
    IEnumerator FlashEffect()
    {
        input.actions.Enable();
        float flashDelay = 0.0833f;
        for (int i = 0; i < 10; i++)
        {
            sprite.color = Color.clear;
            yield return new WaitForSeconds(flashDelay);
            sprite.color = Color.white;
            yield return new WaitForSeconds(flashDelay);
        }
        IsHurt = false;
    }
    // HP = 0
    public void _ResetGame()
    {
        StopAllCoroutines();
        StartCoroutine(ResetGame());
    }
    IEnumerator ResetGame()
    {
        Time.timeScale = 0;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        currentHP = 0;
        AudioManager.Instance.PlayMusic(null);
        AudioManager.Instance.PlayGlobalSFX("Death");
        yield return new WaitWhile(() => AudioManager.Instance.GlobalSfxSource.isPlaying);
        GameUI.Instance.Lose1UP();
        gameObject.SetActive(false);
        if (GameUI.Instance.live >= 0)
            Death();
        else
            GameOver();
    }
    // mat mang
    private void Death()
    {
        // Cập nhật lại nhân vật
        IsHurt = false;
        input.actions.Enable();
        transform.eulerAngles = Vector3.zero;
        transform.position = CheckPointPos;
        animator.updateMode = AnimatorUpdateMode.Normal;
        // Reset màn chơi và đưa nhân vật về save point
        RoomController.Instance.ResetLevel(transform.position);
        //Bắt đầu Game
        GameUI.Instance.StartLevel();
    }
    // het mang
    private void GameOver()
    {
        // Cập nhật lại nhân vật
        Time.timeScale = 1;
        IsHurt = false;
        animator.updateMode = AnimatorUpdateMode.Normal;
        input.actions.Enable();
        PlayerPrefs.DeleteKey(UserDataKey.score);
        PlayerPrefs.DeleteKey(UserDataKey.Live);
        // Lưu lại Level hiện tại
        DataGame.Instance.globaldata.CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        // Chuyển qua màn hình GameOver
        SceneManager.LoadScene("GameOver");
    }
}
