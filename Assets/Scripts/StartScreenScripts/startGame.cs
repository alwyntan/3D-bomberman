using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startGame : MonoBehaviour {

    public GameObject LoadingScreen;
    public Scrollbar LoadingBar;
    Rigidbody rb;
    AsyncOperation async;

    public void LoadLevel()
    {
        rb = GameObject.FindGameObjectWithTag("player1").GetComponent<Rigidbody>();
        GameObject.FindGameObjectWithTag("player1").SetActive(false);
        StartCoroutine(LevelCoroutine());
    }

    bool notStarted = true;
    IEnumerator LevelCoroutine()
    {
        LoadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(1);

        while (!async.isDone)
        {
            LoadingBar.size = async.progress + 0.1f;
            async.allowSceneActivation = false;

            if (async.progress > 0.89 && notStarted)
            {
                notStarted = false;
                GameObject.FindGameObjectWithTag("CanvasFader").SetActive(true);
                GameObject.FindGameObjectWithTag("CanvasFader").GetComponent<Fader>().startFading(1);
                StartCoroutine(activateScene());
            }

            yield return null;
        }
    }

    IEnumerator activateScene()
    {
        yield return new WaitForSeconds(2f);
        async.allowSceneActivation = true;
    }
}
