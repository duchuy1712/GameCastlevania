using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCannon_Control : MonoBehaviour
{
    [SerializeField] Animator CanonAnim,SparkAnim;
    public float ReloadTime;
    private float ReloadTimeCount;
    private bool Active;
    public LayerMask player;

    private void OnBecameVisible()
    {
        Active = true;
        ReloadTimeCount = 0;
    }
    private void OnBecameInvisible()
    {
        Active = false;
    }
    private void Update()
    {
        if(ReloadTimeCount < ReloadTime)
        {
            ReloadTimeCount += Time.deltaTime;
        } 
    }
    public void Shoot()
    {
        if(ReloadTimeCount >= ReloadTime && Active)
        {
            CanonAnim.SetTrigger("Fire");
            SparkAnim.SetTrigger("Spark");
            ReloadTimeCount = 0;
        }
    }
}
