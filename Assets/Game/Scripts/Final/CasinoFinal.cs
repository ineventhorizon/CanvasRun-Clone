using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoFinal : Final
{
    [SerializeField] private Transform rotatingPlatform1, rotatingPlatform2;
    [SerializeField] private Transform spawnPoint;
    private void Update()
    {
        RotatePlatforms();
    }

    protected override void TriggerFinal()
    {
        base.TriggerFinal();
        GameManager.Instance.FinalGame(FinalType.CASINO, spawnPoint);

    }

    private void RotatePlatforms()
    {
        rotatingPlatform1.Rotate(0, 0, 50 * Time.deltaTime);
        rotatingPlatform2.Rotate(0, 0, -50 * Time.deltaTime);
    }
}
