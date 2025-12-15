using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _tuto;
    [SerializeField] private Slider _slider;
    private float _timePress;
    void Update()
    {
        if (_gameManager.isPressingA)
        {
            _timePress += Time.unscaledDeltaTime;
            _slider.value = _timePress;
        }
        else if (_timePress > 0)
        {
            _timePress -= Time.unscaledDeltaTime;
            _slider.value = _timePress;
        }
        if (_slider.value >= _slider.maxValue)
        {
            _gameManager.tutoOn = false;
            _tuto.SetActive(false);
            _timePress = 0;
            Time.timeScale = 1;
        }
    }
}
