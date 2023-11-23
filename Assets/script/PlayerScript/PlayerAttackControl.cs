using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackControl : MonoBehaviour
{
    private float CoolDownCounter;
    public float AttackCoolDown;
    public SubWeaponData[] SubWeaponList;
    public bool mainAttack { get; private set; }
    public bool subAttack { get; private set; }
    public bool subAttackTrigger => (Input.GetButtonDown("Fire2") && PlayerManager.Instance.Stat.mana >= ManaCost);
    [SerializeField] private WeaponAnimation Weapon;
    [SerializeField] private Transform firepoint;
    public int CurrentMainWeapon { get; private set; }
    public int CurrentSubWeapon { get; private set; }
    public int mainDamge { get; private set; }
    public int subDamage { get; private set; }
    private int ManaCost;
    
    private void Awake()
    {
        CurrentMainWeapon = DataGame.Instance.userdata.mainWeaponLv;
        UpdateMainDamge();
        CurrentSubWeapon = DataGame.Instance.userdata.subWeapon;
        changeSubWeapon(CurrentSubWeapon);
    }
    private void Update()
    {
        if(Time.time >= CoolDownCounter && Time.timeScale.Equals(1))
        {
            if (subAttackTrigger || Input.GetButtonDown("Fire1"))
            {
                Reset();
                PlayerManager.Instance.Animation.AttackState();
                CoolDownCounter = Time.time + 1f / AttackCoolDown;
            }
            IsAttack();
        }
    }
    private void IsAttack()
    {
        if (subAttackTrigger && mainAttack.Equals(false))
        {
            subAttack = true;
        }
        else if (Input.GetButtonDown("Fire1") && subAttack.Equals(false))
        {
            mainAttack = true;
        }
    }
    public void Reset()
    {
        subAttack = false;
        mainAttack = false;
    }
    
    public void MainAttack()
    {
        if (mainAttack.Equals(true))
        {
            Weapon.MainAttack(CurrentMainWeapon);
        }
    }
    public void SubAttack()
    {
        if(subAttack.Equals(true))
        {
            PlayerManager.Instance.Stat.UseMana(ManaCost);
            GameObject obj = SubWeaponList[CurrentSubWeapon].Pooling.GetObject();
            obj.transform.position = firepoint.position;
            obj.SetActive(true);
        }
    }

    // weapon change
    public void UpdateMainWeapon(int amount)
    {
        if (CurrentMainWeapon < PlayerManager.Instance.Stat.baseData.maxWeaponLv)
        {
            CurrentMainWeapon += amount;
            UpdateMainDamge();
        }
        else
            return;
    }
    private void UpdateMainDamge()
    {
        mainDamge = Weapon.Damage[CurrentMainWeapon];
    }
    public void changeSubWeapon(int NewSubWeapon)
    {
        ManaCost = SubWeaponList[NewSubWeapon].ManaCost;
        subDamage = SubWeaponList[NewSubWeapon].Damge;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            AudioManager.Instance.PlayUserSFX("GetItem");
            switch (collision.gameObject.GetComponent<PowerUp>().ItemType)
            {
                case "SubWeapon":
                    if (CurrentSubWeapon.Equals(collision.gameObject.GetComponent<PowerUp>().value))
                        PlayerManager.Instance.Stat.GetMana(5);
                    else
                        CurrentSubWeapon = collision.gameObject.GetComponent<PowerUp>().value;
                    changeSubWeapon(CurrentSubWeapon);
                    break;
                case "MainWeapon":
                    if(CurrentMainWeapon.Equals(PlayerManager.Instance.Stat.baseData.maxWeaponLv))
                        PlayerManager.Instance.Stat.GetMana(5);
                    else
                        UpdateMainWeapon(collision.gameObject.GetComponent<PowerUp>().value);
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
