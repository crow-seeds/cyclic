using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menu : MonoBehaviour {
    //this code is fucking atrocious, but this menu is so fucking annoying to code. literally for some reason colliders would not work specifically in this scene which would have made this 30000 times easier

    public Color f;
    public Color f2;
    public Color f3;
    public Color f4;

    public GameObject area;
    public GameObject area2;
    public GameObject area3;
    public GameObject area4;

    public GameObject selection;
    public options optionclass;
    public NewBehaviourScript curve;
    public GameObject main;
    public GameObject option;
    public GameObject achievement;
    public GameObject statistics;
    public GameObject controls;

    bool yikes = false;
    bool spikes = false;
    bool dikes = false;
    bool hikes = false;
    public colorFader greg;
    Color t;

    // Use this for initialization
    void Start () {
        achievementGlowExit();
        optionGlowExit();
        levelGlowExit();
        exitGlowExit();
    }
	
	// Update is called once per frame
	void Update () {


        if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow)) && curve.GetComponent<Transform>().position.x > 2 && curve.GetComponent<Transform>().position.y < 0)
        {
            SceneManager.LoadScene("levelSelect");
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow)) && curve.GetComponent<Transform>().position.x > 2 && curve.GetComponent<Transform>().position.y > 0)
        {
            options();
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow)) && curve.GetComponent<Transform>().position.x < -2 && curve.GetComponent<Transform>().position.y < 0)
        {
            Application.Quit();
            
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow)) && curve.GetComponent<Transform>().position.x < -2 && curve.GetComponent<Transform>().position.y > 0)
        {
            achievements();

        }

        if (curve.GetComponent<Transform>().position.x > 2 && curve.GetComponent<Transform>().position.y < 0 && yikes == false)
        {
            iwanttoexplode();
            yikes = true;
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SceneManager.LoadScene("levelSelect");
            }
        }

        if (curve.GetComponent<Transform>().position.x < -2 && curve.GetComponent<Transform>().position.y > 0 && hikes == false)
        {
            iwanttoblowup();
            hikes = true;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                achievements();
            }
        }

        if (curve.GetComponent<Transform>().position.x < -2 && curve.GetComponent<Transform>().position.y < 0 && spikes == false)
        {
            iwanttocombust();
            spikes = true;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Application.Quit();
            }
        }

        if ((!(curve.GetComponent<Transform>().position.x < -2) || !(curve.GetComponent<Transform>().position.y < 0)) && spikes == true)
        {
            fuckthisshit(2);
            spikes = false;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Application.Quit();
            }
        }

        if ((!(curve.GetComponent<Transform>().position.x < -2) || !(curve.GetComponent<Transform>().position.y > 0)) && hikes == true)
        {
            fuckthisshit(4);
            hikes = false;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                achievements();
            }
        }

        if ((!(curve.GetComponent<Transform>().position.x > 2) || !(curve.GetComponent<Transform>().position.y < 0)) && yikes == true)
        {
            fuckthisshit(1);
            yikes = false;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                options();
            }
        }

        if (curve.GetComponent<Transform>().position.x > 2 && curve.GetComponent<Transform>().position.y > 0 && dikes == false)
        {
            iwanttokaboom();
            dikes = true;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                options();
            }
        }

        if ((!(curve.GetComponent<Transform>().position.x > 2) || !(curve.GetComponent<Transform>().position.y > 0)) && dikes == true)
        {
            fuckthisshit(3);
            dikes = false;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                options();
            }
        }

        if (area2.GetComponent<SpriteRenderer>().color.Equals(f2))
        {
           
            area2.GetComponent<SpriteRenderer>().color = f2;
            if(Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Application.Quit();
            }  
        }

        if (area.GetComponent<SpriteRenderer>().color.Equals(f))
        {
            area.GetComponent<SpriteRenderer>().color = f;
            SceneManager.LoadScene("levelSelect");
        }

        if (area3.GetComponent<SpriteRenderer>().color.Equals(f3))
        {
            area3.GetComponent<SpriteRenderer>().color = f3;
            options();
        }

        if (area4.GetComponent<SpriteRenderer>().color.Equals(f4))
        {
            area4.GetComponent<SpriteRenderer>().color = f4;
            achievements();
        }

        if ((Input.GetKeyDown(KeyCode.BackQuote) && Application.platform == RuntimePlatform.WebGLPlayer) || (Input.GetKeyDown(KeyCode.Escape) && Application.platform != RuntimePlatform.WebGLPlayer))
        {
            mainmenu();
        }


    }

    void fuckthisshit(int x)
    {
       
        colorFader c = Instantiate(greg);
        c.setMode("other");
        c.setFade(1);
        c.setColor(Color.white);
        if(x == 1)
        {
            c.setOther(area);
        } else if(x == 2)
        {
            c.setOther(area2);
        } else if(x == 3)
        { 
            c.setOther(area3);
        }
        else
        {
            c.setOther(area4);
        }
            
        
    }

    void iwanttoexplode()
    {
        
        colorFader c = Instantiate(greg, Vector3.zero, Quaternion.Euler(0, 0, 0));
        c.setMode("other");
        c.setFade(4);
        c.setColor(f);
        c.setOther(area);
    }

    void iwanttocombust()
    {
        
        colorFader c = Instantiate(greg, Vector3.zero, Quaternion.Euler(0, 0, 0));
        c.setMode("other");
        c.setFade(4);
        c.setColor(f2);
        c.setOther(area2);
     }

    void iwanttokaboom()
    {
        
        colorFader c = Instantiate(greg, Vector3.zero, Quaternion.Euler(0, 0, 0));
        c.setMode("other");
        c.setFade(4);
        c.setColor(f3);
        c.setOther(area3);
    }

    void iwanttoblowup()
    {
        colorFader c = Instantiate(greg, Vector3.zero, Quaternion.Euler(0, 0, 0));
        c.setMode("other");
        c.setFade(4);
        c.setColor(f4);
        c.setOther(area4);
    }

    public void achievementGlowEnter()
    {
        colorFader c = Instantiate(greg, Vector3.zero, Quaternion.Euler(0, 0, 0));
        c.setMode("other");
        c.setFade(4);
        c.setColor(f4);
        c.setOther(area4);
    }

    public void achievementGlowExit()
    {
        fuckthisshit(4);
    }

    public void optionGlowEnter()
    {
        colorFader c = Instantiate(greg, Vector3.zero, Quaternion.Euler(0, 0, 0));
        c.setMode("other");
        c.setFade(4);
        c.setColor(f3);
        c.setOther(area3);
    }

    public void optionGlowExit()
    {
        fuckthisshit(3);
    }

    public void exitGlowEnter()
    {
        colorFader c = Instantiate(greg, Vector3.zero, Quaternion.Euler(0, 0, 0));
        c.setMode("other");
        c.setFade(4);
        c.setColor(f2);
        c.setOther(area2);
    }

    public void exitGlowExit()
    {
        fuckthisshit(2);
    }

    public void levelGlowEnter()
    {
        colorFader c = Instantiate(greg, Vector3.zero, Quaternion.Euler(0, 0, 0));
        c.setMode("other");
        c.setFade(4);
        c.setColor(f);
        c.setOther(area);
    }

    public void levelGlowExit()
    {
        fuckthisshit(1);
    }

    public void options()
    {
        curve.reset();
        area.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        main.SetActive(false);
        controls.SetActive(false);
        option.SetActive(true);
        optionGlowExit();
    }

    public void levelSelect()
    {
        SceneManager.LoadScene("levelSelect");
    }

    public void mainmenu()
    {       
        option.SetActive(false);
        achievement.SetActive(false);
        statistics.SetActive(false);
        controls.SetActive(false);
        main.SetActive(true);
        curve.check();
        yikes = false;
        spikes = false;
        dikes = false;
        hikes = false;
        Debug.Log("ok");
        optionGlowExit();
        levelGlowExit();
        exitGlowExit();
    }

    public void achievements()
    {
        curve.reset();
        optionclass.achievementCheck();
        area4.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        main.SetActive(false);
        achievement.SetActive(true);
        statistics.SetActive(false);
        achievementGlowExit();
    }

    public void stats()
    {
        achievement.SetActive(false);
        statistics.SetActive(true);
    }

    public void control()
    {
        option.SetActive(false);
        controls.SetActive(true);
    }

    public void exitGame()
    {
        if(Application.platform != RuntimePlatform.WebGLPlayer)
        {
            Application.Quit();
        }   
    }
}
