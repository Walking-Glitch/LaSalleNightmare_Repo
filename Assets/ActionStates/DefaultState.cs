using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager action)
    {
      
       
    }

    public override void UpdateState(ActionStateManager action)
    {
        if (!action.moving.isRunning)
        {
            action.rHandAim.weight = Mathf.Lerp(action.rHandAim.weight, 1, 10 * Time.deltaTime);
            action.lHandAim.weight = Mathf.Lerp(action.lHandAim.weight, 1, 10 * Time.deltaTime);

            Debug.Log("Inside update at default state");
        }
        else
        {
            action.rHandAim.weight = 0f;
            action.lHandAim.weight = 0f;

            Debug.Log("Inside else update at default state");
        }
       
        if (Input.GetKeyDown(KeyCode.R) && CanReload(action))
        {
            action.SwitchState(action.Reload);
        }
        else Debug.Log("if statement failed");
    }

    bool CanReload(ActionStateManager action)
    {
        if (action.ammo.currentAmmo == action.ammo.clipSize) return false;
        if (action.ammo.extraAmmo == 0) return false;
        return true;
    }
}
