using UnityEngine;
using System.Collections;

public class breakableBehavior : MonoBehaviour {

    public GameObject bombPowUp = new GameObject();
    public GameObject rangePowUp = new GameObject();
    public GameObject speedPowUp = new GameObject();
    public GameObject spikeBombPowUp = new GameObject();
    public GameObject kickBombPowUp = new GameObject();
    public GameObject explodingFragment = new GameObject();
    Quaternion rotationForPowUp;

    // Use this for initialization
    void Start () {
        rotationForPowUp.eulerAngles = new Vector3(45,0,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Destroy()
    {
        StartCoroutine(randomCreate());
    }

    Vector3 randomVector()
    {
        return new Vector3(transform.position.x + Random.Range(-0.3f, 0.3f), transform.position.y + Random.Range(-0.3f, 0.3f), transform.position.z + Random.Range(-0.3f, 0.3f));
    }

    public IEnumerator randomCreate()
    {
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<BoxCollider>().isTrigger = true;

        for (int i = 0; i < 15; i++)
        {
            Instantiate(explodingFragment, randomVector(), rotationForPowUp);
        }

        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
        int num = Random.Range(0, 20);        

        if (num < 3)
        {
            Instantiate(bombPowUp, transform.position, rotationForPowUp);
        }

        if (num >= 3 && num < 6)
        {
            Instantiate(rangePowUp, transform.position, rotationForPowUp);
        }

        if (num >= 6 && num < 9)
        {
            Instantiate(speedPowUp, transform.position, rotationForPowUp);
        }

        if (num == 9)
        {
            Instantiate(spikeBombPowUp, transform.position, rotationForPowUp);
        }

        if (num == 10)
        {
            Instantiate(kickBombPowUp, transform.position, rotationForPowUp);
        }
    }
}
