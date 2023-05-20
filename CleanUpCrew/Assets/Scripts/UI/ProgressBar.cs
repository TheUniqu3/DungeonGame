using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private int baseValue;
    private int maxValue;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI amount;

    public void SetValues(int _baseValue,int _maxValue)
    {
        baseValue = _baseValue;
        maxValue = _maxValue;

        amount.text = baseValue.ToString();
        CalculateFillAmount();
    }
    private void CalculateFillAmount()
    {
        float fillAmount = (float)baseValue / (float)maxValue;
        slider.value = fillAmount;
    }
}

