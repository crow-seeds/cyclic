using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spammer : MonoBehaviour
{
    public Text amountCounter;
    public int amountLeft;
    bool inHitbox = false;
    public ring circle;
    public NewBehaviourScript player;
    public GameObject shooter;
    public GameObject childspammer;
    public inputManager manager;
    public float startTime = 0;
    public float expirationTime = 1;
    public Material defaultthing;
    public movingRing ring;

    bool mobile = false;
    bool mobileSwap = true;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        childspammer.GetComponent<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color;
        childspammer.GetComponent<SpriteRenderer>().material = this.GetComponent<SpriteRenderer>().material;
        Color n = shooter.gameObject.GetComponent<SpriteRenderer>().color;
        if (n.r + n.g + n.b > 1.5)
        {
            amountCounter.color = new Color(0, 0, 0);
        }
        else
        {
            amountCounter.color = new Color(1, 1, 1);
        }

        if(PlayerPrefs.GetInt("isMobile", 0) == 1)
        {
            mobile = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if(amountCounter.text == "" && amountLeft > 0)
        {
            amountCounter.text = amountLeft.ToString() + "x";
        }
        for (int i = 0; i < manager.swap.Count; i++)
        {
            if (Input.GetKeyDown(manager.swap[i]) && inHitbox)
            {
                if (PlayerPrefs.GetInt("lowdetail", 0) == 0)
                {
                    movingRing r = Instantiate(ring, Vector3.zero, Quaternion.Euler(0, 0, 0));
                    r.setVelocity(3f / 32f);
                }
                amountLeft--;
                player.collect++;
                amountCounter.text = amountLeft.ToString() + "x";
                if(amountLeft <= 0)
                {
                    amountCounter.text = "";
                    this.gameObject.SetActive(false);
                }
            }
        }

        if (mobile)
        {
            var touch = Input.touches[0];

            if (touch.position.x > Screen.width * 0.78125f && touch.position.y < Screen.height * 0.388f && mobileSwap && inHitbox)
            {
                if (PlayerPrefs.GetInt("lowdetail", 0) == 0)
                {
                    movingRing r = Instantiate(ring, Vector3.zero, Quaternion.Euler(0, 0, 0));
                    r.setVelocity(3f / 32f);
                }
                amountLeft--;
                player.collect++;
                amountCounter.text = amountLeft.ToString() + "x";
                if (amountLeft <= 0)
                {
                    amountCounter.text = "";
                    this.gameObject.SetActive(false);
                }
                mobileSwap = false;
            }
            else if (Input.touchCount < 1 && !mobileSwap)
            {
                mobileSwap = true;
            }
        }

        if (startTime >= expirationTime && this.GetComponent<Transform>().localPosition != new Vector3(-310, 0, 0))
        {
            circle.missed += amountLeft;
            amountCounter.text = "";
            player.loseHexagon();
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "curve")
        {
            inHitbox = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "curve")
        {
            inHitbox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "curve")
        {
            inHitbox = false;
        }
    }


    public void setExpiration(float s)
    {
        expirationTime = startTime + s;
    }
}
