using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSelect : MonoBehaviour {

    public GameObject egg;
    Vector3 ploopy = Vector3.zero;
    public int column = 0;
    public int row = 0;

    public int maxRows;
    public int maxColumns;
    bool moving = false;

    public audio playerthing;
    public AudioSource music;

    public GameObject invert;

    public string[,] levelList = new string[,]
    {
        {"droplets", "drag", "nostalgia", "birds", ""},
        {"plus", "hideandseek" , "liftoff", "marble", "tautstrings"},
        {"discovery", "gravitation", "milkshake", "computerblues", ""},
        {"gems", "sanctuary", "purgatori", "", "" },
        {"aumetrathewitch", "", "", "", "" }
    };

    public float[,] startTimes = new float[,] {
        {0, 3, 22.9f, 21.27f, 0},
        {29.95f, 63, 43, 37.6f, 89},
        {61, 61.5f, 33.6f, 17.2f, 0},
        {73.6f,28f,81.6f,0,0 },
        {90f,0,0,0,0 }
    };

    public float[,] endTimes = new float[,] {
        {23, 34, 47.5f, 42, 0},
        {42.25f, 83, 56.5f, 52.4f, 107},
        {75, 82, 48.4f, 32.5f, 0},
        {85.4f,45.5f,94.4f,0,0 },
        {105f,0,0,0,0 }
    };


    // Use this for initialization
    void Start () {
        ploopy = egg.GetComponent<RectTransform>().anchoredPosition;
        column = PlayerPrefs.GetInt("lastColumn", 1);
        row = PlayerPrefs.GetInt("lastRow", 2);
        egg.GetComponent<RectTransform>().anchoredPosition = new Vector3(column * -1600, row * 900);
        playerthing.setaudio(levelList[row, column]);
        playerthing.resetaudio(startTimes[row, column], endTimes[row, column]);
        ploopy = egg.GetComponent<RectTransform>().anchoredPosition;

        if (PlayerPrefs.GetInt("nightcore", 0) == 1)
        {
            invert.SetActive(true);
            Time.timeScale = 1.3f;
            music.pitch = 1.3f;
        }
        else
        {
            invert.SetActive(false);
            Time.timeScale = 1f;
            music.pitch = 1f;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.DownArrow) && row != maxRows - 1 && levelList[row+1, column] != "" && !moving)
        {
            moving = true;
            InvokeRepeating("moveDown", 0, 0.01f);   
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && row != 0  && levelList[row-1, column] != "" && !moving)
        {
            moving = true;
            InvokeRepeating("moveUp", 0, 0.01f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)  && column != maxColumns-1 && levelList[row, column + 1] != "" && !moving)
        {
            moving = true;
            InvokeRepeating("moveRight", 0, 0.01f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && column != 0 && !moving)
        {
            moving = true;
            InvokeRepeating("moveLeft", 0, 0.01f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if(PlayerPrefs.GetInt("nightcore", 0) == 0)
            {
                PlayerPrefs.SetInt("nightcore", 1);
                invert.SetActive(true);
                Time.timeScale = 1.3f;
                music.pitch = 1.3f;
            }
            else
            {
                PlayerPrefs.SetInt("nightcore", 0);
                invert.SetActive(false);
                Time.timeScale = 1f;
                music.pitch = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(PlayerPrefs.GetInt("nightcore", 0) == 1 && PlayerPrefs.GetInt((row+1) + "," + (column+1)) < 90)
            {

            }
            else
            {
                string s = levelList[row, column];
                loadLevel(s);
            }
        }

        if ((Input.GetKeyDown(KeyCode.BackQuote) && Application.platform == RuntimePlatform.WebGLPlayer) || (Input.GetKeyDown(KeyCode.Escape) && Application.platform != RuntimePlatform.WebGLPlayer))
        {
            SceneManager.LoadScene("menu");
        }
    }

    public void moveDown()
    {
        egg.GetComponent<RectTransform>().anchoredPosition = new Vector3(egg.GetComponent<RectTransform>().anchoredPosition.x, egg.GetComponent<RectTransform>().anchoredPosition.y + 30);
        if (egg.GetComponent<RectTransform>().anchoredPosition.y >= ploopy.y + 900)
        {
            CancelInvoke();
            ploopy = egg.GetComponent<RectTransform>().anchoredPosition;
            row++;
            playerthing.setaudio(levelList[row, column]);
            playerthing.resetaudio(startTimes[row, column], endTimes[row, column]);
            moving = false;
        }
    }

    public void moveUp()
    {
        egg.GetComponent<RectTransform>().anchoredPosition = new Vector3(egg.GetComponent<RectTransform>().anchoredPosition.x, egg.GetComponent<RectTransform>().anchoredPosition.y - 30);
        if (egg.GetComponent<RectTransform>().anchoredPosition.y <= ploopy.y - 900)
        {
            CancelInvoke();
            ploopy = egg.GetComponent<RectTransform>().anchoredPosition;
            row--;
            playerthing.setaudio(levelList[row, column]);
            playerthing.resetaudio(startTimes[row, column], endTimes[row, column]);
            moving = false;
        }
    }

    public void moveRight()
    {
        egg.GetComponent<RectTransform>().anchoredPosition = new Vector3(egg.GetComponent<RectTransform>().anchoredPosition.x-32, egg.GetComponent<RectTransform>().anchoredPosition.y);
        if (egg.GetComponent<RectTransform>().anchoredPosition.x <= ploopy.x-1600)
        {
            CancelInvoke();
            ploopy = egg.GetComponent<RectTransform>().anchoredPosition;
            column++;
            playerthing.setaudio(levelList[row, column]);
            playerthing.resetaudio(startTimes[row, column], endTimes[row, column]);
            moving = false;
        }
    }

    public void moveLeft()
    {
        egg.GetComponent<RectTransform>().anchoredPosition = new Vector3(egg.GetComponent<RectTransform>().anchoredPosition.x + 32, egg.GetComponent<RectTransform>().anchoredPosition.y);
        if (egg.GetComponent<RectTransform>().anchoredPosition.x >= ploopy.x + 1600)
        {
            CancelInvoke();
            ploopy = egg.GetComponent<RectTransform>().anchoredPosition;
            column--;
            playerthing.setaudio(levelList[row, column]);
            playerthing.resetaudio(startTimes[row, column], endTimes[row, column]);
            moving = false;
        }
    }

    public void getDown()
    {
        moving = true;
        InvokeRepeating("moveDown", 0, 0.01f);
    }

    public void getUp()
    {
        moving = true;
        InvokeRepeating("moveUp", 0, 0.01f);
    }

    public void getRight()
    {
        moving = true;
        InvokeRepeating("moveRight", 0, 0.01f);
    }

    public void getLeft()
    {
        moving = true;
        InvokeRepeating("moveLeft", 0, 0.01f);
    }

    public void loadLevel(string s)
    {
        PlayerPrefs.SetString("level", s);
        PlayerPrefs.SetInt("lastRow", row);
        PlayerPrefs.SetInt("lastColumn", column);
        SceneManager.LoadScene("liftoff");
    }

    public void goToMenu()
    {
        PlayerPrefs.SetInt("lastRow", row);
        PlayerPrefs.SetInt("lastColumn", column);
        SceneManager.LoadScene("menu");
    }

    public void nightcoreToggle()
    {
        Debug.Log("aaaaa");
        foreach(GameObject f in GameObject.FindGameObjectsWithTag("nightcore"))
        {
            if(f.GetComponent<nightcoretext>() != null)
            {
                f.GetComponent<nightcoretext>().toggleNightcore();
            }
            if (f.GetComponent<nightcorelocking>() != null)
            {
                f.GetComponent<nightcorelocking>().toggleNightcore();
            }
            if (f.GetComponent<status>() != null)
            {
                f.GetComponent<status>().toggleNightcore();
            }
            if (f.GetComponent<progress>() != null)
            {
                f.GetComponent<progress>().toggleNightcore();
            }
        }

        if (PlayerPrefs.GetInt("nightcore", 0) == 0)
        {
            PlayerPrefs.SetInt("nightcore", 1);
            invert.SetActive(true);
            Time.timeScale = 1.3f;
            music.pitch = 1.3f;
        }
        else
        {
            PlayerPrefs.SetInt("nightcore", 0);
            invert.SetActive(false);
            Time.timeScale = 1f;
            music.pitch = 1f;
        }
    }
}
