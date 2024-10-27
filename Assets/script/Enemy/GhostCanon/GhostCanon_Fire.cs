using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCanon_Fire : MonoBehaviour
{
    [SerializeField] Pooling CannonBallContain;
    [SerializeField] GameObject firepoint;
    public void Shoot()
    {
        GameObject obj = CannonBallContain.GetObject();
        obj.transform.eulerAngles = gameObject.transform.eulerAngles;
        obj.transform.position = firepoint.transform.position;
        obj.SetActive(true);
    }
}
