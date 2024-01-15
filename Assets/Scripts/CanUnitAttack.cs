using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class CanUnitAttack
{
    private bool simpleAttackAnimationNow = false;

    private bool doubleAttackAnimationNow = false;
    private bool doubleAttackCooldownNow = false;
    private float lastCooldownTime = 0;
    public float LastColldownTime => lastCooldownTime;

    public bool IsSimpleAttackAnimationFinished => !simpleAttackAnimationNow;
    public bool IsDoubleAttackAnimationFinished => !doubleAttackAnimationNow;
    public bool IsDoubleAttackCooldownFinished => !doubleAttackCooldownNow;

    public void StartSimpleAttack()
    {
        simpleAttackAnimationNow = true;
    }
    public void EndSimpleAttack()
    {
        simpleAttackAnimationNow = false;
    }
    public void StartDoubleAttack()
    {
        doubleAttackAnimationNow = true;
    }
    public async void EndDoubleAttack(float cooldown)
    {
        doubleAttackAnimationNow = false;
        doubleAttackCooldownNow = true;
        lastCooldownTime = Time.time;
        await WaitCooldown(cooldown);
        doubleAttackCooldownNow = false;
    }
    private async Task WaitCooldown(float seconds)
    {
        await Task.Delay((int)(1000 * seconds));
    }
}
