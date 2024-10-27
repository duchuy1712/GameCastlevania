using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public RectTransform rectTransform;
    private void OnEnable()
    {
        rectTransform.localScale = Vector3.one ;
        rectTransform.localPosition = Vector3.zero;
    }
}
