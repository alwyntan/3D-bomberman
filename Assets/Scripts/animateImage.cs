using UnityEngine;
using System.Collections;

public class animateImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(picColorChg());
	}

    int fadeDir = 1;
	// Update is called once per frame
	void Update () {
         if (fadeDir == 1) GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.black, 0.3f * Time.deltaTime);
         else GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(GetComponent<UnityEngine.UI.Image>().color, Color.white, 0.3f  * Time.deltaTime);

        GetComponent<RectTransform>().Rotate(new Vector3(0,0,1));
	}

    IEnumerator picColorChg()
    {
        yield return new WaitForSeconds(5);
        if (fadeDir == 1) fadeDir = -1;
        else fadeDir = 1;
        StartCoroutine(picColorChg());
    }
}
