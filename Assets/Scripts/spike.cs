using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour {

    public GameObject bullets;
    public GameObject other;
    public GameObject spikesprite;
    public float speed = 1;
    public ring circle;

    float speedModifier = 1;
    bool lowDetailMode = false;
    bool original = false;

    // Use this for initialization
    void Start()
    {
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
        if(!lowDetailMode)
        {
            spikesprite.transform.Rotate(Vector3.forward, Time.deltaTime * 300);
        }
        if(!original)
        {
            spikesprite.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
        }
        if(original){
            spikesprite.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
        }

        if(!original && Vector2.Distance(this.transform.localPosition, Vector2.zero) > 4.6)
        {
            circle.spike();
            bullets.SetActive(false);
            Destroy(this.gameObject);
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "curve")
        {
            bullets.SetActive(false);
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "spike")
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }

        if (col.gameObject.tag == "bullet")
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }

        if (col.gameObject.tag == "telemain")
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }
    }

    public void setSpeed(float s)
    {
        speed = s;
    }

    public void timewarp(float s)
    {
        speedModifier = s;
    }
}
