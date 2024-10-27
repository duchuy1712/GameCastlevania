using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType itemType;
    [field: SerializeField] public int value { get; private set; }

    
    [SerializeField] private int CountDown = 3;
    private float Count;
    public enum ItemType
    {
        Health,
        Mana,
        Point,
    }
    private void OnEnable()
    {
        Count = CountDown;
    }
    private void Update()
    {
        if (Count > 0)
            Count -= Time.deltaTime;
        else
            transform.parent.gameObject.SetActive(false);
    }
}
