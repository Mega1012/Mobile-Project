using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCoins : PowerUpBase
{
    [Header("Coin Collector")]
    public float sizeAmount = 7;
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.instance.ChangeCoinCollectorSize(sizeAmount);
        PlayerController.instance.SetPowerUpText("Coins");
    }
    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.instance.ChangeCoinCollectorSize(1);
    }
}
