using UnityEngine;
using System.Collections;

public class powUpTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("bombPowUp"))
        {
            if (other.CompareTag("player1") || other.CompareTag("player2"))
            {
                other.gameObject.GetComponent<playerController>().bombCount++;
                Destroy(this.transform.parent.gameObject);
            }
        }

        if (this.CompareTag("rangePowUp"))
        {
            if (other.CompareTag("player1") || other.CompareTag("player2"))
            {
                other.gameObject.GetComponent<playerController>().bombRange++;
                Destroy(this.transform.parent.gameObject);
            }
        }

        if (this.CompareTag("speedPowUp"))
        {
            if (other.CompareTag("player1") || other.CompareTag("player2"))
            {
                other.gameObject.GetComponent<playerController>().moveSpeed = other.gameObject.GetComponent<playerController>().moveSpeed + 0.01f;
                Destroy(this.transform.parent.gameObject);
            }
        }

        if (this.CompareTag("spikeBombPowUp"))
        {
            if (other.CompareTag("player1") || other.CompareTag("player2"))
            {
                other.gameObject.GetComponent<playerController>().bombType = "spike"; 
                Destroy(this.transform.parent.gameObject);
            }
        }

        if (this.CompareTag("kickBombPowUp"))
        {
            if (other.CompareTag("player1") || other.CompareTag("player2"))
            {
                other.gameObject.GetComponent<playerController>().kickBomb = true;
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
