using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHelper : MonoBehaviour
{
    [Header("Animation")]
    public float ScaleDuration = .2f;
    public float ScaleBounce = 1.2f;
    public Ease ease = Ease.OutBack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Bounce();
        }
    }

    public void Bounce()
    {
        transform.DOScale(ScaleBounce, ScaleDuration).SetEase(ease). SetLoops(2, LoopType.Yoyo);
    }
}
