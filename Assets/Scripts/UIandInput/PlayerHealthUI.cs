using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI playercurrentHealthTMP;

    public Slider PlayercurrentHealthSlider;

    static public int currenthealth;
    static public int maxhealth;


    private void Awake()
    {
    }
    private void Update()
    {
        UpdateHealth();
        UpdateHealthText();
    }

    private void UpdateHealth()
    {
        PlayercurrentHealthSlider.maxValue = maxhealth;
        PlayercurrentHealthSlider.value = currenthealth;
    }

    private void UpdateHealthText()
    {
        playercurrentHealthTMP.text = currenthealth + "/" + maxhealth;
    }
}
