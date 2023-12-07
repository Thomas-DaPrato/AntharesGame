using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderUpdateValue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textValue;

    public void OnChangeValue() {
        textValue.text = GetComponent<Slider>().value.ToString();
    }
}
