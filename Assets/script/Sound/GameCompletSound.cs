using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletSound : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayGlobalSFX("GameComplete");
    }
}
