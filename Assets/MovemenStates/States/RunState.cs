using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{

    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetLayerWeight(1, 0); // Enable layer 1
        movement.anim.SetLayerWeight(2, 1); // Disable layer 2

        movement.isRunning = true;
        movement.anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movement,movement.Walk);
        }

        else if (movement.direction.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }

        if (movement.vInput < 0) movement.currentMoveSpeed = movement.runBackSpeed;
        else movement.currentMoveSpeed = movement.runSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.Jump);
        }

    }

    public void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetLayerWeight(1, 1); // Enable layer 1
        movement.anim.SetLayerWeight(2, 0); // Disabl
        movement.isRunning = false;
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
