using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnablePollingEnemy : MonoBehaviour
{
    [SerializeField] Transform Child;
    private void OnEnable()
    {
        Child.gameObject.SetActive(true);
        Child.position = transform.position;
    }
}
