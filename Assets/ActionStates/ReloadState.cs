using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager action)
    {
        Debug.Log("asdadsadsad");

        action.rHandAim.weight = 0f;
        action.lHandAim.weight = 0f;
        action.anim.SetTrigger("Reload");
    }

    public override void UpdateState(ActionStateManager action)
    {
       
    }
}
