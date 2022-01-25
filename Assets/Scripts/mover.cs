using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public float duration;
    public float xDisp;
    public float yDisp;

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
        float time = Time.deltaTime;
        float endTime = (Time.time - startTime) / duration;
        if (this.GetComponent<Transform>().position.Equals(Vector3.zero)){
            here.transform.Translate(time / duration * xDisp, time / duration * yDisp, 0, Space.World);
        }
        
        if (endTime >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
        {
            me.SetActive(false);
            Destroy(this.gameObject);
        }

        if(here == null)
        {
            me.SetActive(false);
            Destroy(this.gameObject);
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
        xDisp = x;
        yDisp = y;
    }
}
