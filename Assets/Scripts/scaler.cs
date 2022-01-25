using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaler : MonoBehaviour
{
    public float duration;
    public float xScale;
    public float yScale;

    float originalX = -1;
    float originalY = -1;

    float startTime;

    public GameObject here;
    public GameObject me;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(here.GetComponent<SpriteRenderer>() != null || here.GetComponent<SpriteMask>() != null)
        {
            originalX = here.transform.localScale.x;
            originalY = here.transform.localScale.y;
        }

        if(originalX != -1)
        {
            float time = Time.deltaTime;
            float endTime = (Time.time - startTime) / duration;
            if (this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.transform.localScale = new Vector3(originalX + (time / duration * (xScale - originalX)), (originalY + time / duration * (yScale - originalY)), 0);
            }

            if (endTime >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                me.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        
    }

    public void setObject(GameObject o)
    {
        here = o;
    }

    public void setDuration(float d)
    {
        duration = d;
    }

    public void setDisplacement(float x, float y)
    {
        xScale = x;
        yScale = y;
    }
}
