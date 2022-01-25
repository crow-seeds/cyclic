using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour {

    public float duration;
    public float degrees;

    public GameObject here;
    public GameObject me;
    public float originalAngle;
    float startTime;
    bool mode = false; //false is rotate self, true is rotate around

    Vector3 meme = new Vector3();

    // Use this for initialization
    void Start () {
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if(duration == 0)
        {
            if(degrees != 0)
            {
                if (!mode)
                {
                    here.transform.rotation = Quaternion.Euler(0, 0, degrees);
                    me.SetActive(false);
                    Destroy(this.gameObject);
                }
                else
                {
                    here.transform.RotateAround(meme, Vector3.forward, degrees);
                    me.SetActive(false);
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            float time = (Time.time - startTime) / duration;

            if(here == null)
            {
                me.SetActive(false);
                Destroy(this.gameObject);
            }
            else
            {
                if (!mode)
                {
                    here.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(originalAngle, originalAngle + degrees, time));
                    if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
                    {
                        me.SetActive(false);
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    here.transform.RotateAround(meme, Vector3.forward, Time.deltaTime * degrees / duration);
                    if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
                    {
                        me.SetActive(false);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }



    public void setObject(GameObject o)
    {
        here = o;
        originalAngle = here.transform.GetComponent<Transform>().eulerAngles.z;
    }

    public void setDegrees(float d)
    {
        degrees = d;
    }

    public void setDuration(float d)
    {
        duration = d;
    }

    public void rotateAround(float x, float y)
    {
        mode = true;
        meme = new Vector3(x, y, here.transform.GetComponent<Transform>().eulerAngles.z);
    }
}
