using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingRing : MonoBehaviour
{
    private float something = 0;
    public GameObject ring;
    float pl = 1;
    // Use this for initialization
    void Start()
    {
        something = 0;
    }

    // Update is called once per frame
    void Update()
    {
        something += Time.deltaTime;
        if (this.GetComponent<Transform>().position.Equals(Vector3.zero))
        {
            transform.localScale += new Vector3(something*pl, something*pl, 0);
            if(transform.localScale.x >= 2)
            {
                ring.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        
        this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, 1f-something*3f);
    }

    public void setVelocity(float p)
    {
        pl = p;
    }
}
