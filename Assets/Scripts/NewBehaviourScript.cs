using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class NewBehaviourScript : MonoBehaviour
{

    float koala = 2.5F;
    public int collect = 1;
    public int missed = 0;
    int controlsetting = 1;
    float distanceTraveled = 0;

    bool wentLeft = false;
    bool wentRight = false;
    bool isPaused = false;
    public GameObject pauseScreen;
    public GameObject practiceScreen;
    public AudioSource thing;
    public inputManager manager;

    public Text practice;
    public baselevel level;

    bool canSwap = true;

    bool hasWon = false;

    public Text practiceOn;
    public Text practiceOff;
    public GameObject invert;

    public List<Color> colors = new List<Color>();
    float startTime;
    bool mobileSwap = true;

    public int practiceMode = 0; //1 for is practice, 0 for not
    bool canMove = true;
    bool mobile = false;

    public GameObject mobileSwapIcon;
    public GameObject practiceRestart;
    public GameObject practice5;
    public GameObject practiceSave;

    public bool nightcoreDebug = false;

    public bool hasHexagon = false;
    public GameObject hexagon;
    bool lowDetailMode = false;
    bool hasIndicator = false;

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
             return IsMobile();
        #endif
        return false;
    }


    // Use this for initialization
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Skins/skin" + PlayerPrefs.GetInt("skin", 1).ToString());
        this.GetComponent<SpriteRenderer>().color = colors[PlayerPrefs.GetInt("color", 0)];
        Color color = this.GetComponent<SpriteRenderer>().color;

        if (PlayerPrefs.GetInt("lowdetail", 0) == 1)
        {
            lowDetailMode = true;
        }

        

        canMove = SceneManager.GetActiveScene().name != "menu";

        practiceMode = PlayerPrefs.GetInt("practice", 0);
        if (PlayerPrefs.GetInt("invert") == -1)
        {
            controlsetting = -1;
        }
        if(practiceMode == 1)
        {
            practice.gameObject.SetActive(true);
            practice.text = "Checkpoint:\n" + System.Math.Round(PlayerPrefs.GetFloat("checkpointtime",0), 2) + "s";

            if(PlayerPrefs.GetInt("removePracticeIcons", 0) == 0)
            {
                practiceRestart.gameObject.SetActive(true);
                practice5.gameObject.SetActive(true);
                practiceSave.gameObject.SetActive(true);
            }
            
        }
        else
        {
            practice.gameObject.SetActive(false);
        }

        if(PlayerPrefs.GetInt("nightcore", 0) == 1 || nightcoreDebug)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1 - color.r, 1 - color.g, 1 - color.b);
            invert.SetActive(true);
            Time.timeScale = 1.3f;
            thing.pitch = 1.3f;
        }
        else
        {
            invert.SetActive(false);
            Time.timeScale = 1f;
            thing.pitch = 1f;
        }

        if (PlayerPrefs.GetInt("locationIndicator", 1) == 1)
        {
            hexagon.SetActive(true);
            hasIndicator = true;
            hexagon.GetComponent<SpriteRenderer>().color = this.gameObject.GetComponent<SpriteRenderer>().color;
        }

        mobile = isMobile();
        if(mobile == true)
        {
            PlayerPrefs.SetInt("isMobile", 1);
            mobileSwapIcon.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("isMobile", 0);
        }
        

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cat = new Vector3(0, 0, 0);

        if (mobile && canMove)
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.touches[0];

                if(touch.position.x > Screen.width * 0.78125f && touch.position.y < Screen.height * 0.388f)
                {
                    if(canSwap && mobileSwap)
                    {
                        transform.RotateAround(cat, Vector3.back, 180);
                        mobileSwap = false;
                    }
                }else if (!mobileSwap)
                {

                }else if(touch.position.x < Screen.width / 2)
                {
                    transform.RotateAround(cat, Vector3.back, 1 * koala * controlsetting * (Time.deltaTime * 60f));
                    distanceTraveled += 1 * koala * controlsetting * (Time.deltaTime * 60f);
                    wentLeft = true;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    transform.RotateAround(cat, Vector3.back, -1 * koala * controlsetting * (Time.deltaTime * 60f));
                    distanceTraveled += 1 * koala * controlsetting * (Time.deltaTime * 60f);
                    wentRight = true;
                }
            }
            else if (Input.touchCount < 1 && !mobileSwap)
            {
                mobileSwap = true;
            }
        }
        else if(canMove)
        {
            for (int i = 0; i < manager.left.Count; i++)
            {
                if (Input.GetKey(manager.left[i]))
                {
                    transform.RotateAround(cat, Vector3.back, 1 * koala * controlsetting * (Time.deltaTime * 60f));
                    distanceTraveled += 1 * koala * controlsetting * (Time.deltaTime * 60f);
                    wentLeft = true;
                    break;
                }
            }

            for (int i = 0; i < manager.right.Count; i++)
            {
                if (Input.GetKey(manager.right[i]))
                {
                    transform.RotateAround(cat, Vector3.back, -1 * koala * controlsetting * (Time.deltaTime * 60f));
                    distanceTraveled += 1 * koala * controlsetting * (Time.deltaTime * 60f);
                    wentRight = true;
                    break;
                }
            }

            for (int i = 0; i < manager.swap.Count; i++)
            {
                if (Input.GetKeyDown(manager.swap[i]) && canSwap)
                {
                    transform.RotateAround(cat, Vector3.back, 180);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "menu" && (isPaused || hasWon))
        {
            goBackToMenu();
        }

       
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "menu" && !isPaused && !hasWon)
        {
            pause();
        }

        if(Input.GetKeyDown(KeyCode.Return) && SceneManager.GetActiveScene().name != "menu" && isPaused)
        {
            exitPause();
        }

        if (Input.GetKeyDown(KeyCode.E) && practiceMode == 1)
        {
            float timeElapsed = Time.time - startTime;
            practice.text = "Checkpoint:\n" + System.Math.Round(timeElapsed + level.startpos, 2) + "s";
            PlayerPrefs.SetFloat("checkpointtime", timeElapsed + level.startpos);
            PlayerPrefs.SetFloat("checkpointloc", this.transform.eulerAngles.z);
        }

        if (Input.GetKeyDown(KeyCode.W) && practiceMode == 1)
        {
            if(PlayerPrefs.GetFloat("checkpointtime", 0) < 5)
            {
                practice.text = "Checkpoint:\n" + "0.00s";
                PlayerPrefs.SetFloat("checkpointtime", 0);
            }
            else
            {
                practice.text = "Checkpoint:\n" + System.Math.Round(PlayerPrefs.GetFloat("checkpointtime", 0) - 5, 2) + "s";
                PlayerPrefs.SetFloat("checkpointtime", PlayerPrefs.GetFloat("checkpointtime", 0) - 5);
                PlayerPrefs.SetFloat("checkpointloc", this.transform.eulerAngles.z);
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetFloat("distanceTraveled", PlayerPrefs.GetFloat("distanceTraveled", 0) + distanceTraveled);
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (lowDetailMode == false)
        {
            hexagon.transform.Rotate(Vector3.forward, Time.deltaTime * 50);
        }


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            collect++;
        }

        if(col.gameObject.tag == "hexagon")
        {
            hasHexagon = true;
            hexagon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Hexagons/fillhexagon");
            hexagon.SetActive(true);
            hexagon.GetComponent<SpriteRenderer>().color = this.gameObject.GetComponent<SpriteRenderer>().color;

            if(PlayerPrefs.GetInt("nightcore", 0) == 1)
            {
                hexagon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Heptagons/fillheptagon");
            }
        }

        if(col.gameObject.tag == "spike")
        {
            missed++;
            if(PlayerPrefs.GetInt("seppukuMode", 0) == 1)
            {
                PlayerPrefs.SetFloat("distanceTraveled", PlayerPrefs.GetFloat("distanceTraveled", 0) + distanceTraveled);
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }

            if (hasHexagon)
            {
                loseHexagon();
            }
        }
    }

    public double getCollect()
    {
        return collect;
    }

    public void reset()
    {
        this.GetComponent<Transform>().localPosition = new Vector3(0, -4.61f, 0);
        this.transform.Rotate(0, 0, -1*this.GetComponent<Transform>().localEulerAngles.z);
    }

    public void check()
    {
        if (PlayerPrefs.GetInt("invert") == -1)
        {
            controlsetting = -1;
        }
        else
        {
            controlsetting = 1;
        }

        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Skins/skin" + PlayerPrefs.GetInt("skin", 1).ToString());

        Color color = colors[PlayerPrefs.GetInt("color", 0)];

        if (PlayerPrefs.GetInt("nightcore", 0) == 1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1 - color.r, 1 - color.g, 1 - color.b);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = color;
        }
            
    }


    public float distance()
    {
        return distanceTraveled;
    }

    public bool oneDirection()
    {
        return !(wentLeft && wentRight);
    }

    public void goBackToMenu()
    {
        PlayerPrefs.SetFloat("distanceTraveled", PlayerPrefs.GetFloat("distanceTraveled", 0) + distanceTraveled);
        PlayerPrefs.SetInt("practice", 0);
        PlayerPrefs.SetFloat("checkpointtime", 0);
        PlayerPrefs.SetFloat("checkpointloc", 0);
        practiceMode = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("levelSelect");
    }

    public void exitPause()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        thing.Play();
        if(PlayerPrefs.GetInt("nightcore", 0) == 1)
        {
            Time.timeScale = 1.3f;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }

    public void pause()
    {
        if (practiceMode == 0)
        {
            practiceOff.gameObject.SetActive(false);
            practiceOn.gameObject.SetActive(true);
        }else
        {
            practiceOff.gameObject.SetActive(true);
            practiceOn.gameObject.SetActive(false);
            Debug.Log("log");
        }
        pauseScreen.SetActive(true);
        isPaused = true;
        thing.Pause();
        Time.timeScale = 0;
    }

    public void practiceToggle()
    {
        if(practiceMode == 0)
        {
            pauseScreen.SetActive(false);
            practiceScreen.SetActive(true);
        }
        else
        {
            practiceMode = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene("liftoff");
            PlayerPrefs.SetInt("practice", practiceMode);
            PlayerPrefs.SetFloat("checkpointtimeLast", 0);
            PlayerPrefs.SetFloat("checkpointtime", 0);
            PlayerPrefs.SetFloat("checkpointlocLast", 0);
            PlayerPrefs.SetFloat("checkpointloc", 0);
        }
    }

    public void backToPause()
    {
        practiceScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void PracticeOn()
    {
        practiceMode = 1;
        Time.timeScale = 1;
        SceneManager.LoadScene("liftoff");
        PlayerPrefs.SetInt("practice", practiceMode);
    }

    public void won()
    {
        hasWon = true;
    }

    public void changeSwap(bool t)
    {
        canSwap = t;
    }

    public void placeCheckpoint()
    {
        if (practiceMode == 1)
        {
            float timeElapsed = Time.time - startTime;
            practice.text = "Checkpoint:\n" + System.Math.Round(timeElapsed + level.startpos, 2) + "s";
            PlayerPrefs.SetFloat("checkpointtime", timeElapsed + level.startpos);
            PlayerPrefs.SetFloat("checkpointloc", this.transform.eulerAngles.z);
        }
    }

    public void placeCheckPoint5SecAgo()
    {
        if (practiceMode == 1)
        {
            if (PlayerPrefs.GetFloat("checkpointtime", 0) < 5)
            {
                practice.text = "Checkpoint:\n" + "0.00s";
                PlayerPrefs.SetFloat("checkpointtime", 0);
            }
            else
            {
                practice.text = "Checkpoint:\n" + System.Math.Round(PlayerPrefs.GetFloat("checkpointtime", 0) - 5, 2) + "s";
                PlayerPrefs.SetFloat("checkpointtime", PlayerPrefs.GetFloat("checkpointtime", 0) - 5);
                PlayerPrefs.SetFloat("checkpointloc", this.transform.eulerAngles.z);
            }
        }
    }

    public void restartLevel()
    {
        PlayerPrefs.SetFloat("distanceTraveled", PlayerPrefs.GetFloat("distanceTraveled", 0) + distanceTraveled);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void loseHexagon()
    {
        if (hasHexagon)
        {
            if (!hasIndicator)
            {
                hexagon.SetActive(false);
            }
            else
            {
                Debug.Log("why!!!");
                hexagon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Projectiles/Triangles/filltriangle");
            }
            
            hasHexagon = false;
        }
    }
}