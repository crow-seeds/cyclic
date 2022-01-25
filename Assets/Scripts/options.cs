using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class options : MonoBehaviour {

    public Slider volume;
    public Toggle invert;
    public Toggle vsync;
    public Toggle lowdetail;
    public Toggle seppuku;
    public Toggle loweffect;
    public Toggle fullscreen;
    public Toggle practiceIcons;
    public Toggle positionIndicator;
    public Toggle newgroundsIntegration;
    public GameObject selection;
    public GameObject colorselection;

    public List<GameObject> locks;
    public List<GameObject> skins;
    public List<GameObject> colors;
    public GameObject defaultskin;
    public GameObject defaultcolor;

    public List<GameObject> optionPages;
    int currentPage = 0;

    public GameObject invert1;
    public GameObject invert2;
    public GameObject invert3;
    public GameObject invert4;

    public NGHelper ng;

    int[] achievementIDs = new int[] { 66936, 66936, 66937, 66938, 66939, 66940, 66941, 66942, 66943, 66944, 66945, 66946, 66947, 66948, 66949, 66950, 66951, 66952, 66953, 66954, 66955, 66956, 66957, 66958, 66959, 66960, 66961, 66962, 66963, 66964, 66965, 66966, 66967, 67017, 67018, 67019, 67020, 66968, 66969, 66970, 66971, 67012, 67013, 67014, 67015, 67021, 67022, 67023, 67024, 67025, 67016 };

    // Use this for initialization
    void Start()
    {
        if (Screen.fullScreen)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }

        if (PlayerPrefs.GetInt("invert", 1) == -1)
        {
            invert.GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("vsync", 0) == 1)
        {
            vsync.GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("lowdetail", 0) == 1)
        {
            lowdetail.GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("loweffect", 0) == 1)
        {
            loweffect.GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("seppukuMode", 0) == 1)
        {
            seppuku.GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("fullscreen", 0) == 1)
        {
            fullscreen.GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("removePracticeIcons", 0) == 1)
        {
            practiceIcons.GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("locationIndicator", 1) == 1)
        {
            positionIndicator.GetComponent<Toggle>().isOn = true;
        }

        if (PlayerPrefs.GetInt("newgroundsIntegration", 1) == 1)
        {
            newgroundsIntegration.GetComponent<Toggle>().isOn = true;
        }

        fullscreen.onValueChanged.AddListener(delegate {
            toggleFullscreen(fullscreen);
        });

        volume.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume", .5f);      

        if (PlayerPrefs.GetInt("nightcore", 0) == 1)
        {
            invert1.SetActive(true);
            invert2.SetActive(true);
            invert3.SetActive(true);
            invert4.SetActive(true);

            foreach(GameObject c in colors)
            {
                Color co = c.GetComponent<RawImage>().color;
                c.GetComponent<RawImage>().color = new Color(1f - co.r, 1f - co.g, 1f - co.b);
            }
        }
        else
        {
            invert1.SetActive(false);
            invert2.SetActive(false);
            invert3.SetActive(false);
            invert4.SetActive(false);
        }

        achievementCheck();

    }
	// Update is called once per frame
	void Update () {
        if (invert.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("invert", -1);
        }
        else
        {
            PlayerPrefs.SetInt("invert", 0);
        }

        if (vsync.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("vsync", 1);
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            PlayerPrefs.SetInt("vsync", 0);
            QualitySettings.vSyncCount = 0;
        }

        if (lowdetail.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("lowdetail", 1);
        }
        else
        {
            PlayerPrefs.SetInt("lowdetail", 0);
        }

        if (loweffect.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("loweffect", 1);
        }
        else
        {
            PlayerPrefs.SetInt("loweffect", 0);
        }

        if (seppuku.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("seppukuMode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("seppukuMode", 0);
        }

        if (practiceIcons.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("removePracticeIcons", 1);
        }
        else
        {
            PlayerPrefs.SetInt("removePracticeIcons", 0);
        }

        if (positionIndicator.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("locationIndicator", 1);
        }
        else
        {
            PlayerPrefs.SetInt("locationIndicator", 0);
        }

        if (newgroundsIntegration.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("newgroundsIntegration", 1);
        }
        else
        {
            PlayerPrefs.SetInt("newgroundsIntegration", 0);
        }

        PlayerPrefs.SetFloat("volume", volume.GetComponent<Slider>().value);
	}

    public void deleteSave()
    {
        invert.isOn = false;
        vsync.isOn = false;
        lowdetail.isOn = false;
        PlayerPrefs.DeleteAll();
    }

    public void changeSkin(int skin)
    {
        PlayerPrefs.SetInt("skin", skin);
        if(skin == 1)
        {
            selection.GetComponent<RectTransform>().localPosition = new Vector3(100, defaultskin.transform.parent.GetComponent<RectTransform>().localPosition.y);
        }
        else
        {
            selection.GetComponent<RectTransform>().localPosition = new Vector3(100, skins[skin - 2].transform.parent.GetComponent<RectTransform>().localPosition.y);
        }
    }

    public void changeColor(int color)
    {
        PlayerPrefs.SetInt("color", color);
        if(color == 0)
        {
            colorselection.GetComponent<RectTransform>().localPosition = new Vector3(100, defaultcolor.transform.parent.GetComponent<RectTransform>().localPosition.y);
            Color c = new Color(.59f, .59f, .59f);
            if (PlayerPrefs.GetInt("nightcore", 0) == 1)
            {
                c = new Color(.41f, .41f, .41f);
            }
            foreach (GameObject s in skins)
            {
                s.GetComponent<RawImage>().color = c;
            }
        }
        else
        {
            colorselection.GetComponent<RectTransform>().localPosition = new Vector3(100, colors[color-1].transform.parent.GetComponent<RectTransform>().localPosition.y);
            Color c = colors[color - 1].GetComponent<RawImage>().color;
            
            foreach (GameObject s in skins)
            {
                s.GetComponent<RawImage>().color =  c;
            }
        }

        
    } 

    public void achievementCheck()
    {

        for (int i = 0; i < locks.Count; i++)
        {
            if (PlayerPrefs.GetInt("achievement" + (i + 1).ToString(), 0) == 1)
            {
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    ng.unlockMedal(achievementIDs[i + 1]);
                }

                locks[i].SetActive(false);
                if(i+1 >= 33 && i+1 <= 36)
                {
                    colors[i - 32].SetActive(true);
                }else if(i + 1 >= 45 && i + 1 <= 49)
                {
                    Debug.Log(i - 40);
                    colors[i - 40].SetActive(true);
                }else if(i + 1 > 49)
                {
                    Debug.Log("pee");
                    skins[i - 10].SetActive(true);
                }
                else if(i+1 > 36)
                {
                    skins[i-4].SetActive(true);
                }
                else
                {
                    skins[i].SetActive(true);
                }
            }

            if (PlayerPrefs.GetInt("achievement" + (i + 1).ToString(), 0) != 1)
            {
                locks[i].SetActive(true);
                if (i + 1 >= 33 && i + 1 <= 36)
                {
                    colors[i - 32].SetActive(false);
                }
                else if (i + 1 >= 45 && i + 1 <= 49)
                {
                    colors[i - 40].SetActive(false);
                }
                else if (i + 1 > 49)
                {
                    skins[i - 9].SetActive(false);
                }
                else if (i + 1 > 36)
                {
                    skins[i-4].SetActive(false);
                }
                else
                {
                    skins[i].SetActive(false);
                }
            }
        }
        int skin = PlayerPrefs.GetInt("skin", 1);
        if (skin == 1)
        {
            selection.GetComponent<RectTransform>().localPosition = new Vector3(100, defaultskin.transform.parent.GetComponent<RectTransform>().localPosition.y);
        }
        else
        {
            selection.GetComponent<RectTransform>().localPosition = new Vector3(100, skins[skin - 2].transform.parent.GetComponent<RectTransform>().localPosition.y);
        }

        int color = PlayerPrefs.GetInt("color", 0);
        if (color == 0)
        {
            colorselection.GetComponent<RectTransform>().localPosition = new Vector3(100, defaultcolor.transform.parent.GetComponent<RectTransform>().localPosition.y);
            Color c = new Color(.59f, .59f, .59f);
            if(PlayerPrefs.GetInt("nightcore", 0) == 1)
            {
                c = new Color(.41f, .41f, .41f);
            }
            defaultskin.GetComponent<RawImage>().color = c;
            foreach (GameObject s in skins)
            {
                s.GetComponent<RawImage>().color = c;
            }
        }
        else
        {
            colorselection.GetComponent<RectTransform>().localPosition = new Vector3(100, colors[color-1].transform.parent.GetComponent<RectTransform>().localPosition.y);
            Color c = colors[color - 1].GetComponent<RawImage>().color;
            
            defaultskin.GetComponent<RawImage>().color = c;
            foreach (GameObject s in skins)
            {
                s.GetComponent<RawImage>().color = c;
            }
        }
        
    }

    public static int calculatePercent()
    {
        int total = 0;
        for(int world = 1; world < 6; world++)
        {
            for(int level = 1; level < 6; level++)
            {
                total += PlayerPrefs.GetInt(world.ToString() + "," + level.ToString(), 0);
            }
        }
        return total;
    }

    public void nextPage()
    {
        optionPages[currentPage].SetActive(false);
        optionPages[currentPage + 1].SetActive(true);
        currentPage++;
    }

    public void previousPage()
    {
        optionPages[currentPage].SetActive(false);
        optionPages[currentPage - 1].SetActive(true);
        currentPage--;
    }
    [DllImport("__Internal")]
    private static extern bool GoFullscreen();

    public void activateFullscreen()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
             GoFullscreen();
        #else
            Screen.fullScreen = !Screen.fullScreen;
        #endif
    }

    void toggleFullscreen(Toggle t)
    {
        if (t.GetComponent<Toggle>().isOn)
        {
            Debug.Log("on");
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            Debug.Log("off");
            PlayerPrefs.SetInt("fullscreen", 0);
        }
        activateFullscreen();
    }
}
