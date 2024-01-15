using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText; 

    void Update()
    {
        hpText.text = "hp: " +  SceneManager.Instance.Player.Hp.ToString();
    }
}
