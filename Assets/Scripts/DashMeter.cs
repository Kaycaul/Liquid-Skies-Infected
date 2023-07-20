using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashMeter : MonoBehaviour {

    [SerializeField] Image meter;
    [SerializeField] Color chargingColor, chargedColor;
    [SerializeField] AnimationCurve colorInterpolationCurve;

    public void SetPercent(float percent) {
        meter.fillAmount = percent;
        meter.color = Color.Lerp(chargingColor, chargedColor, colorInterpolationCurve.Evaluate(percent));
        meter.rectTransform.localScale = Vector3.one * Mathf.Lerp(1f, 1.2f, colorInterpolationCurve.Evaluate(percent));
    }

}
