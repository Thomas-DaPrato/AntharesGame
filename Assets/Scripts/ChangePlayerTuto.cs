using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class ChangePlayerTuto : MonoBehaviour
{
    [Header("UI Elements")] [SerializeField]
    private Image p1Indicator;

    [SerializeField] private Image p2Indicator;
    [SerializeField] private RectTransform p1Bar;
    [SerializeField] private RectTransform p2Bar;
    [SerializeField] private List<DeviceColorTextHandler> deviceColorTextHandlers;
    [SerializeField] private List<DeviceColorHandler> deviceColorHandlers;

    [Header("Sprite")] [SerializeField] private Sprite indicated;
    [SerializeField] private Sprite notIndicated;
    [SerializeField] List<Image> whiteBarP1;
    [SerializeField] List<Image> redBarP1;
    [SerializeField] List<Image> whiteBarP2;
    [SerializeField] List<Image> redBarP2;

    [Header("Value")] [SerializeField] private float delayChangeBar = 3f;
    [SerializeField] private float delayBeforeAnimation = 2f;
    [SerializeField] private float delayBetweenAnimation;
    [SerializeField] private float speed;
    [SerializeField] private float posXP1Activate;
    [SerializeField] private float posXP1Desactivate;
    [SerializeField] private float posXP2Activate;
    [SerializeField] private float posXP2Desactivate;

    [Header("Image to Grey")]
    [SerializeField] private DeviceGreyImageHandler imageP1Light;
    [SerializeField] private DeviceGreyImageHandler imageP1Medium;
    [SerializeField] private DeviceGreyImageHandler imageP1Heavy;
    [SerializeField] private DeviceGreyImageHandler imageP2Light;
    [SerializeField] private DeviceGreyImageHandler imageP2Medium;
    [SerializeField] private DeviceGreyImageHandler imageP2Heavy;

    [Header("Animation")] [SerializeField] private MMF_Player mediumP1Animation;
    [SerializeField] private MMF_Player lightP1Animation;
    [SerializeField] private MMF_Player mediumP2Animation;
    [SerializeField] private MMF_Player lightP2Animation;

    [Header("Colors")] [SerializeField] Color endLine12Playstation;
    [SerializeField] Color endLine12Xbox;
    [SerializeField] Color endLine12Switch;
    [SerializeField] Color endLine12PC;
    [SerializeField] Color endLine3Playstation;
    [SerializeField] Color endLine3Xbox;
    [SerializeField] Color endLine3Switch;
    [SerializeField] Color endLine3PC;

    void Awake()
    {
        imageP1Light.SetGreyBool(false);
        print("Light " + imageP1Light.P1);
        imageP1Medium.SetGreyBool(true);
        print("Medium " + imageP1Medium.P1);
        imageP1Heavy.SetGreyBool(true);
        print("Heavy " + imageP1Heavy.P1);

    }
    void Start()
    {
        /*materialP1Light.SetInt("_Grey", 0);
        materialP1Medium.SetInt("_Grey", 1);
        materialP1Heavy.SetInt("_Grey", 1);
        materialP2Light.SetInt("_Grey", 0);
        materialP2Medium.SetInt("_Grey", 1);
        materialP2Heavy.SetInt("_Grey", 1);*/

        
        /*imageP2Light.SetGreyBool(false);
        imageP2Medium.SetGreyBool(true);
        imageP2Heavy.SetGreyBool(true);*/
        StartCoroutine(DoAfterDelay(delayBeforeAnimation, FirstAnimationP1Bar));
    }

    public IEnumerator DoAfterDelay(float delaySeconds, System.Action thingToDo)
    {
        yield return new WaitForSecondsRealtime(delaySeconds);
        thingToDo();
    }

    private void ChangeToP1()
    {
        p1Bar.DOAnchorPosX(posXP1Activate, speed).SetUpdate(true);
        p2Bar.DOAnchorPosX(posXP2Desactivate, speed).SetUpdate(true).OnComplete(() =>
            StartCoroutine(DoAfterDelay(delayBeforeAnimation, FirstAnimationP1Bar)));
        p1Indicator.sprite = indicated;
        p2Indicator.sprite = notIndicated;
        ChangePlayerColorHandler();
    }

    private void ChangeToP2()
    {
        p1Bar.DOAnchorPosX(posXP1Desactivate, speed).SetUpdate(true);
        p2Bar.DOAnchorPosX(posXP2Activate, speed).SetUpdate(true).OnComplete(() =>
            StartCoroutine(DoAfterDelay(delayBeforeAnimation, FirstAnimationP2Bar)));
        p1Indicator.sprite = notIndicated;
        p2Indicator.sprite = indicated;
        ChangePlayerColorHandler();
    }

    private void ResetAnimationP1Bar()
    {
        for (int i = 0; i < 4; i++)
            whiteBarP1[i].enabled = true;
        foreach (Image image in redBarP1)
            image.fillAmount = 1;
        /*materialP1Light.SetInt("_Grey", 0);
        materialP1Medium.SetInt("_Grey", 1);
        materialP1Heavy.SetInt("_Grey", 1);*/
        imageP1Light.SetGreyBool(false);
        imageP1Medium.SetGreyBool(true);
        imageP1Heavy.SetGreyBool(true);
    }

    private Gradient GenerateGradient(GradientColorKey[] colorKeys, Color finalColor)
    {
        Gradient gradient = new Gradient();
        List<GradientColorKey> gradientColorKeys = new List<GradientColorKey>();
        for (int i = 0; i < colorKeys.Length - 1; i++)
        {
            gradientColorKeys.Add(colorKeys[i]);
        }

        gradientColorKeys.Add(new GradientColorKey(finalColor, colorKeys[colorKeys.Length - 1].time));

        List<GradientAlphaKey> gradientAlphaKeys = new List<GradientAlphaKey>();
        foreach (GradientColorKey color in gradient.colorKeys)
        {
            gradientAlphaKeys.Add(new GradientAlphaKey(1, color.time));
        }

        gradient.SetKeys(gradientColorKeys.ToArray(), gradientAlphaKeys.ToArray());
        return gradient;
    }

    private void FirstAnimationP1Bar()
    {
        MMF_Image endLine1 = mediumP1Animation.GetFeedbackOfType<MMF_Image>("EndLine1");
        MMF_Image endLine2 = mediumP1Animation.GetFeedbackOfType<MMF_Image>("EndLine2");
        MMF_Image endLine3 = mediumP1Animation.GetFeedbackOfType<MMF_Image>("EndLine3");
        Color colorFinal12 = Color.green;
        Color colorFinal3 = Color.green;
        switch (DeviceManager.Instance.Player1Controller)
        {
            case Controller.Xbox:
                colorFinal12 = endLine12Xbox;
                colorFinal3 = endLine3Xbox;
                break;
            case Controller.PlayStation:
                colorFinal12 = endLine12Playstation;
                colorFinal3 = endLine3Playstation;
                break;
            case Controller.PC:
                colorFinal12 = endLine12PC;
                colorFinal3 = endLine3Xbox;
                break;
            case Controller.Switch:
                colorFinal12 = endLine12Switch;
                colorFinal3 = endLine3Switch;
                break;
        }

        Gradient gradient1 = GenerateGradient(endLine1.ColorOverTime.colorKeys, colorFinal12);
        Gradient gradient2 = GenerateGradient(endLine2.ColorOverTime.colorKeys, colorFinal12);
        Gradient gradient3 = GenerateGradient(endLine2.ColorOverTime.colorKeys, colorFinal3);

        endLine1.ColorOverTime = gradient1;
        endLine2.ColorOverTime = gradient2;
        endLine3.ColorOverTime = gradient3;

        mediumP1Animation.ComputeCachedTotalDuration();
        mediumP1Animation.Initialization();
        mediumP1Animation.PlayFeedbacks();

        for (int i = 2; i < whiteBarP1.Count; i++)
            whiteBarP1[i].enabled = false;
        //materialP1Heavy.SetInt("_Grey", 0);
        imageP1Heavy.SetGreyBool(false);
        redBarP1[3].DOFillAmount(0, 0.5f).SetUpdate(true).OnComplete(() =>
            redBarP1[2].DOFillAmount(0, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                StartCoroutine(DoAfterDelay(2f, SecondAnimationP1Bar));
            }));
    }

    private void SecondAnimationP1Bar()
    {
        MMF_Image endLine1 = lightP1Animation.GetFeedbackOfType<MMF_Image>("EndLine1");
        MMF_Image endLine2 = lightP1Animation.GetFeedbackOfType<MMF_Image>("EndLine2");
        MMF_Image endLine3 = lightP1Animation.GetFeedbackOfType<MMF_Image>("EndLine3");

        Color colorFinal12 = Color.green;
        Color colorFinal3 = Color.green;
        switch (DeviceManager.Instance.Player1Controller)
        {
            case Controller.Xbox:
                colorFinal12 = endLine12Xbox;
                colorFinal3 = endLine3Xbox;
                break;
            case Controller.PlayStation:
                colorFinal12 = endLine12Playstation;
                colorFinal3 = endLine3Playstation;
                break;
            case Controller.PC:
                colorFinal12 = endLine12PC;
                colorFinal3 = endLine3Xbox;
                break;
            case Controller.Switch:
                colorFinal12 = endLine12Switch;
                colorFinal3 = endLine3Switch;
                break;
        }

        Gradient gradient1 = GenerateGradient(endLine1.ColorOverTime.colorKeys, colorFinal12);
        Gradient gradient2 = GenerateGradient(endLine2.ColorOverTime.colorKeys, colorFinal12);
        Gradient gradient3 = GenerateGradient(endLine2.ColorOverTime.colorKeys, colorFinal3);

        endLine1.ColorOverTime = gradient1;
        endLine2.ColorOverTime = gradient2;
        endLine3.ColorOverTime = gradient3;
        lightP1Animation.ComputeCachedTotalDuration();
        lightP1Animation.Initialization();
        lightP1Animation.PlayFeedbacks();
        for (int i = 1; i < whiteBarP1.Count; i++)
            whiteBarP1[i].enabled = false;
        /*materialP1Medium.SetInt("_Grey", 0);
        materialP1Light.SetInt("_Grey", 1);*/
        imageP1Medium.SetGreyBool(false);
        imageP1Light.SetGreyBool(true);
        redBarP1[1].DOFillAmount(0, 0.5f).SetUpdate(true);
        ResetAnimationP2Bar();
        StartCoroutine(DoAfterDelay(delayChangeBar, ChangeToP2));
    }

    private void ResetAnimationP2Bar()
    {
        for (int i = 0; i < 4; i++)
            whiteBarP2[i].enabled = true;
        foreach (Image image in redBarP2)
            image.fillAmount = 1;
        /*materialP2Light.SetInt("_Grey", 0);
        materialP2Medium.SetInt("_Grey", 1);
        materialP2Heavy.SetInt("_Grey", 1);*/
        imageP2Light.SetGreyBool(false);
        imageP2Medium.SetGreyBool(true);
        imageP2Heavy.SetGreyBool(true);
    }

    private void FirstAnimationP2Bar()
    {
        MMF_Image endLine1 = mediumP2Animation.GetFeedbackOfType<MMF_Image>("EndLine1");
        MMF_Image endLine2 = mediumP2Animation.GetFeedbackOfType<MMF_Image>("EndLine2");
        MMF_Image endLine3 = mediumP2Animation.GetFeedbackOfType<MMF_Image>("EndLine3");

        Color colorFinal12 = Color.green;
        Color colorFinal3 = Color.green;
        switch (DeviceManager.Instance.Player2Controller)
        {
            case Controller.Xbox:
                colorFinal12 = endLine12Xbox;
                colorFinal3 = endLine3Xbox;
                break;
            case Controller.PlayStation:
                colorFinal12 = endLine12Playstation;
                colorFinal3 = endLine3Playstation;
                break;
            case Controller.PC:
                colorFinal12 = endLine12PC;
                colorFinal3 = endLine3Xbox;
                break;
            case Controller.Switch:
                colorFinal12 = endLine12Switch;
                colorFinal3 = endLine3Switch;
                break;
        }

        Gradient gradient1 = GenerateGradient(endLine1.ColorOverTime.colorKeys, colorFinal12);
        Gradient gradient2 = GenerateGradient(endLine2.ColorOverTime.colorKeys, colorFinal12);
        Gradient gradient3 = GenerateGradient(endLine2.ColorOverTime.colorKeys, colorFinal3);
        endLine1.ColorOverTime = gradient1;
        endLine2.ColorOverTime = gradient2;
        endLine3.ColorOverTime = gradient3;
        mediumP2Animation.ComputeCachedTotalDuration();
        mediumP2Animation.Initialization();
        mediumP2Animation.PlayFeedbacks();
        for (int i = 2; i < whiteBarP2.Count; i++)
            whiteBarP2[i].enabled = false;
        //materialP2Heavy.SetInt("_Grey", 0);
        imageP2Heavy.SetGreyBool(false);
        redBarP2[3].DOFillAmount(0, 0.5f).SetUpdate(true).OnComplete(() =>
            redBarP2[2].DOFillAmount(0, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                StartCoroutine(DoAfterDelay(2f, SecondAnimationP2Bar));
            }));
    }

    private void SecondAnimationP2Bar()
    {
        MMF_Image endLine1 = lightP2Animation.GetFeedbackOfType<MMF_Image>("EndLine1");
        MMF_Image endLine2 = lightP2Animation.GetFeedbackOfType<MMF_Image>("EndLine2");
        MMF_Image endLine3 = lightP2Animation.GetFeedbackOfType<MMF_Image>("EndLine3");

        Color colorFinal12 = Color.green;
        Color colorFinal3 = Color.green;
        switch (DeviceManager.Instance.Player2Controller)
        {
            case Controller.Xbox:
                colorFinal12 = endLine12Xbox;
                colorFinal3 = endLine3Xbox;
                break;
            case Controller.PlayStation:
                colorFinal12 = endLine12Playstation;
                colorFinal3 = endLine3Playstation;
                break;
            case Controller.PC:
                colorFinal12 = endLine12PC;
                colorFinal3 = endLine3Xbox;
                break;
            case Controller.Switch:
                colorFinal12 = endLine12Switch;
                colorFinal3 = endLine3Switch;
                break;
        }

        Gradient gradient1 = GenerateGradient(endLine1.ColorOverTime.colorKeys, colorFinal12);
        Gradient gradient2 = GenerateGradient(endLine2.ColorOverTime.colorKeys, colorFinal12);
        Gradient gradient3 = GenerateGradient(endLine2.ColorOverTime.colorKeys, colorFinal3);
        endLine1.ColorOverTime = gradient1;
        endLine2.ColorOverTime = gradient2;
        endLine3.ColorOverTime = gradient3;
        lightP2Animation.ComputeCachedTotalDuration();
        lightP2Animation.Initialization();
        lightP2Animation.PlayFeedbacks();
        for (int i = 1; i < whiteBarP2.Count; i++)
            whiteBarP2[i].enabled = false;
        /*materialP2Medium.SetInt("_Grey", 0);
        materialP2Light.SetInt("_Grey", 1);*/
        imageP2Medium.SetGreyBool(false);
        imageP2Light.SetGreyBool(true);
        redBarP2[1].DOFillAmount(0, 0.5f).SetUpdate(true);
        ResetAnimationP1Bar();
        StartCoroutine(DoAfterDelay(delayChangeBar, ChangeToP1));
    }

    private void ChangePlayerColorHandler()
    {
        foreach (DeviceColorTextHandler deviceColorTextHandler in deviceColorTextHandlers)
        {
            deviceColorTextHandler.P1 = !deviceColorTextHandler.P1;
            if (deviceColorTextHandler.P1)
                deviceColorTextHandler.ChangeColor(DeviceManager.Instance.Player1Controller);
            else
                deviceColorTextHandler.ChangeColor(DeviceManager.Instance.Player2Controller);
        }

        foreach (DeviceColorHandler deviceColorHandler in deviceColorHandlers)
        {
            deviceColorHandler.P1 = !deviceColorHandler.P1;
            if (deviceColorHandler.P1)
                deviceColorHandler.ChangeColor(DeviceManager.Instance.Player1Controller);
            else
                deviceColorHandler.ChangeColor(DeviceManager.Instance.Player2Controller);
        }
    }
}