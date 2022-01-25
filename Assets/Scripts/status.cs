using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class status : MonoBehaviour
{

    public Text me;
    public int world;
    public int level;
    public int mode; //0 for completion status, 1 for nightcore text
    public bool nightcoretog = false; //false for non nightcore

    // Use this for initialization
    void Start()
    {

        if (mode == 0)
        {
            string nightcore = "";

            if(PlayerPrefs.GetInt("nightcore", 0) == 1)
            {
                nightcore = "nightcore";
                nightcoretog = true;
            }

            if (PlayerPrefs.GetInt(nightcore + world + "," + level, 0) == 100)
            {
                me.text = "Perfect";
            }
            else if (PlayerPrefs.GetInt(nightcore + world + "," + level, 0) >= 90)
            {
                me.text = "Complete";
            }
            else
            {
                me.text = "";
            }

        }
        else if (mode == 1)
        {
            if (PlayerPrefs.GetInt(world + "," + level, 0) >= 90)
            {
                if (PlayerPrefs.GetInt("nightcore", 0) == 0)
                {
                    me.text = "[SHIFT] for Nightcore Mode";
                    if(PlayerPrefs.GetInt("isMobile", 0) == 1)
                    {
                        me.text = "Press here for Nightcore Mode";
                    }
                }
                else
                {
                    me.text = "[SHIFT] for Normal Mode";
                    if (PlayerPrefs.GetInt("isMobile", 0) == 1)
                    {
                        me.text = "Press here for Normal Mode";
                    }
                    nightcoretog = true;
                }

            }
            else
            {
                me.text = "";
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && me.text != "" && mode == 1)
        {
            if (me.text == "[SHIFT] for Nightcore Mode")
            {
                me.text = "[SHIFT] for Normal Mode";
                if (PlayerPrefs.GetInt("isMobile", 0) == 1)
                {
                    me.text = "Press here for Normal Mode";
                }
            }
            else
            {
                me.text = "[SHIFT] for Nightcore Mode";
                if (PlayerPrefs.GetInt("isMobile", 0) == 1)
                {
                    me.text = "Press here for Nightcore Mode";
                }
            }
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && mode == 0)
        {
            int x = 0;

            if (!nightcoretog)
            {
                x = PlayerPrefs.GetInt("nightcore" + world + "," + level, 0);
                nightcoretog = true;
            }
            else
            {
                x = PlayerPrefs.GetInt(world + "," + level, 0);
                nightcoretog = false;
            }

            if (x == 100)
            {
                me.text = "Perfect";
            }
            else if (x >= 90)
            {
                me.text = "Complete";
            }
            else
            {
                me.text = "";
            }
        }
    }

    public void toggleNightcore()
    {
        if (me.text != "" && mode == 1)
        {
            if (me.text == "[SHIFT] for Nightcore Mode")
            {
                me.text = "[SHIFT] for Normal Mode";
                if (PlayerPrefs.GetInt("isMobile", 0) == 1)
                {
                    me.text = "Press here for Normal Mode";
                }
            }
            else
            {
                me.text = "[SHIFT] for Nightcore Mode";
                if (PlayerPrefs.GetInt("isMobile", 0) == 1)
                {
                    me.text = "Press here for Nightcore Mode";
                }
            }
        }

        if (mode == 0)
        {
            int x = 0;

            if (!nightcoretog)
            {
                x = PlayerPrefs.GetInt("nightcore" + world + "," + level, 0);
                nightcoretog = true;
            }
            else
            {
                x = PlayerPrefs.GetInt(world + "," + level, 0);
                nightcoretog = false;
            }

            if (x == 100)
            {
                me.text = "Perfect";
            }
            else if (x >= 90)
            {
                me.text = "Complete";
            }
            else
            {
                me.text = "";
            }
        }
    }
}
