using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Freezer : MonoBehaviour
{
    private static bool canFreeze = true;
    public IEnumerator Freeze(float duration) {
        if(canFreeze){
            canFreeze = false;
            var original = Time.timeScale;
            Time.timeScale = 0;

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = original;
            canFreeze = true;
        }
    }
}
