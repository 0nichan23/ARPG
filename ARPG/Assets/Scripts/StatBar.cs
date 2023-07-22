using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    public void UpdateBar(int maxValue, int curValue)
    {
        text.text = $"{curValue}/{maxValue}";
        slider.maxValue = maxValue;
        slider.value = curValue;
    }

}
