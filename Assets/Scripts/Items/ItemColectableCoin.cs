using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public Collider collider;
    public bool collect = false;
    public float lerp = 5f;
    public float minDistance = 1f;
    
    private void Start()
    {
        //CoinsAnimationManager.instance.RegisterCoins(this);
    }
    protected override void OnCollect()
    {
        base.OnCollect();
        collider.enabled = false;
        collect = true;
        //PlayerController.instance.Bounce();
    }
    protected override void Collect()
    {
        base.OnCollect();
        
    }

    private void Update()
    {
        if (collect)
        {
            transform.position = Vector3.Lerp(transform.position,
           PlayerController.instance.transform.position, lerp * Time.deltaTime);
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <
           minDistance)
            {
                
                Destroy(gameObject);
            }
        }
    }

}
