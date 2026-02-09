using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInMenu : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private Image image;
    void Start()
    {
        image.DOFade(0, fadeDuration);
    }
    
}
