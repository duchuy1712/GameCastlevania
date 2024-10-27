using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    [Header("Target Player")]
    [SerializeField] GameObject Player;
    [Header("Component")]
    [SerializeField] PlayerInput input;
    [Header("UI")]
    [SerializeField] GameObject ReadyUI;
    [SerializeField] GameObject InGameUI;
    [Header("HealthBar")]
    public Pooling ObjsPooling;
    public List<Image> Objs;
    public Sprite FullSprite, EmptySprite;
    [Header("ItemHeld")]
    public Image Item;
    public Sprite[] IteamList;
    [Header("Energy and Live")]
    public Text Energy;
    public Text Live;
    public int live { get; private set; }
    public int score { get; private set; }
    public Slider LiveUp;
    public int MaxScore = 1;
    private void OnEnable()
    {
        score = DataGame.Instance.userdata.score;
        live = DataGame.Instance.userdata.Live;
        UpdateLive(live);
        UpdateMaxScore(live);
        UpdateScore(score);
        StartLevel();
    }
    //Health Bar
    public void  SetupHealthBar(int MaxHP)
    {
        if (Objs.Count == MaxHP)
            return;
        for (int i = 0; i < MaxHP; i++)
        {
            GameObject obj = ObjsPooling.GetObject();
            obj.SetActive(true);
            Objs.Add(obj.GetComponent<Image>());
        }
    }
    public void UpdateHealthBar(int hp)
    {
        for (int i = 0; i < Objs.Count; i++)
        {
            if (i < hp)
            {
                Objs[i].sprite = FullSprite;
            }
            else
            {
                Objs[i].sprite = EmptySprite;
            }
        }
    }
    //ItemHeld
    public void ChangeItemIcon(int CurrentSubWeapon)
    {
        Item.sprite = IteamList[CurrentSubWeapon];
    }
    //Energy and Live
    public void UpdateEnergy(int mana)
    {
        Energy.text = "E= " + mana.ToString();
    }
    public void UpdateLive(int live)
    {
        Live.text = "P= " + live.ToString();
    }
    public void UpdateScore(int score)
    {
        LiveUp.value = score;
    }
    public void ResetScoreBar()
    {
        score = 0;
        LiveUp.value = score;
    }
    public void UpdateMaxScore(int Live)
    {
        LiveUp.maxValue = Live * 3000;
    }
    //Nhận điểm
    public void GetPoint(int _anmount)
    {
        score += _anmount;
        UpdateScore(score);
        if(score >= LiveUp.maxValue)
        {
            Get1IUP();
        }
    }
    // tăng mạng
    public void Get1IUP()
    {
        live += 1;
        ResetScoreBar();
        UpdateMaxScore(live);
        UpdateLive(live);
    }
    //Mất mạng
    public void Lose1UP()
    {
        live -= 1;
        ResetScoreBar();
        UpdateMaxScore(live);
        UpdateLive(live);
    }
    public void StartLevel()
    {
        StartCoroutine(_StartLevel());
    }
    IEnumerator _StartLevel()
    {
        Player.SetActive(false);
        AudioManager.Instance.LoadMusic();
        InGameUI.SetActive(false);
        ReadyUI.SetActive(true);
        input.actions.Disable();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        InGameUI.SetActive(true);
        ReadyUI.SetActive(false);
        Player.SetActive(true);
        Time.timeScale = 1;
        RoomController.Instance.ActiveObjectRoom();
        input.actions.Enable();
    }
}
