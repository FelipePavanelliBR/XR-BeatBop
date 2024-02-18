using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update

    public DissolveTransition dissolveScreen;

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        dissolveScreen.DissolveIn();
        yield return new WaitForSeconds(dissolveScreen.duration);

        //launching scene
        SceneManager.LoadScene(sceneIndex);
    }

}
