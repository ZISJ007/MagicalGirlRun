using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceBar : MonoBehaviour
{
    [SerializeField] private Image gaugeFill;

    private float current = 100f;
    private float max = 100f;

    public void SetGauge(float value)
    {
        current = Mathf.Clamp(value, 0, max);
        gaugeFill.fillAmount = current / max;
    }

    public void SetMax(float value)
    {
        max = value;
        SetGauge(current); // max 바뀌었을 때 갱신
    }
}

