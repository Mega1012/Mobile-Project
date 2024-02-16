using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvencible : PowerUpBase
{
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.instance.SetPowerUpText("Invencible");
        PlayerController.instance.SetInvencible();
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.instance.SetInvencible(false);
        PlayerController.instance.SetPowerUpText("");
    }




}
