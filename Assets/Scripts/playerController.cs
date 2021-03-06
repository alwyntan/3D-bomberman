﻿using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    Animator anim;
    public GameObject normalBomb;
    public GameObject spikeBomb;
    //public bool IsFirstPerson = false;
 
    public int bombCount = 1;
    public int bombPlanted = 0;
    public int bombRange = 1;
    public string bombType = "normal";
    public bool kickBomb = false;
    public float moveSpeed = 0.01f;
    float offsetForOverlapSphere = 0.3f;

    public string currentFacingDirection = "up";

    private string player;

    public string test = "";

    bool moveup = true;
    bool movedown = true;
    bool moveleft = true;
    bool moveright = true;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        player = this.tag;
    }
	
	// Update is called once per frame
	void Update ()
    {
            rotatin();
            movin();
            plantbom();

        if (transform.rotation.eulerAngles.y == 0) currentFacingDirection = "up";
        else if (transform.rotation.eulerAngles.y > 179 && transform.rotation.eulerAngles.y < 181) currentFacingDirection = "down";
        else if (transform.rotation.eulerAngles.y > 89 && transform.rotation.eulerAngles.y < 91) currentFacingDirection = "right";
        else if (transform.rotation.eulerAngles.y > 269 && transform.rotation.eulerAngles.y < 271) currentFacingDirection = "left";
    }

    void plantbom()
    {
        if (player == "player1")
        {
            if (bombPlanted < bombCount)
                if (Input.GetKeyDown(KeyCode.Q))
                    plant();
        }
        else if (player == "player2")
        {
            if (bombPlanted < bombCount)
                if (Input.GetKeyDown(KeyCode.Space))
                    plant();
        }
    }

    void plant()
    {
        var roundedPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

        if (noBomb(roundedPosition))
        {

            bombPlanted += 1;
            if (bombType == "normal")
            {
                ((GameObject)Instantiate(normalBomb, roundedPosition, transform.rotation)).tag = player + "bomb";
            }
            else if (bombType == "spike")
            {
                ((GameObject)Instantiate(spikeBomb, roundedPosition, transform.rotation)).tag = player + "bomb";
            }
        }
    }

    private bool noBomb(Vector3 location)
    {
        Collider[] objects = Physics.OverlapSphere(location, 0.1f);
        foreach (Collider coll in objects)
            if (coll.gameObject.CompareTag("player1bomb") || coll.gameObject.CompareTag("player2bomb"))
                return false;
        return true;
    }

    void rotatin()
    {
        var rotationVector = transform.rotation.eulerAngles;

        //if (IsFirstPerson == false)
        //{
            if (player == "player1")
            {
                if (Input.GetAxis("Vert_P1") < 0) { rotationVector.y = 180; }
                if (Input.GetAxis("Vert_P1") > 0) { rotationVector.y = 0; }
                if (Input.GetAxis("Horiz_P1") < 0) { rotationVector.y = 270; }
                if (Input.GetAxis("Horiz_P1") > 0) { rotationVector.y = 90; }
            }
            else if (player == "player2")
            {
                if (Input.GetAxis("Vert_P2") < 0) { rotationVector.y = 180; }
                if (Input.GetAxis("Vert_P2") > 0) { rotationVector.y = 0; }
                if (Input.GetAxis("Horiz_P2") < 0) { rotationVector.y = 270; }
                if (Input.GetAxis("Horiz_P2") > 0) { rotationVector.y = 90; }
            }
            transform.rotation = Quaternion.Euler(rotationVector);
    }

    private bool checkIfExistOn(Vector3 location, string data)
    {
        Collider[] objects = Physics.OverlapSphere(location, 0.2f);

        if (data == "all")
        {
            foreach (Collider coll in objects)
            {
                if (coll.gameObject.CompareTag("wall") || coll.gameObject.CompareTag("breakable"))
                {
                    return true;
                }
            }
        }
        else
        {
            foreach (Collider coll in objects)
            {
                if (coll.gameObject.CompareTag(data))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool idleCoroutineStarted = false;

    /// <summary>
    /// Automatically moves the player sideways to provide smooth transition on blocks
    /// </summary>
    /// <param name="type"></param>
    void autoMoveSideways(string type, float inputMoveSpeed)
    {
        if (type == "moveVertical")
        {
            bool up = false;
            bool down = false;

            if (checkIfExistOn(new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetForOverlapSphere), "all") == false)
                up = true;
            if (checkIfExistOn(new Vector3(transform.position.x, transform.position.y, transform.position.z - offsetForOverlapSphere), "all") == false)
                down = true;

            if (up == true)
            {
                transform.Translate(inputMoveSpeed * -moveSpeed, 0, 0);
            }
        }

    }
    
    void movin()
    {
        float moveVert = 0;
        float moveHoriz = 0;

        if (player == "player1")
        {
            moveVert = Input.GetAxis("Vert_P1");
            moveHoriz = Input.GetAxis("Horiz_P1");
        }
        else if (player == "player2")
        {
            moveVert = Input.GetAxis("Vert_P2");
            moveHoriz = Input.GetAxis("Horiz_P2");
        }

        var roundedPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

        bool tempskip = false;

        if (transform.rotation.eulerAngles.y < 181 && transform.rotation.eulerAngles.y > 179 || transform.rotation.eulerAngles.y == 0)
        {
            if (Mathf.Abs(moveVert) > 0)
            {
                anim.SetBool("allowIdle", false);
                StopCoroutine(allowIdleState());
                idleCoroutineStarted = false;
            }

            if (moveVert > 0 && moveup) // up
            {
                if (checkIfExistOn(new Vector3(transform.position.x, transform.position.y, transform.position.z + offsetForOverlapSphere), "all") == false)
                {
                    transform.Translate(0, 0, moveVert * moveSpeed);
                }
                else
                {
                    anim.SetFloat("speed", 0);
                    tempskip = true;
                }
                movedown = true; moveleft = true; moveright = true;
            }
            if (moveVert < 0 && movedown) // down
            {
                if (checkIfExistOn(new Vector3(transform.position.x, transform.position.y, transform.position.z - offsetForOverlapSphere), "all") == false)
                {
                    transform.Translate(0, 0, -moveVert * moveSpeed);
                }
                else
                {
                    anim.SetFloat("speed", 0);
                    tempskip = true;
                }
                moveup = true; moveleft = true; moveright = true;
            }

            if (tempskip == false)
                anim.SetFloat("speed", Mathf.Abs(moveVert));
        }

        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == 270)
        {
            if (Mathf.Abs(moveHoriz) > 0)
            {
                anim.SetBool("allowIdle", false);
                StopCoroutine(allowIdleState());
                idleCoroutineStarted = false;
            }

            if (moveHoriz > 0 && moveright)
            {
                //autoMoveSideways("moveVertical", moveHoriz);///////////////////////////////working on this

                if (checkIfExistOn(new Vector3(transform.position.x + offsetForOverlapSphere, transform.position.y, transform.position.z), "all") == false)
                {
                    transform.Translate(0, 0, moveHoriz * moveSpeed);
                }
                else
                {
                    anim.SetFloat("speed", 0);
                    tempskip = true;
                }
                movedown = true; moveleft = true; moveup = true;
            }
            if (moveHoriz < 0 && moveleft)
            {
                if (checkIfExistOn(new Vector3(transform.position.x - offsetForOverlapSphere, transform.position.y, transform.position.z), "all") == false)
                {
                    transform.Translate(0, 0, -moveHoriz * moveSpeed);
                }
                else
                {
                    anim.SetFloat("speed", 0);
                    tempskip = true;
                }
                movedown = true; moveup = true; moveright = true;
            }
            if (tempskip == false)
                anim.SetFloat("speed", Mathf.Abs(moveHoriz));
        }

        if (moveVert == 0 && moveHoriz == 0)
        {
            if (idleCoroutineStarted == false)
            {
                StartCoroutine(allowIdleState());
                idleCoroutineStarted = true;
            }

        }
    }

    IEnumerator allowIdleState()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("allowIdle", true);
        idleCoroutineStarted = false;
        StopCoroutine(allowIdleState());
    }

    void OnCollisionEnter(Collision other)
    {
        if (kickBomb == true)
        {
            if (other.gameObject.CompareTag("player1bomb") || other.gameObject.CompareTag("player2bomb"))
            {
                anim.SetBool("kick", true);

                switch (currentFacingDirection)
                {
                    case "up":
                        moveup = false;
                        break;

                    case "down":
                        movedown = false;
                        break;

                    case "left":
                        moveleft = false;
                        break;

                    case "right":
                        moveright = false;
                        break;
                }

                StartCoroutine(stopKick(other));                                        
            }
        }
         
        if (other.gameObject.CompareTag("player1") || other.gameObject.CompareTag("player2"))
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), transform.GetComponent<Collider>());
        }

        if (other.gameObject.CompareTag("fragment"))
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), transform.GetComponent<Collider>());
        }
    }

    IEnumerator stopKick(Collision other)
    {
        string dir = currentFacingDirection;
        yield return new WaitForSeconds(0.1f);
        other.gameObject.GetComponent<bombBehavior>().StopMoving();
        other.gameObject.GetComponent<bombBehavior>().Move(dir);
        anim.SetBool("kick", false);
        moveup = true;
        movedown = true;
        moveleft = true;
        moveright = true;
        StopCoroutine(stopKick(other));
    }
}
