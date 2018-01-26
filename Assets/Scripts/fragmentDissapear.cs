using UnityEngine;
using System.Collections;

public class fragmentDissapear : MonoBehaviour {

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(destroy());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator destroy()
    {
        rb.AddForce(Vector3.up * Random.Range(2, 5), ForceMode.Impulse);
        rb.AddForce(Vector3.left * Random.Range(2, 4), ForceMode.Impulse);
        rb.AddForce(Vector3.right * Random.Range(2, 4), ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
