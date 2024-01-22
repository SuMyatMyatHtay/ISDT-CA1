
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

//---------------------------------------------------------------------------------
// Author		: SU MYAT MYAT HTAY 
// Date  		: 2023-02-12
// Description	: Slider Animate as it shot the gun. 
//---------------------------------------------------------------------------------
[RequireComponent(typeof(Gun))]
public class AnimateCharging : MonoBehaviour
{
    public bool isLastBullet;
    
    [SerializeField] private Transform pistolTop;
    [SerializeField] private Vector3 chargeBackPosStart;
    [SerializeField] private Vector3 chargeBackPosEnd;
    [SerializeField] private float tweenTime;

    public void StartCharge()
    {
        Debug.Log("Start Charge");
        //Tween LocalPosition of Charging Handle to End of Slide Position
        Tween.LocalPosition(pistolTop, chargeBackPosStart, tweenTime);
    }

    public void EndCharge()
    {
        Debug.Log("End Charge");
        //Tween LocalPosition of Charging Handle to End Position (Original Position)
        Tween.LocalPosition(pistolTop, chargeBackPosEnd, tweenTime);
    }

    public void AnimateCharge()
    {
        Sequence mySequence = Sequence.Create();
        mySequence.ChainCallback(() => { // fire event
            StartCharge();
        });

        if (!isLastBullet) // Make ChargeHandle stop at the back.
        {
            mySequence.ChainDelay(tweenTime); // delay everything by Showtime in second
            mySequence.ChainCallback(() => { // fire event
                EndCharge();
            });
        }

    }

}
