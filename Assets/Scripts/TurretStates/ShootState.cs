using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : TurretState
{

    public override void Enter(Turret parent)
    {
        //Debug.Log("finally enter ShootState");
        base.Enter(parent);
        parent.Animator.SetBool("Shoot", true);
        if(parent.Animator == null)
        {
            //Debug.Log("shit shootstate is null"); 
        }
        else
        {
            //Debug.Log(parent.Animator.GetBool("Shoot") + " : umm shootstate get bool "); 
            //Debug.Log("shootstate  " + parent.Animator.name);
            
        }
    }
    public override void Update()
    {
        //Debug.Log("ShootState update");
        if (parent.Target != null)
        {
            parent.Rotator.LookAt(parent.Target.position + parent.AimOffset);
        }
        if (!parent.CanSeeTarget(parent.GunBarrels[0].forward, parent.Rotator.position, "Player"))
        {
            parent.ChangeState(new IdleState());
        }
    }
}
