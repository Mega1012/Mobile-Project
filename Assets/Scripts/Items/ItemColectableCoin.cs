using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemColectableBase
{
    public Collider collider;
    public bool collect = false;
    public float lerp = 5f;
    public float minDistance = 1f;


    
    private void Start()
    {
        CoinsAnimatorManager.instance.RegisterCoin(this);
    }

    protected override void OnCollect()
    {
        PlayerController.instance.Bounce();
        base.OnCollect();
        collider.enabled = false;
        collect = true;
    }
    protected override void Collect()
    {
        base.OnCollect();
        PlayerController.instance.Bounce2();
    }

    private void Update()
    {
        if (collect)
        {
            
            transform.position = Vector3.Lerp(transform.position,
            PlayerController.instance.transform.position, lerp * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < minDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    

}
