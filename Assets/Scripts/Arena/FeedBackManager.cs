using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    public static List<MMFeedbacks> feedbacks = new List<MMFeedbacks>();

    public static bool isPlayingFeedback = false;

    public IEnumerator PlayFeedBackInQueue() {
        if (!isPlayingFeedback) {
            isPlayingFeedback = true;
            Debug.Log("size " + feedbacks.Count);
            for (int i = 0; i < feedbacks.Count; i += 1) {
                feedbacks[i].PlayFeedbacks();
                yield return null;
            }
            feedbacks = new List<MMFeedbacks>();
            isPlayingFeedback = false;
        }
    }
}
