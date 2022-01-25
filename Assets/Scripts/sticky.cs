using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sticky : MonoBehaviour {

    public GameObject bullets;
    public GameObject other;
    public GameObject stickysprite;
    public float speed = 0;
    public float startTime = 0;
    public float expirationTime = 1;

    float speedModifier = 1;
    bool lowDetailMode = false;
    bool original = false;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        if (PlayerPrefs.GetInt("lowdetail", 0) == 1)
        {
            lowDetailMode = true;
        }

        if (this.transform.localPosition.x < -200)
        {
            original = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * speed * speedModifier);

        startTime += Time.deltaTime;
        if (lowDetailMode == false)
        {
            stickysprite.transform.Rotate(Vector3.forward, Time.deltaTime * 300);
        }

        if (!original)
        {
            stickysprite.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
        }
        if (original)
        {
            stickysprite.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
        }

        if (startTime >= expirationTime && this.GetComponent<Transform>().localPosition != new Vector3(-301, 0, 0))
        {
            bullets.SetActive(false);
            Destroy(this.gameObject);
        }

        if(!original && Vector2.Distance(this.transform.localPosition, Vector2.zero) > 4.7)
        {
            speed = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "spike")
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }

        if (col.gameObject.tag == "bullet")
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }
    }

    public void setSpeed(float s)
    {
        speed = s;
    }

    public void setExpiration(float s)
    {
        expirationTime = startTime+s;
    }

    public void timewarp(float s)
    {
        speedModifier = s;
    }
}
