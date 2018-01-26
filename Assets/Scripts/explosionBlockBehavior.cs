using UnityEngine;
using System.Collections;

public class explosionBlockBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(explode());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator explode()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        //game tag on bombs per player
        if (other.gameObject.CompareTag("player1bomb")) other.gameObject.GetComponent<bombBehavior>().instantExplode();
        if (other.gameObject.CompareTag("player2bomb")) other.gameObject.GetComponent<bombBehavior>().instantExplode();

        if (other.gameObject.CompareTag("breakable")) other.gameObject.GetComponent<breakableBehavior>().Destroy();
        if (other.gameObject.CompareTag("player1")) Destroy(other.gameObject);
        if (other.gameObject.CompareTag("player2")) Destroy(other.gameObject);
        //if (other.transform.parent.transform.gameObject.tag.Contains("powUp")) Destroy(other.transform.parent.gameObject);
    }
}
