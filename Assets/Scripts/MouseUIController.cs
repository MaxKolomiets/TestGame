using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class MouseUIController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image right;
    [SerializeField] private Image left;
    [SerializeField] private Image rightBorder;
    [SerializeField] private Image leftBorder;
    [SerializeField] private TextMeshProUGUI counter;
    private Color fullColor = new Color(1, 1, 1, 1);
    private Color emptyColor =  new Color(1, 1, 1, 0.2f);

    private void LeftButtonSetting() {
        if (player.CanUnitAttack.IsSimpleAttackAnimationFinished)
        {
            left.color = fullColor;
            leftBorder.gameObject.SetActive(true);
        }
        else
        {
            left.color = emptyColor;
            leftBorder.gameObject.SetActive(false);
        }
    }
    private void RightButtonSetting() {
        if (player.CanUnitAttack.IsDoubleAttackAnimationFinished && player.CanUnitAttack.IsDoubleAttackCooldownFinished)
        {
            right.color = fullColor;
            counter.text = "";
        }
        else if (player.CanUnitAttack.IsDoubleAttackAnimationFinished && !player.CanUnitAttack.IsDoubleAttackCooldownFinished)
        {
            float lastCooldownTime = player.CanUnitAttack.LastColldownTime;
            float timeEndCooldown = lastCooldownTime + player.DoubleAttackCooldown;
            right.color = new Color(1, 1, 1, Mathf.InverseLerp(lastCooldownTime, timeEndCooldown, Time.time));
            float elapsedTime = timeEndCooldown - Time.time;
            counter.text = elapsedTime.ToString("F1");
            rightBorder.gameObject.SetActive(false);
        }
        else
        {
            right.color = emptyColor;
            counter.text = "";
            rightBorder.gameObject.SetActive(false);
        }
    }

    private void RightBorderSetting() {

            rightBorder.gameObject.SetActive(player.CheckEnemieNearPlayer());
    }

    void Update()
    {
        LeftButtonSetting();
        RightButtonSetting();
        RightBorderSetting();
    }
}
