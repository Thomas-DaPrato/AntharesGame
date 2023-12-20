using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderUpdateValue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textValue;
    
    [SerializeField]
    private Image borne;
    [SerializeField]
    private Sprite emptyBorne;
    [SerializeField]
    private Sprite fullBorne;

    public void OnChangeValue() {
        textValue.text = GetComponent<Slider>().value.ToString();
        if (GetComponent<Slider>().value == GetComponent<Slider>().maxValue)
            borne.sprite = fullBorne;
        else
            borne.sprite = emptyBorne;

    }
}
