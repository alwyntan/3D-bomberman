using UnityEngine;
using System.Collections;

public class explosionBehavior : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        var explode = GetComponent<ParticleSystem>();
        explode.Play();
        Destroy(this.gameObject, explode.duration);
    }	

	// Update is called once per frame
	void Update ()
    {

	}
}
