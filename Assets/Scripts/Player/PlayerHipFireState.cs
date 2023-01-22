using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHipFireState : PlayerAimBaseState
{
    // Start is called before the first frame update
    public override void EnterState(PlayerAim aim)
    {
        aim.currentFov = aim.hipFov;

    }
    public override void UpdateState(PlayerAim aim)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
