using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fader : MonoBehaviour {
    bool startFade = false;
    int fadeDir = 0;

    void Start()
    {
        if (this.CompareTag("CanvasFaderMainGame"))
        {
            //if (GameObject.FindGameObjectWithTag("player1").GetComponent<playerController>().IsFirstPerson == false)
                GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, -10f, 0);
            startFading(-1);
        }
    }

    void Update()
    {
        if (startFade)
        {
            if (fadeDir > 0)
                GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.black, 4f * Time.deltaTime);
            if (fadeDir < 0)
                GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.clear, 4f * Time.deltaTime);
        }
        if (GameObject.FindGameObjectWithTag("MainCamera").transform.position.y < 22.41f)// && GameObject.FindGameObjectWithTag("player1").GetComponent<playerController>().IsFirstPerson == false)
            GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(new Vector3(0, 0, -25f * Time.deltaTime));
        else this.enabled = false;
    }

    public void startFading(int fadeDirection)
    {
        fadeDir = fadeDirection;
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        startFade = true;
        StopCoroutine(delay());
    }
}
