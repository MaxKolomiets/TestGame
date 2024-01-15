using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WaveCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waves;

    public void SetCounter(int currentWave, int Waves) {
        waves.text = "wave: " + currentWave + "/" + Waves;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
