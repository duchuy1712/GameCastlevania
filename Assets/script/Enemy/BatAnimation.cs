    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private BatController bat;
    private string current_st;
    const string sleep = "Sleep";
    const string fly = "Fly";

    private void LateUpdate()
    {
        if(bat.attack == false)
        {
            animation_editor(sleep);
        }    
        else
        {

            animation_editor(fly);
        }
    }
    private void animation_editor(string new_st)
    {
        if (new_st == current_st)
            return;
        anim.Play(new_st);
        new_st = current_st;
    }
}
