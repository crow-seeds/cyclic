using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tele : MonoBehaviour {

    public GameObject bullets; //me
    public GameObject other; //copy off it
    public GameObject output; //output, where it gets teleported
    public movingRing ring;
    public GameObject telesprite;
    public float speed = 1;
    public bool collision = false;
    public float outputAngle;

    float speedModifier = 1;

    bool original = false;

    // Use this for initialization
    void Start () {
		if((this.GetComponent<Transform>().localPosition == new Vector3(-302, 0, 0))){
            original = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * speed * speedModifier);
        telesprite.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);

        if (PlayerPrefs.GetInt("lowdetail", 0) == 0)
        {
            telesprite.transform.Rotate(Vector3.forward, Time.deltaTime * 300);
        }

        if (!original)
        {
            telesprite.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
        }
        if(original){
            telesprite.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;
        }

        if (!original && Vector2.Distance(this.transform.localPosition, Vector2.zero) > 4.6)
        {
            bullets.SetActive(false);
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
       if (col.gameObject.name == "curve")
        {
            bullets.SetActive(false);
            Destroy(output);
            Destroy(this.gameObject);
            col.gameObject.transform.RotateAround(Vector3.zero, Vector3.forward, outputAngle - col.gameObject.GetComponent<Transform>().localRotation.eulerAngles.z);
        }

        if (col.gameObject.tag == "bullet" && collision == false)
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }

        if (col.gameObject.tag == "spike" && collision == false)
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }

        if (col.gameObject.tag == "telemain")
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

    public void setOutput(GameObject o)
    {
        output = o;
        outputAngle = output.GetComponent<Transform>().localRotation.eulerAngles.z;
    }

    public void timewarp(float s)
    {
        speedModifier = s;
    }

}
