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
        action.rHandAim.weight = Mathf.Lerp(action.rHandAim.weight, 1, 10 * Time.deltaTime);
        action.lHandAim.weight = Mathf.Lerp(action.lHandAim.weight, 1, 10 * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.R) && CanReload(action))
        {
            action.SwitchState(action.Reload);
        }
    }

    bool CanReload(ActionStateManager action)
    {
        if (action.ammo.currentAmmo == action.ammo.clipSize) return false;
        if (action.ammo.extraAmmo == 0) return false;
        return true;
    }
}
