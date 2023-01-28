using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerAimBaseState
{
    // Start is called before the first frame update
    public override void EnterState(PlayerAim aim)
    {
        aim.currentFov = aim.adsFov;

    }
    public override void UpdateState(PlayerAim aim)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) aim.SwitchState(aim.Hip);
    }
}
