using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArenaASync : MonoBehaviour
{
    private bool canChangeScene;

    private void Start() {
        StartCoroutine(Timer());
        StartCoroutine(LoadASync());
    }

    public IEnumerator LoadASync() {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
        operation.allowSceneActivation = false;
        while (!operation.isDone) {
            //Debug.Log(operation.progress);
            if (canChangeScene && operation.progress == 0.9f) {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
        
    }

    public IEnumerator Timer() {
        canChangeScene = false;
        yield return new WaitForSeconds(3);
        canChangeScene = true;
    }
}
