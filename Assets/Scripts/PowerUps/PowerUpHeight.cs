using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUpHeight : PowerUpBase
{
    [Header("Power Up Height")]
    public float amountHeight = 2;
    public float animationDuration = .1f;
    public DG.Tweening.Ease ease = DG.Tweening.Ease.OutBack;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.instance.ChangeHeight(amountHeight, duration, animationDuration, ease);
        PlayerController.instance.SetPowerUpText("Flying");
        PlayerController.instance.Bounce2();
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.instance.ResetHeight();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
            
        }
    }
}
