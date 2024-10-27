using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AttackingControl : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] MovementControl movementControl;
    [SerializeField] DataControl data;
    [Header("Component")]
    [SerializeField] Animator animator;
    [SerializeField] Transform firepoint;
    [SerializeField] GameObject MainWeapon;
    [Header("Stat")]
    [SerializeField] float CoolDownTime;
    private float CoolDownCounter;
    private bool Subweapon;
    private int CurrentSubWeapon;
    // subweapon list
    [System.Serializable]
    private struct weapon
    {
        public Pooling pool;
        public WeaponSO data;
    }
    [Header("SubWeapon")]
    [SerializeField] weapon[] weaponList;
    private void Awake()
    {
        CurrentSubWeapon = DataGame.Instance.userdata.subWeapon;
    }
    private void OnEnable()
    {
        TurnOff();
    }
    private void Update()
    {
        if (CoolDownCounter < CoolDownTime)
        {
            CoolDownCounter += Time.deltaTime;
        }
        else return;
    }
    // Kich hoat supweapon
    public void ThrowSubWeapon()
    {
        if (Subweapon)
        {
            data.UseMana(weaponList[CurrentSubWeapon].data.ManaCost);
            GameObject obj = weaponList[CurrentSubWeapon].pool.GetObject();
            obj.transform.position = firepoint.position;
            obj.GetComponent<Subweapon>().SetDirection(transform.eulerAngles);
            obj.SetActive(true);
        }
    }
    // Aniamtion
    private void Attack()
    {
        animator.SetFloat("AttackState", System.Convert.ToSingle(movementControl.GroundCheck()));
        animator.SetTrigger("Attack");
    }
    // Input
    // main wreapon
    public void MainAttack(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started && CoolDownCounter >= CoolDownTime)
        {
            Attack();
            AudioManager.Instance.PlayUserSFX("attack");
            MainWeapon.SetActive(true);
            CoolDownCounter = 0;
        }
    }    
    // subweapon
    public void SubAttack(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started && CoolDownCounter >= CoolDownTime && data.currentMP >= weaponList[CurrentSubWeapon].data.ManaCost)
        {
            Attack();
            Subweapon = true;
            CoolDownCounter = 0;
        }
    }
    // thay doi subweapon
    public void ChangeSubWeaponInput(InputAction.CallbackContext ctxt)
    {
        if (ctxt.started)
        {
            int xAxis = (int)ctxt.ReadValue<float>();
            CurrentSubWeapon = CurrentSubWeapon + xAxis;
            if (CurrentSubWeapon > weaponList.Length - 1)
                CurrentSubWeapon = 0;
            else if (CurrentSubWeapon < 0)
                CurrentSubWeapon = weaponList.Length - 1;
            GameUI.Instance.ChangeItemIcon(CurrentSubWeapon);
        }
    }
    // Tat cac component, bool sau khi animation ket thuc
    public void TurnOff()
    {
        MainWeapon.SetActive(false);
        Subweapon = false;
    }
}
