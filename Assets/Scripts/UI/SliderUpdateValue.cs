using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class SliderUpdateValue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textValue;
    [SerializeField]
    private MMSoundManagerTrackVolumeSlider slider;

    [SerializeField]
    private Image borne;
    [SerializeField]
    private Sprite emptyBorne;
    [SerializeField]
    private Sprite fullBorne;

    private void Start()
    {
        
    }


    public void OnChangeValue() {
        textValue.text = (Mathf.Round(GetComponent<Slider>().value*10)).ToString();
        if (GetComponent<Slider>().value == GetComponent<Slider>().maxValue)
            borne.sprite = fullBorne;
        else
            borne.sprite = emptyBorne;

    }
}
