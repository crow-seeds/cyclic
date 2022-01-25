using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorFader : MonoBehaviour {

    public float fadeTime;
    public Color c = new Color(0, 0, 0);
    public Text fere;
    public Camera cere;
    public RawImage mobileSwap;
    public GameObject m;
    public GameObject r;
    public GameObject b;
    public GameObject s;
    public GameObject o;
    public GameObject i; //inner circle
    public GameObject here;
    public Canvas were;
    string stasis = "none";

    public Text pauseButton;
    public Text practiceButton;

    bool water = false;

    public Color r1;
    public Color m1;
    public Color c1;
    public Color t1;
    public Color b1;
    public Color s1;
    public Color w1;
    public Color o1;
    public Color i1; //inner circle

    float startTime;
    float time;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        time = (Time.time - startTime) / fadeTime;
        if (fadeTime == 0)
        {
            time = 1f;
        }
        if (water == false)
        {
            r1 = r.GetComponent<SpriteRenderer>().color;
            m1 = m.GetComponent<SpriteRenderer>().color;
            c1 = cere.GetComponent<Camera>().backgroundColor;
            t1 = fere.GetComponent<Text>().color;
            b1 = b.GetComponent<SpriteRenderer>().color;
            if ((s.GetComponent("Text") as Text) != null)
            {
                s1 = s.GetComponent<Text>().color;
            }
            else
            {
                s1 = s.GetComponent<SpriteRenderer>().color;
            }
            o1 = o.GetComponent<SpriteRenderer>().color;
            i1 = i.GetComponent<SpriteRenderer>().color;
            w1 = were.GetComponent<CanvasRenderer>().GetColor();
            water = true;
        }
        else if (stasis.Equals("circle"))
        {
            r.GetComponent<SpriteRenderer>().color = Color.Lerp(r1, c, time);
            m.GetComponent<SpriteRenderer>().color = Color.Lerp(m1, c, time);
            if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }
        } else if (stasis.Equals("background"))
        {
            Color n = new Color(1 - c.r, 1 - c.g, 1 - c.b);
            if(c.r+c.g+c.b > 1.5)
            {
                n = new Color(0, 0, 0);
            }
            else
            {
                n = new Color(1, 1, 1);
            }
            cere.GetComponent<Camera>().backgroundColor = Color.Lerp(c1, c, time);
            fere.GetComponent<Text>().color = Color.Lerp(t1, n, time);
            pauseButton.GetComponent<Text>().color = Color.Lerp(t1, n, time);
            practiceButton.GetComponent<Text>().color = Color.Lerp(t1, n, time);
            Color pn = new Color(n.r, n.g, n.b, .24f);
            mobileSwap.color = Color.Lerp(t1, pn, time);
            if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else if(stasis.Equals("bullets"))
        {
            b.GetComponent<SpriteRenderer>().color = Color.Lerp(b1, c, time);
            if (time >= 1  && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else if(stasis.Equals("shoot"))
        {
            s.GetComponent<SpriteRenderer>().color = Color.Lerp(s1, c, time);
            if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else if (stasis.Equals("overlay"))
        {
            o.GetComponent<SpriteRenderer>().color = Color.Lerp(o1, c, time);
            if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else if (stasis.Equals("inner"))
        {
            i.GetComponent<SpriteRenderer>().color = Color.Lerp(i1, c, time);
            if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else if (stasis.Equals("win"))
        {
            were.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, time); 
            if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }

        }
        else if (stasis.Equals("other"))
        {
            if(s == null)
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }
            if ((s.GetComponent("Text") as Text) != null)
            {
                s.GetComponent<Text>().color = Color.Lerp(s1, c, time);
            }
            else
            {
                s.GetComponent<SpriteRenderer>().color = Color.Lerp(s1, c, time);
            }
            if (time >= 1 && this.GetComponent<Transform>().position.Equals(Vector3.zero))
            {
                here.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        
        
    }

    public void setColor(Color ca)
    {
        c = ca;
    }

    public void setFade(float f)
    {
        fadeTime = f;
    }

    public void setMode(string s)
    {
        stasis = s;
    }

    public void setOther(GameObject o)
    {
        s = o;
    }

    public Color getColor(string mode)
    {
        switch (mode)
        {
            case "background":
                return c1;
            case "shoot":
                return s1;
            case "circle":
                return r1;
            case "bullets":
                return b1;
        }
        return new Color(0, 0, 0);  
    }

}
