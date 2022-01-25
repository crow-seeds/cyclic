using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooter : MonoBehaviour {

    public Text percentage;
    public GameObject shootthing;
    public bullet bullets;
    public colorFader fader;
    public GameObject winscreen;
    public ring outer;
    public NewBehaviourScript player;
    public float timer = 0;
    int order = 1;
    int[] colorPriority = new int[7];
    bool hasWon = false;

    public Text percent;
    public Text missed;
    public Text hit;
    public Text timething;

    string world = "0";
    string level = "0";

    public List<GameObject> defaults = new List<GameObject>();
    public List<GameObject> objects = new List<GameObject>();

    public void shoot(float angle, float speed, float time, int priority)
    {
        if(timer >= time && priority == order)
        {
            bullet b = Instantiate(bullets, Vector3.zero, Quaternion.Euler(0, 0, angle));
            b.setSpeed(speed);
            order++;
            shootthing.transform.rotation = Quaternion.Euler(0, 0, angle+180);
        }
    }

    public void multishot(float angle, float speed, float time, int priority, int amount, float delay)
    {
        for(int i = 0; i < amount; i++)
        {
            shoot(angle, speed, time + delay * i, priority+i);
        }
    }

    public void spread(float angle, float speed, float time, int priority, int amount, float delay, string direction, float spacing)
    {
        for(int i = 0; i < amount; i++)
        {
            if(direction == "Clockwise")
            {
                shoot(angle - i * spacing, speed, time + delay*i, priority + i);
            }
            else
            {
                shoot(angle + i * spacing, speed, time + delay * i, priority + i);
            }
            
        }
    }

    public void zigshot(float angle, float speed, float time, int priority, int amount, float delay, float spacing)
    {
        for(int i = 0; i < amount; i++)
        {
            if(i%2 == 0)
            {
                shoot(angle, speed, time + delay * i, priority + i);
            }
            else
            {
                shoot(angle+spacing, speed, time + delay * i, priority + i);
            }
        }
    }

    public void colorChangeCircle(float red, float green, float blue, float fadetime, float time, int priority)
    {
        if(timer >= time && priority == colorPriority[0])
        {
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setColor(new Color(red / 255, green / 255, blue / 255));
            f.setMode("circle");
            f.setFade(fadetime);
            colorPriority[0]++;
        }
    }

    public void colorChangeBullets(float red, float green, float blue, float fadetime, float time, int priority)
    {
        if (timer >= time && priority == colorPriority[1])
        {
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setColor(new Color(red / 255, green / 255, blue / 255));
            f.setMode("bullets");
            f.setFade(fadetime);
            colorPriority[1]++;
        }
    }

    public void colorChangeBackground(float red, float green, float blue, float fadetime, float time, int priority)
    {
        if (timer >= time && priority == colorPriority[2])
        {
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setColor(new Color(red / 255, green / 255, blue / 255));
            f.setMode("background");
            f.setFade(fadetime);
            colorPriority[2]++;
        }
    }

    public void colorChangeShooter(float red, float green, float blue, float fadetime, float time, int priority)
    {
        if (timer >= time && priority == colorPriority[3])
        {
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setColor(new Color(red / 255, green / 255, blue / 255));
            f.setMode("shoot");
            f.setFade(fadetime);
            colorPriority[3]++;
        }
    }

    public void colorChangeOverlay(float red, float green, float blue, float fadetime, float time, int priority, float alpha)
    {
        if (timer >= time && priority == colorPriority[4])
        {
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setColor(new Color(red / 255, green / 255, blue / 255, alpha / 255));
            f.setMode("overlay");
            f.setFade(fadetime);
            colorPriority[4]++;
        }
    }

    public void colorChangeInner(float red, float green, float blue, float fadetime, float time, int priority, float alpha)
    {
        if (timer >= time && priority == colorPriority[5])
        {
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setColor(new Color(red / 255, green / 255, blue / 255, alpha / 255));
            f.setMode("inner");
            f.setFade(fadetime);
            colorPriority[5]++;
        }
    }

    public void createObject(float xpos, float ypos, float zpos, float length, float height, float rotation, float red, float green, float blue, int alpha, int objectid)
    {
        GameObject s = Instantiate(defaults[objectid], new Vector3(xpos, ypos, zpos), Quaternion.Euler(0, 0, rotation));
        s.GetComponent<SpriteRenderer>().color = new Color(red / 255, green / 255, blue / 255, alpha / 255);
        s.transform.localScale = new Vector3(length, height);
        objects.Add(s);
    }

    public void colorChangeObject(float red, float green, float blue, float alpha, float fadetime, float time, int priority, int id)
    {
        if (timer >= time && priority == colorPriority[6])
        {
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setMode("other");
            f.setOther(objects[id]);
            f.setColor(new Color(red / 255, green / 255, blue / 255, alpha / 255));
            f.setFade(fadetime);
            colorPriority[6]++;
        }
    }

    public void gonnaWinLevel(float time)
    {
        if (timer >= time && hasWon == false)
        {
            if(PlayerPrefs.GetInt(world.ToString() + "," + level.ToString()) < (player.getCollect() / (player.getCollect() + outer.getCollect())) * 100){
                PlayerPrefs.SetInt(world.ToString() + "," + level.ToString(), (int)((player.getCollect() / (player.getCollect() + outer.getCollect())) * 100));
            }
            
            hasWon = true;
            winscreen.SetActive(true);
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setMode("win");
            f.setFade(3);
            missed.text = outer.getCollect().ToString();
            hit.text = player.getCollect().ToString();
            percent.text = ((int)((player.getCollect() / (player.getCollect() + outer.getCollect())) * 100)).ToString() + "%";
            string thing = ((int)(timer) / 60).ToString() + ":";
            if (((int)(timer) % 60) < 10)
            {
                thing = thing + "0" + ((int)(timer) % 60).ToString();
            }
            else
            {
                thing = thing + ((int)(timer) % 60).ToString();
            }
            timething.text = thing;
        }
    }

    public void setLevel(string r, string c)
    {
        world = r;
        level = c;
    }

    public void progress()
    {
        if(player.getCollect() + outer.getCollect() == 0)
        {
            percentage.text = "100%";
        }
        else
        {
            percentage.text = ((int)((player.getCollect() / (player.getCollect() + outer.getCollect())) * 100)).ToString() + "%";
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
