using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Freezer : MonoBehaviour
{

    public MMFeedbacks LightFeedbacks;

    public IEnumerator Freeze(float duration) {
        var original = Time.timeScale;
        Time.timeScale = 0;
        LightFeedbacks.PlayFeedbacks();

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
    }
}
