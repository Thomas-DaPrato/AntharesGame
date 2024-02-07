using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Freezer : MonoBehaviour
{


    public IEnumerator Freeze(float duration) {
        var original = Time.timeScale;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
    }
}
