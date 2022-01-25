using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexagon : MonoBehaviour
{
    public GameObject myself;
    public GameObject other;
    public movingRing ring;
    public float speed = 1;
    public bool collision = false;

    public GameObject hexagonsprite;

    float speedModifier = 1;
    bool lowdetailmode = false;
    bool original = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("lowdetail", 0) == 1)
        {
            lowdetailmode = true;
        }

        if (this.transform.localPosition.x < -200)
        {
            original = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed * speedModifier);
        if (!lowdetailmode)
        {
            hexagonsprite.transform.Rotate(Vector3.forward, Time.deltaTime * 300);
        }
        hexagonsprite.GetComponent<SpriteRenderer>().color = other.GetComponent<SpriteRenderer>().color;

        if (!original && Vector2.Distance(this.transform.localPosition, Vector2.zero) > 4.6)
        {
            myself.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "curve")
        {
            myself.SetActive(false);
            Destroy(this.gameObject);
        }

        if (col.gameObject.name == "circle")
        {
            myself.SetActive(false);
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
