using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooterv2 : MonoBehaviour
{

    public Text percentage;
    public GameObject shootthing;
    public bullet bullets;
    public spike spikes;
    public sticky stickys;
    public spammer spammers;
    public holdCircle holdCircles;
    public hexagon hexagons;
    public hexagon heptagons;
    public tele teles;
    public colorFader fader;
    public rotator rotatorthing;
    public mover moverthing;
    public scaler scalerthing;
    public GameObject winscreen;
    public ring outer;
    public NewBehaviourScript player;
    public GameObject playerObject;
    public float timer = 0;
    public Camera camerathing;

    public GameObject swapwarning;

    public Text percent;
    public Text missed;
    public Text hit;
    public Text timething;
    public Text spikehit;
    public Text spikedodge;

    public Text spamAmount;

    int achievementnumber = 0;
    public Text[] visualunlocks = new Text[5];
    public Text completeText;

    string world = "0";
    string level = "0";

    public List<GameObject> defaults = new List<GameObject>();
    public List<GameObject> objects = new List<GameObject>();

    public GameObject textDefault;
    public GameObject canvas;

    public GameObject swapIcon;
    public GameObject swapX;

    public GameObject temp;
    public NGHelper ng;
    int[] achievementIDs = new int[] {66936, 66936, 66937, 66938, 66939, 66940, 66941, 66942, 66943, 66944, 66945, 66946, 66947, 66948, 66949, 66950, 66951, 66952, 66953, 66954, 66955, 66956, 66957, 66958, 66959, 66960, 66961, 66962, 66963, 66964, 66965, 66966, 66967, 67017, 67018, 67019, 67020, 66968, 66969, 66970, 66971, 67012, 67013, 67014, 67015, 67021, 67022, 67023, 67024, 67025, 67016 };


    public IEnumerator shoot(float angle, float speed, float time)
    {
        yield return new WaitForSeconds(time);
        bullet b = Instantiate(bullets, Vector3.zero, Quaternion.Euler(0, 0, angle));
        b.setSpeed(speed);
        shootthing.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
    }

    public IEnumerator hexagon(float angle, float speed, float time)
    {
        yield return new WaitForSeconds(time);
        hexagon h = Instantiate(hexagons, Vector3.zero, Quaternion.Euler(0, 0, angle));
        h.setSpeed(speed);
        shootthing.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
    }

    public IEnumerator heptagon(float angle, float speed, float time)
    {
        yield return new WaitForSeconds(time);
        hexagon h = Instantiate(heptagons, Vector3.zero, Quaternion.Euler(0, 0, angle));
        h.setSpeed(speed);
        shootthing.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
    }

    public IEnumerator aimshoot(float buffer, float speed, float time)
    {
        yield return new WaitForSeconds(time);
        bullet b = Instantiate(bullets, Vector3.zero, Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + buffer));
        b.setSpeed(speed);
        shootthing.transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + buffer + 180);
    }

    public IEnumerator shootspikes(float angle, float speed, float time)
    {
        yield return new WaitForSeconds(time);
        spike s = Instantiate(spikes, Vector3.zero, Quaternion.Euler(0, 0, angle));
        s.setSpeed(speed);
        shootthing.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
    }

    public IEnumerator shootsticky(float angle, float speed, float time, float duration)
    {
        yield return new WaitForSeconds(time);
        sticky s = Instantiate(stickys, Vector3.zero, Quaternion.Euler(0, 0, angle));
        s.setExpiration(duration);
        s.setSpeed(speed);
        shootthing.transform.rotation = Quaternion.Euler(0, 0, angle + 180);
    }

    public IEnumerator instantSticky(float angle, float duration)
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 spawn = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle) * 4.644f, Mathf.Cos(Mathf.Deg2Rad * angle) * -4.644f, 0);
        sticky s = Instantiate(stickys, spawn, Quaternion.Euler(0, 0, angle));
        s.setExpiration(duration);
    }

    public IEnumerator spammer(float angle, float time, float duration, int amount, int red, int green, int blue, string blending)
    {
        yield return new WaitForSeconds(time);
        spammer s = Instantiate(spammers, Vector3.zero, Quaternion.Euler(0, 0, angle + 90));
        if(blending == "false") { s.gameObject.GetComponentInChildren<SpriteRenderer>().material = s.defaultthing; }
        s.setExpiration(duration);
        s.amountLeft = amount;
        s.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(red/255f, green/255f, blue/255f, 75/255f);
        spamAmount.text = amount.ToString() + "x";
    }

    public IEnumerator localspammer(float buffer, float time, float duration, int amount, int red, int green, int blue, string blending)
    {
        yield return new WaitForSeconds(time);
        spammer s = Instantiate(spammers, Vector3.zero, Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + 90 + buffer));
        if (blending == "false") { s.gameObject.GetComponentInChildren<SpriteRenderer>().material = s.defaultthing; }
        s.setExpiration(duration);
        s.amountLeft = amount;
        s.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(red / 255f, green / 255f, blue / 255f, 75 / 255f);
        spamAmount.text = amount.ToString() + "x";
    }

    public IEnumerator holdCircle(float time, float aliveDuration, float needDuration, float xpos, float ypos, float size)
    {
        yield return new WaitForSeconds(time);
        Vector3 pos = new Vector3(xpos, ypos);
        holdCircle h = Instantiate(holdCircles, pos, Quaternion.Euler(0, 0, 0));
        h.gameObject.transform.localScale = new Vector3(size, size);
        h.setTimes(aliveDuration, needDuration);
    }

        public IEnumerator teleporter(float angle, float angle2, float speed, float time)
    {
        yield return new WaitForSeconds(time);
        tele t = Instantiate(teles, Vector3.zero, Quaternion.Euler(0, 0, angle));
        tele t2 = Instantiate(teles, Vector3.zero, Quaternion.Euler(0, 0, angle2));
        t.setSpeed(speed);
        t2.setSpeed(speed);
        t.setOutput(t2.bullets);
        t2.setOutput(t.bullets);
    }

    public IEnumerator aimspikes(float buffer, float speed, float time)
    {
        yield return new WaitForSeconds(time);
        spike s = Instantiate(spikes, Vector3.zero, Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + buffer));
        s.setSpeed(speed);
        shootthing.transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + buffer + 180);
    }

    public IEnumerator conespikes(float buffer, float speed, float time, int amount)
    {
        yield return new WaitForSeconds(time);
        float start = 0;
        if(amount % 2 == 0)
        {
            start = (buffer / 2) - (amount / 2)*buffer;
        }
        else
        {
            start = (int)(amount / 2) * -1 * buffer;
        }

        for(int i = 0; i < amount; i++)
        {
            spike s = Instantiate(spikes, Vector3.zero, Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + start + buffer * i));
            s.setSpeed(speed);
            shootthing.transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + 180);
        }
    }

    public IEnumerator conespikes(float buffer, float speed, float time, int amount, float angleFromPlayer)
    {
        yield return new WaitForSeconds(time);
        float start = 0;
        if (amount % 2 == 0)
        {
            start = (buffer / 2) - (amount / 2) * buffer + angleFromPlayer;
        }
        else
        {
            start = (int)(amount / 2) * -1 * buffer;
        }

        for (int i = 0; i < amount; i++)
        {
            spike s = Instantiate(spikes, Vector3.zero, Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + start + buffer * i));
            s.setSpeed(speed);
            shootthing.transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<Transform>().eulerAngles.z + 180);
        }
    }

    public IEnumerator multishot(float angle, float speed, float time, int amount, float delay)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < amount; i++)
        {
            StartCoroutine(shoot(angle, speed, delay * i));
        }
    }

    public IEnumerator multishotspikes(float angle, float speed, float time, int amount, float delay)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < amount; i++)
        {
            StartCoroutine(shootspikes(angle, speed, delay * i));
        }
    }

    public IEnumerator spread(float angle, float speed, float time, int amount, float delay, string direction, float spacing)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < amount; i++)
        {
            if (direction == "Clockwise")
            {
                StartCoroutine(shoot(angle - i * spacing, speed, delay * i));
            }
            else
            {
                StartCoroutine(shoot(angle + i * spacing, speed, delay * i));
            }

        }
    }

    public IEnumerator spreadspikes(float angle, float speed, float time, int amount, float delay, string direction, float spacing)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < amount; i++)
        {
            if (direction == "Clockwise")
            {
                StartCoroutine(shootspikes(angle - i * spacing, speed, delay * i));
            }
            else
            {
                StartCoroutine(shootspikes(angle + i * spacing, speed, delay * i));
            }

        }
    }

    public IEnumerator zigshot(float angle, float speed, float time, int amount, float delay, float spacing)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < amount; i++)
        {
            if (i % 2 == 0)
            {
                StartCoroutine(shoot(angle, speed, delay * i));
            }
            else
            {
                StartCoroutine(shoot(angle + spacing, speed, delay * i));
            }
        }
    }

    public IEnumerator zigshotspikes(float angle, float speed, float time, int amount, float delay, float spacing)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < amount; i++)
        {
            if (i % 2 == 0)
            {
                StartCoroutine(shootspikes(angle, speed, delay * i));
            }
            else
            {
                StartCoroutine(shootspikes(angle + spacing, speed, delay * i));
            }
        }
    }

    public IEnumerator colorChangeCircle(float red, float green, float blue, float fadetime, float time)
    {
        yield return new WaitForSeconds(time);
        colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
        f.setColor(new Color(red / 255, green / 255, blue / 255));
        f.setMode("circle");
        f.setFade(fadetime);
    }

    public IEnumerator colorChangeBullets(float red, float green, float blue, float fadetime, float time)
    {
        yield return new WaitForSeconds(time);
        colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
        f.setColor(new Color(red / 255, green / 255, blue / 255));
        f.setMode("bullets");
        f.setFade(fadetime);
    }

    public IEnumerator colorChangeBackground(float red, float green, float blue, float fadetime, float time)
    {
        yield return new WaitForSeconds(time);
        colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
        f.setColor(new Color(red / 255, green / 255, blue / 255));
        f.setMode("background");
        f.setFade(fadetime);
    }

    public IEnumerator colorChangeShooter(float red, float green, float blue, float fadetime, float time, float alpha)
    {
        yield return new WaitForSeconds(time);
        colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
        f.setColor(new Color(red / 255, green / 255, blue / 255, alpha / 255));
        f.setMode("shoot");
        f.setFade(fadetime);
    }

    public IEnumerator colorChangeOverlay(float red, float green, float blue, float fadetime, float time, float alpha)
    {
        yield return new WaitForSeconds(time);
        colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
        f.setColor(new Color(red / 255, green / 255, blue / 255, alpha / 255));
        f.setMode("overlay");
        f.setFade(fadetime);
    }

    public IEnumerator colorChangeInner(float red, float green, float blue, float fadetime, float time, float alpha)
    {
        yield return new WaitForSeconds(time);
        colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
        f.setColor(new Color(red / 255, green / 255, blue / 255, alpha / 255));
        f.setMode("inner");
        f.setFade(fadetime);
    }

    public IEnumerator createObject(float xpos, float ypos, float zpos, float length, float height, float rotation, float red, float green, float blue, float alpha, int objectid, float time, bool blending)
    {
        yield return new WaitForSeconds(time);
        GameObject s = Instantiate(defaults[objectid], new Vector3(xpos, ypos, zpos), Quaternion.Euler(0, 0, rotation));
        s.GetComponent<SpriteRenderer>().color = new Color(red / 255, green / 255, blue / 255, alpha / 255);
        s.transform.localScale = new Vector3(length, height);
        s.GetComponent<SpriteRenderer>().sortingOrder = (int)(zpos * -1 * 10);
        if (blending) { s.GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Materials/blending"); }
        objects.Add(s);
    }

    public IEnumerator createObject(float xpos, float ypos, float zpos, float length, float height, float rotation, float red, float green, float blue, float alpha, int objectid, float time, string[] group, bool blending)
    {
        yield return new WaitForSeconds(time);
        GameObject s = Instantiate(defaults[objectid], new Vector3(xpos, ypos, zpos), Quaternion.Euler(0, 0, rotation));
        s.GetComponent<group>().setId(group);
        s.GetComponent<SpriteRenderer>().color = new Color(red / 255, green / 255, blue / 255, alpha / 255);
        s.GetComponent<SpriteRenderer>().sortingOrder = (int)(zpos*-1*10);
        s.transform.localScale = new Vector3(length, height);
        if (blending) { s.GetComponent<SpriteRenderer>().material = Resources.Load<Material>("Materials/blending"); }
        objects.Add(s);
    }

    public IEnumerator colorChangeObject(float red, float green, float blue, float alpha, float fadetime, float time, int id)
    {
        yield return new WaitForSeconds(time);
        colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
        f.setMode("other");
        f.setOther(objects[id]);
        f.setColor(new Color(red / 255, green / 255, blue / 255, alpha / 255));
        f.setFade(fadetime);
    }

    public IEnumerator createText(float xpos, float ypos, float zpos, float rotation, float red, float green, float blue, float alpha, float time, string text)
    {
        yield return new WaitForSeconds(time);
        GameObject s = Instantiate(textDefault);
        s.transform.parent = canvas.transform;
        s.GetComponent<RectTransform>().localPosition = new Vector3(xpos, ypos, zpos);
        s.GetComponent<Text>().color = new Color(red / 255, green / 255, blue / 255, alpha / 255);
        s.GetComponent<Text>().text = text;
        s.transform.localScale = new Vector3(1, 1);
        objects.Add(s);
    }

    public IEnumerator createText(float xpos, float ypos, float zpos, float rotation, float red, float green, float blue, float alpha, float time, string text, string[] id)
    {
        yield return new WaitForSeconds(time);
        GameObject s = Instantiate(textDefault);
        s.transform.parent = canvas.transform;
        s.GetComponent<RectTransform>().localPosition = new Vector3(xpos, ypos, zpos);
        s.GetComponent<Text>().color = new Color(red / 255, green / 255, blue / 255, alpha / 255);
        s.GetComponent<Text>().text = text;
        s.transform.localScale = new Vector3(1, 1);
        s.GetComponent<group>().setId(id);
        objects.Add(s);
    }

    

    public void enableBulletCollision()
    {
        bullets.collision = true;
    }

    public IEnumerator changeStyle(int style, float time)
    {
        yield return new WaitForSeconds(time);
        if (style == 0)
        {
            bullets.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Circles/glowcircle");
            spikes.spikesprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Triangles/thicctriangle");
            stickys.stickysprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Stickys/thiccsticky");
            teles.telesprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Teles/thiccsquare");
            hexagons.hexagonsprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Hexagons/thicchexagon");
            heptagons.hexagonsprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Heptagons/thiccheptagon");
        }
        else if(style == 1)
        {
            bullets.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Circles/fillcircle");
            spikes.spikesprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Triangles/filltriangle");
            stickys.stickysprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Stickys/fillsticky");
            teles.telesprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Teles/fillsquare");
            hexagons.hexagonsprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Hexagons/fillhexagon");
            heptagons.hexagonsprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Heptagons/fillheptagon");
        }
    }

    public IEnumerator deleteObject(int upper, int lower, float time)
    {
        yield return new WaitForSeconds(time);
        for(int i = upper; i >= lower; i--)
        {
            Destroy(objects[i]);
            objects.RemoveAt(i);
        }
    }

    public IEnumerator toggleGroup(int id, float time, string state)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject p in objects)
        {
            if (p.GetComponent<group>() != null && p.GetComponent<group>().getId().Contains(id))
            {
                if (state == "off")
                {
                    
                    p.SetActive(false);
                }
                else
                {
                    p.SetActive(true);
                }
            }   
        }
    }

    public IEnumerator colorGroup(float time, float fadetime, int id, float red, float green, float blue, float alpha)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject p in objects){
            if(p.GetComponent<group>() != null && p.GetComponent<group>().getId().Contains(id))
            {
                colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
                f.setMode("other");
                f.setOther(p);
                f.setColor(new Color(red / 255, green / 255, blue / 255, alpha / 255));
                f.setFade(fadetime);
            }
        }
    }

    public IEnumerator alphaGroup(float time, float fadetime, int id, float alpha)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject p in objects)
        {
            if (p.GetComponent<group>() != null && p.GetComponent<group>().getId().Contains(id))
            {
                colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
                f.setMode("other");
                f.setOther(p);
                f.setColor(new Color(p.GetComponent<SpriteRenderer>().color.r, p.GetComponent<SpriteRenderer>().color.g, p.GetComponent<SpriteRenderer>().color.b, alpha / 255));
                f.setFade(fadetime);
            }
        }
    }

    public IEnumerator rotateGameplay(float time, float duration, float degrees, float xpos, float ypos)
    {
        yield return new WaitForSeconds(time);

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("spike"))
        {
            if (f.transform.localPosition.x > -200)
            {
                rotator r = Instantiate(rotatorthing, Vector3.zero, Quaternion.Euler(0, 0, 0));
                r.rotateAround(xpos, ypos);
                r.setObject(f);
                r.setDuration(duration);
                r.setDegrees(degrees);
            }
            
        }

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("bullet"))
        {
            if (f.transform.localPosition.x > -200)
            {
                rotator r = Instantiate(rotatorthing, Vector3.zero, Quaternion.Euler(0, 0, 0));
                r.rotateAround(xpos, ypos);
                r.setObject(f);
                r.setDuration(duration);
                r.setDegrees(degrees);
            }
        }

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("telemain"))
        {
            if (f.transform.localPosition.x > -200)
            {
                rotator r = Instantiate(rotatorthing, Vector3.zero, Quaternion.Euler(0, 0, 0));
                r.rotateAround(xpos, ypos);
                r.setObject(f);
                r.setDuration(duration);
                r.setDegrees(degrees);
            }
        }
    }

    public IEnumerator slowGameplay(float time, float speed)
    {
        yield return new WaitForSeconds(time);

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("spike"))
        {
            if(f.GetComponent<spike>() != null)
            {
                f.GetComponent<spike>().timewarp(speed);
            }
            else
            {
                f.GetComponent<sticky>().timewarp(speed);
            }
        }

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("bullet"))
        {
            f.GetComponent<bullet>().timewarp(speed);
        }

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("telemain"))
        {
            f.GetComponent<tele>().timewarp(speed);
        }
    }

    public IEnumerator rotateGroupSelf(float time, float duration, int id, float degrees)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject p in objects)
        {
            if (p.GetComponent<group>() != null && p.GetComponent<group>().getId().Contains(id))
            {
                if (duration <= .1)
                {
                    p.transform.rotation = Quaternion.Euler(0, 0, p.transform.GetComponent<Transform>().eulerAngles.z + degrees); ;
                }
                else
                {
                    rotator r = Instantiate(rotatorthing, Vector3.zero, Quaternion.Euler(0, 0, 0));
                    r.setObject(p);
                    r.setDuration(duration);
                    r.setDegrees(degrees);
                }
            }
        }
    }

    public IEnumerator rotateGroupAround(float time, float duration, int id, float degrees, float xpos, float ypos)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject p in objects)
        {
            if (p.GetComponent<group>() != null && p.GetComponent<group>().getId().Contains(id))
            {
                rotator r = Instantiate(rotatorthing, Vector3.zero, Quaternion.Euler(0, 0, 0));
                r.rotateAround(xpos, ypos);
                r.setObject(p);
                r.setDuration(duration);
                r.setDegrees(degrees);
            }
        }
    }

    public IEnumerator moveGroup(float time, float duration, int id, float xDisp, float yDisp)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject p in objects)
        {
            if (p.GetComponent<group>() != null && p.GetComponent<group>().getId().Contains(id))
            {
                if (duration <= .1)
                {
                    p.transform.Translate(xDisp, yDisp, 0, Space.World);
                }
                else
                {
                    mover m = Instantiate(moverthing, Vector3.zero, Quaternion.Euler(0, 0, 0));
                    m.setDisplacement(xDisp, yDisp);
                    m.setObject(p);
                    m.setDuration(duration);
                }
            }
        }
    }

    public IEnumerator scaleGroup(float time, float duration, int id, float xScale, float yScale)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject p in objects)
        {
            if (p.GetComponent<group>() != null && p.GetComponent<group>().getId().Contains(id))
            {
                if (duration <= .1)
                {
                    p.transform.localScale = new Vector3(xScale, yScale, 0);
                }
                else
                {
                    scaler s = Instantiate(scalerthing, Vector3.zero, Quaternion.Euler(0, 0, 0));
                    s.setDisplacement(xScale, yScale);
                    s.setObject(p);
                    s.setDuration(duration);
                }
            }
        }
    }

    public IEnumerator maskGroup(float time, int id, float zposStart, float zposEnd)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject p in objects)
        {
            if (p.GetComponent<group>() != null && p.GetComponent<group>().getId().Contains(id))
            {
                p.AddComponent(typeof(SpriteMask));
                p.GetComponent<SpriteMask>().sprite = p.GetComponent<SpriteRenderer>().sprite;
                p.GetComponent<SpriteMask>().isCustomRangeActive = true;
                p.GetComponent<SpriteMask>().frontSortingOrder = (int)(-1 * zposStart * 10);
                p.GetComponent<SpriteMask>().backSortingOrder = (int)(-1 * zposEnd * 10);
                Destroy(p.GetComponent<SpriteRenderer>());
            }
        }
    }

    public IEnumerator swapSetting(float time, string enable)
    {
        yield return new WaitForSeconds(time);
        if (enable == "false")
        {
            Color n = new Color(0, 0, 0, .8f);
            if(camerathing.backgroundColor.r + camerathing.backgroundColor.g + camerathing.backgroundColor.b <= 1.5)
            {
                n = new Color(1, 1, 1, .8f);
            }
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setMode("other");
            f.setOther(swapIcon);
            f.setFade(.5f);
            f.setColor(new Color(.5f, .5f, .5f, .7f));
            colorFader f2 = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f2.setMode("other");
            f2.setOther(swapX);
            f2.setFade(.5f);
            f2.setColor(n);
            player.changeSwap(false);
        }
        else
        {
            colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f.setMode("other");
            f.setOther(swapIcon);
            f.setFade(.5f);
            f.setColor(new Color(.5f, .5f, .5f, 0f));
            colorFader f2 = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
            f2.setMode("other");
            f2.setOther(swapX);
            f2.setFade(.5f);
            f2.setColor(new Color(0,0,0,0));
            player.changeSwap(true);
        }
    }

    public IEnumerator invisibleTeleport(float leftAngle, float rightAngle, float destination, float time)
    {
        yield return new WaitForSeconds(time);
        if (leftAngle + 360 == rightAngle || rightAngle + 360 == leftAngle)
        {
            playerObject.transform.RotateAround(Vector3.zero, Vector3.forward, destination - playerObject.GetComponent<Transform>().localRotation.eulerAngles.z);
        } else if (playerObject.GetComponent<Transform>().localRotation.eulerAngles.z == leftAngle || playerObject.GetComponent<Transform>().localRotation.eulerAngles.z == rightAngle)
        {
            playerObject.transform.RotateAround(Vector3.zero, Vector3.forward, destination - playerObject.GetComponent<Transform>().localRotation.eulerAngles.z);
        }
    }

    public IEnumerator gonnaWinLevel(float time, float startpos)
    {
        yield return new WaitForSeconds(time - startpos);

        int oldpercent = 0;
        string nightcore = "";

        if(PlayerPrefs.GetInt("nightcore", 0) == 1)
        {
            nightcore = "nightcore";
        }

        if(PlayerPrefs.GetInt("practice",0) != 1)
        {
            oldpercent = PlayerPrefs.GetInt(nightcore + world.ToString() + "," + level.ToString());
            if (PlayerPrefs.GetInt(nightcore + world.ToString() + "," + level.ToString()) < ((int)(((player.getCollect() + outer.collect / 20) / (player.getCollect() + outer.getCollect() + player.missed + outer.collect / 20)) * 100)))
            {
                PlayerPrefs.SetInt(nightcore + world.ToString() + "," + level.ToString(), (int)(((player.getCollect() + outer.collect / 20) / (player.getCollect() + outer.getCollect() + player.missed + outer.collect / 20)) * 100));
            }
        }
        

        winscreen.SetActive(true);
        player.won();
        colorFader f = Instantiate(fader, Vector3.zero, Quaternion.Euler(0, 0, 0));
        f.setMode("win");
        f.setFade(3);

        if (PlayerPrefs.GetInt("practice", 0) != 1)
        {
            missed.text = outer.getCollect().ToString();
            hit.text = (player.getCollect()-1).ToString();
            spikedodge.text = outer.collect.ToString();
            spikehit.text = player.missed.ToString();
        }
        else
        {
            missed.text = "N/A";
            hit.text = "N/A";
            spikedodge.text = "N/A";
            spikehit.text = "N/A";
        }

        PlayerPrefs.SetFloat("distanceTraveled", PlayerPrefs.GetFloat("distanceTraveled", 0) + player.distance());

        if (PlayerPrefs.GetInt("practice", 0) != 1)
        {
            int percentage = (int)(((player.getCollect() + outer.collect / 20) / (player.getCollect() + outer.getCollect() + player.missed + outer.collect / 20)) * 100);
            percent.text = percentage.ToString() + "%";

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

            if (percentage >= 90 && nightcore == "")
            {
                completeText.text = "Level Complete!";
                switch (world + "|" + level)
                {
                    case "1|1":
                        unlock(1);
                        PlayerPrefs.SetInt("achievement1", 1);
                        break;
                    case "1|2":
                        unlock(2);
                        PlayerPrefs.SetInt("achievement2", 1);
                        break;
                    case "1|3":
                        unlock(3);
                        PlayerPrefs.SetInt("achievement3", 1);
                        break;
                    case "1|4":
                        unlock(27);
                        PlayerPrefs.SetInt("achievement27", 1);
                        break;
                    case "2|1":
                        unlock(4);
                        PlayerPrefs.SetInt("achievement4", 1);
                        break;
                    case "2|2":
                        unlock(5);
                        PlayerPrefs.SetInt("achievement5", 1);
                        break;
                    case "2|3":
                        unlock(6);
                        PlayerPrefs.SetInt("achievement6", 1);
                        break;
                    case "2|4":
                        unlock(7);
                        PlayerPrefs.SetInt("achievement7", 1);
                        break;
                    case "2|5":
                        unlock(18);
                        PlayerPrefs.SetInt("achievement18", 1);
                        break;
                    case "3|1":
                        unlock(8);
                        PlayerPrefs.SetInt("achievement8", 1);
                        break;
                    case "3|2":
                        unlock(14);
                        PlayerPrefs.SetInt("achievement14", 1);
                        break;
                    case "3|3":
                        unlock(23);
                        PlayerPrefs.SetInt("achievement23", 1);
                        break;
                    case "3|4":
                        unlock(37);
                        PlayerPrefs.SetInt("achievement37", 1);
                        break;
                    case "4|1":
                        unlock(25);
                        PlayerPrefs.SetInt("achievement25", 1);
                        break;
                    case "4|2":
                        unlock(29);
                        PlayerPrefs.SetInt("achievement29", 1);
                        break;
                    case "4|3":
                        unlock(39);
                        PlayerPrefs.SetInt("achievement39", 1);
                        break;
                    case "5|1":
                        unlock(30);
                        PlayerPrefs.SetInt("achievement30", 1);
                        break;
                }
            }

            if (player.hasHexagon)
            {
                percent.text += "+";

                if(nightcore != "nightcore" && PlayerPrefs.GetInt("hexagon" + world.ToString() + "," + level.ToString(), 0) == 0)
                {
                    PlayerPrefs.SetInt("hexagon" + world.ToString() + "," + level.ToString(), 1);
                    PlayerPrefs.SetInt("hexagonCount", PlayerPrefs.GetInt("hexagonCount", 0) + 1);
                    int hexCount = PlayerPrefs.GetInt("hexagonCount", 0);

                    if(hexCount == 1)
                    {
                        unlock(42);
                        PlayerPrefs.SetInt("achievement42", 1);
                    }

                    if(hexCount == 3)
                    {
                        unlock(43);
                        PlayerPrefs.SetInt("achievement43", 1);
                    }

                    if(hexCount == 6)
                    {
                        unlock(41);
                        PlayerPrefs.SetInt("achievement41", 1);
                    }

                    if(hexCount == 9)
                    {
                        unlock(44);
                        PlayerPrefs.SetInt("achievement44", 1);
                    }

                    if (hexCount == 12)
                    {
                        unlock(50);
                        PlayerPrefs.SetInt("achievement50", 1);
                    }
                }
                else if(nightcore == "nightcore" && PlayerPrefs.GetInt("nightcorehexagon" + world.ToString() + "," + level.ToString(), 0) == 0)
                {
                    PlayerPrefs.SetInt("nightcorehexagon" + world.ToString() + "," + level.ToString(), 1);
                    PlayerPrefs.SetInt("nightcorehexagonCount", PlayerPrefs.GetInt("nightcorehexagonCount", 0) + 1);
                    int hexCount = PlayerPrefs.GetInt("nightcorehexagonCount", 0);

                    if(hexCount == 1)
                    {
                        unlock(45);
                        PlayerPrefs.SetInt("achievement45", 1);
                    }

                    if(hexCount == 3)
                    {
                        unlock(46);
                        PlayerPrefs.SetInt("achievement46", 1);
                    }

                    if (hexCount == 6)
                    {
                        unlock(47);
                        PlayerPrefs.SetInt("achievement47", 1);
                    }

                    if (hexCount == 9)
                    {
                        unlock(48);
                        PlayerPrefs.SetInt("achievement48", 1);
                    }

                    if (hexCount == 12)
                    {
                        unlock(49);
                        PlayerPrefs.SetInt("achievement49", 1);
                    }
                }
            }

            if (nightcore == "nightcore" && percentage >= 90)
            {
                completeText.text = "Nightcore Complete!";
                if(oldpercent < 90)
                {
                    PlayerPrefs.SetInt("nightcoreCount", PlayerPrefs.GetInt("nightcoreCount", 0) + 1);
                    int jeff = PlayerPrefs.GetInt("nightcoreCount", 0);

                    if(jeff == 1)
                    {
                        unlock(33);
                        PlayerPrefs.SetInt("achievement33", 1);
                    }

                    if(jeff == 5)
                    {
                        unlock(34);
                        PlayerPrefs.SetInt("achievement34", 1);
                    }

                    if(jeff == 10)
                    {
                        unlock(35);
                        PlayerPrefs.SetInt("achievement35", 1);
                    }

                    if(jeff == 15)
                    {
                        unlock(36);
                        PlayerPrefs.SetInt("achievement36", 1);
                    }
                }

            }

            if (PlayerPrefs.GetInt("achievement1") == 1 && PlayerPrefs.GetInt("achievement2") == 1 && PlayerPrefs.GetInt("achievement3") == 1 && PlayerPrefs.GetInt("achievement27") == 1)
            {
                unlock(31);
                PlayerPrefs.SetInt("achievement31", 1);
            }

            if (PlayerPrefs.GetInt("achievement4") == 1 && PlayerPrefs.GetInt("achievement5") == 1 && PlayerPrefs.GetInt("achievement6") == 1 && PlayerPrefs.GetInt("achievement7") == 1 && PlayerPrefs.GetInt("achievement18") == 1)
            {
                unlock(32);
                PlayerPrefs.SetInt("achievement32", 1);
            }

            if (PlayerPrefs.GetInt("achievement8") == 1 && PlayerPrefs.GetInt("achievement14") == 1 && PlayerPrefs.GetInt("achievement23") == 1 && PlayerPrefs.GetInt("achievement37") == 1)
            {
                unlock(38);
                PlayerPrefs.SetInt("achievement38", 1);
            }

            if (PlayerPrefs.GetInt("achievement25") == 1 && PlayerPrefs.GetInt("achievement29") == 1 && PlayerPrefs.GetInt("achievement39") == 1)
            {
                unlock(40);
                PlayerPrefs.SetInt("achievement40", 1);
            }

            if (percentage == 100)
            {
                completeText.text = "Level Perfect!";
            }

            if (percentage == 100 && percentage > oldpercent && nightcore != "nightcore")
            {

                PlayerPrefs.SetInt("perfectCount", PlayerPrefs.GetInt("perfectCount", 0) + 1);
                int jeff = PlayerPrefs.GetInt("perfectCount", 0);

                if (jeff >= 1)
                {
                    unlock(9);
                    PlayerPrefs.SetInt("achievement9", 1);
                }

                if (jeff >= 3)
                {
                    unlock(10);
                    PlayerPrefs.SetInt("achievement10", 1);
                }

                if (jeff >= 5)
                {
                    unlock(11);
                    PlayerPrefs.SetInt("achievement11", 1);
                }
            }

            if (player.oneDirection() && percentage >= 90)
            {
                unlock(26);
                PlayerPrefs.SetInt("achievement26", 1);
            }

            PlayerPrefs.SetInt("ballCollection", PlayerPrefs.GetInt("ballCollection", 0) + (int)player.getCollect());
            PlayerPrefs.SetInt("spikeCollection", PlayerPrefs.GetInt("spikeCollection", 0) + (int)outer.collect);
            Debug.Log(PlayerPrefs.GetInt("spikeCollection", 0));

            int ballcount = PlayerPrefs.GetInt("ballCollection", 0);
            int spikecount = PlayerPrefs.GetInt("spikeCollection", 0);
            if (ballcount >= 2000)
            {
                unlock(12);
                PlayerPrefs.SetInt("achievement12", 1);
            }
            if (ballcount >= 5000)
            {
                unlock(13);
                PlayerPrefs.SetInt("achievement13", 1);
            }
            if (ballcount >= 10000)
            {
                unlock(19);
                PlayerPrefs.SetInt("achievement19", 1);
            }

            if (spikecount >= 10000)
            {
                unlock(24);
                PlayerPrefs.SetInt("achievement24", 1);
            }

            int totalPercent = options.calculatePercent();
            if (totalPercent >= 360)
            {
                unlock(15);
                PlayerPrefs.SetInt("achievement15", 1);
            }

            if (totalPercent >= 720)
            {
                unlock(16);
                PlayerPrefs.SetInt("achievement16", 1);
            }

            if (totalPercent >= 1080)
            {
                unlock(17);
                PlayerPrefs.SetInt("achievement17", 1);
            }

            if (totalPercent >= 1440)
            {
                unlock(28);
                PlayerPrefs.SetInt("achievement28", 1);
            }

            if (PlayerPrefs.GetFloat("distanceTraveled", 0) >= 28647.9)
            {
                unlock(20);
                PlayerPrefs.SetInt("achievement20", 1);
            }

            if (PlayerPrefs.GetFloat("distanceTraveled", 0) >= 114591.6)
            {
                unlock(21);
                PlayerPrefs.SetInt("achievement21", 1);
            }

            if (PlayerPrefs.GetFloat("distanceTraveled", 0) >= 572957.8)
            {
                unlock(22);
                PlayerPrefs.SetInt("achievement22", 1);
            }

        }
        else
        {
            completeText.text = "Practice Complete!";
            percent.text = "N/A";
            timething.text = "N/A";
        }
    }

    public void setLevel(string r, string c)
    {
        world = r;
        level = c;
    }

    public void progress()
    {
        if (player.getCollect() + outer.getCollect() + player.missed == 0)
        {
            percentage.text = "100%";
        }
        else
        {
            percentage.text = ((int)(((player.getCollect() + outer.collect/20) / (player.getCollect() + outer.getCollect() + player.missed + outer.collect/20)) * 100)).ToString() + "%";
        }

        if (player.hasHexagon)
        {
            percentage.text += "+";
        }
    }

    void unlock(int a)
    {
        if(PlayerPrefs.GetInt("achievement"+a.ToString()) != 1 && achievementnumber < 5)
        {
            visualunlocks[achievementnumber].gameObject.SetActive(true);
            if ((a >= 33 && a <= 36) || (a >= 45 && a <= 48))
            {
                visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>("Sprites/Decoration/Square");
                switch (a)
                {
                    case 33:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(1f, .75f, .5f);
                        break;
                    case 34:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(.75f, 1f, .5f);
                        break;
                    case 35:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(.5f, .75f, 1);
                        break;
                    case 36:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(.75f, .5f, 1);
                        break;
                    case 45:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(1f, .5f, .75f);
                        break;
                    case 46:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(.5f, .1f, .75f);
                        break;
                    case 47:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(1f, 1f, .5f);
                        break;
                    case 48:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(1f, .5f, .5f);
                        break;
                    case 49:
                        visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().color = new Color(.25f, .25f, .25f);
                        break;
                }
            }
            else if (a > 49)
            {
                visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>("Sprites/Skins/skin" + (a - 8).ToString());
            }
            else if (a > 36)
            {
                visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>("Sprites/Skins/skin" + (a - 3).ToString());
            }
            else
            {
                visualunlocks[achievementnumber].gameObject.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>("Sprites/Skins/skin" + (a + 1).ToString());
            }
            achievementnumber++;

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                newgroundAchievement(a);
            }
        }
    }

    void newgroundAchievement(int p)
    {
        Debug.Log(p);
        ng.unlockMedal(achievementIDs[p]);
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

