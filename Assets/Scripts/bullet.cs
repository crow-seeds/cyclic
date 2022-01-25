using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public GameObject bullets;
    public GameObject other;
    public movingRing ring;
    public float speed = 1;
    public bool collision = false;
    public ring circle;

    float speedModifier = 1;

    bool original = false;
    bool original2 = false;

    // Use this for initialization
    void Start () {
        if (this.transform.localPosition.x == -300)
        {
            original = true;
        }

        if (this.transform.localPosition.x == -301)
        {
            original2 = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * speed * speedModifier);
        if(!original)
        {
            this.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
        }
        if(original2){
            this.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
        }

        if (!original && !original2 && Vector2.Distance(this.transform.localPosition, Vector2.zero) > 4.6)
        {
            circle.ball();
            bullets.SetActive(false);
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
       if (col.gameObject.name == "curve")
        {
            bullets.SetActive(false);
            if(PlayerPrefs.GetInt("lowdetail", 0) == 0)
            {
                movingRing r = Instantiate(ring, Vector3.zero, Quaternion.Euler(0, 0, 0));
                r.setVelocity(speed / 64);
            }
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "bullet" && collision == false)
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }

        if (col.gameObject.tag == "spike" && collision == false)
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }

        if (col.gameObject.tag == "telemain" && collision == false)
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }

        if (col.gameObject.name == "movingCircle")
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
