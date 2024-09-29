using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using UnityEngine.Audio;

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
    

    public enum TypeSound
    {
        Master,
        MUSIC,
        SFX,
        UI
    }

    // Utilisez cette �num�ration dans votre classe
    public TypeSound selectedOption;



    private void Start()
    {
        
        GetComponent<Slider>().value = PlayerPrefs.GetFloat($"volume{selectedOption}");
        textValue.text = Mathf.Round(PlayerPrefs.GetFloat($"volume{selectedOption}")*10).ToString();
    }


    public void OnChangeValue() {
        textValue.text = Mathf.Round(GetComponent<Slider>().value*10).ToString();

        PlayerPrefs.SetFloat($"volume{selectedOption}", GetComponent<Slider>().value);

        if (GetComponent<Slider>().value == GetComponent<Slider>().maxValue)
            borne.sprite = fullBorne;
        else
            borne.sprite = emptyBorne;

    }
}
