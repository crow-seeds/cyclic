using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdCircle : MonoBehaviour
{
    public GameObject holdCircleRing;
    public GameObject holdCircleAnimation;
    public NewBehaviourScript player;
    public ring circle;

    float timeAlive = 1;
    float timeNeeded = 1;
    float aliveTimer = 0;
    float stayTimer = 0;

    float disappearTimer = 0;
    float appearTimer = 0;

    bool inCircle = false;
    bool disappearing = false;
    bool appearing = true;

    bool isOrginial = false;
    public movingRing moving;

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<Transform>().localPosition.x < -300)
        {
            isOrginial = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("lowdetail", 0) == 0)
        {
            holdCircleRing.transform.Rotate(Vector3.forward, Time.deltaTime * 30);
        }

        if (appearing && !isOrginial)
        {
            appearTimer += Time.deltaTime;
            holdCircleRing.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, Time.deltaTime * 6.375f);
            if (appearTimer >= .2f)
            {
                appearing = false;
            }
        }

        if (!disappearing && !isOrginial)
        {
            aliveTimer += Time.deltaTime;

            if (stayTimer >= timeNeeded)
            {
                player.collect++;
                player.collect += Mathf.FloorToInt(timeNeeded);
                disappearing = true;
                if (PlayerPrefs.GetInt("lowdetail", 0) == 0)
                {
                    movingRing r = Instantiate(moving, Vector3.zero, Quaternion.Euler(0, 0, 0));
                    r.setVelocity(3f / 32f);
                }
            }

            if(aliveTimer >= timeAlive)
            {
                circle.missed += Mathf.FloorToInt(timeNeeded - stayTimer);
                player.loseHexagon();
                circle.missed++;
                disappearing = true;
            }

            if (inCircle)
            {
                stayTimer += Time.deltaTime;
                holdCircleAnimation.transform.localScale = new Vector3(stayTimer / timeNeeded * 5.6f, stayTimer / timeNeeded * 5.6f, 0);
            }
        }

        if (disappearing && !isOrginial)
        {
            disappearTimer += Time.deltaTime;
            holdCircleAnimation.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, Time.deltaTime * 2.5f);
            holdCircleRing.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, Time.deltaTime * 6.375f);

            if(disappearTimer >= .2f)
            {
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }

    public void setTimes(float alive, float needed)
    {
        timeAlive = alive;
        timeNeeded = needed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "curve")
        {
            inCircle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "curve")
        {
            inCircle = false;
        }
    }
}
