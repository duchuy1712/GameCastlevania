using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWizard_Control : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject GreenRay,theOrb;
    [SerializeField] Pooling[] FireBallContainer;
    [SerializeField] Transform[] FirePoint;
    [SerializeField] Transform[] Position;
    [SerializeField] Transform Spell2Position,OrbPosition;
    [SerializeField] int MaxSpell1Count;
    [SerializeField] float GreenRayTime;
    private Vector3 CurrentPos;
    private int Spell1Count;
    private void OnEnable()
    {
        CurrentPos = Position[0].position;
    }
    //intro

    //spell 1
    public void ActiveGreenRay()
    {
        StartCoroutine(procedureActiveGreenRay());
    }
    IEnumerator procedureActiveGreenRay()
    {
        GreenRay.SetActive(true);
        yield return new WaitForSeconds(GreenRayTime);
        GreenRay.SetActive(false);
    }    
    //spell 2
    public void ShootFireBall()
    {
        for(int i =0;i< FirePoint.Length;i++)
        {
            GameObject obj = FireBallContainer[i].GetObject();
            obj.transform.eulerAngles = gameObject.transform.eulerAngles;
            obj.transform.position = FirePoint[i].position;
            obj.SetActive(true);
        }
    }
    //teleport
    public void Teleport()
    {
        if (Spell1Count < MaxSpell1Count)
        {
            Transform y = Position[Random.Range(0, Position.Length)];
            while (y.position == CurrentPos)
            {
                y = Position[Random.Range(0, Position.Length)];
            }
            transform.position = y.position;
            CurrentPos = y.position;
            Spell1Count += 1;
            anim.SetInteger("SpellCast", 1);
        }
        else
        {
            transform.position = Spell2Position.position;
            Spell1Count = 0;
            anim.SetInteger("SpellCast", 0);
        }
    }
    public void TurnAround()
    {
        if (CurrentPos.x - Spell2Position.position.x < 0)
            transform.eulerAngles = Vector3.zero;
        else
            transform.eulerAngles = Vector3.up * 180;
    }
    private void OnDisable()
    {
        theOrb.transform.position = OrbPosition.position;
        theOrb.SetActive(true);
    }
}
