using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progress : MonoBehaviour {

    public string row;
    public string column;

    public bool isPercent = true;
    public Text me;

    public bool nightcoretog = false; //false is not nightcore, true is

	// Use this for initialization
	void Start () {
        string nightcore = "";
        if(PlayerPrefs.GetInt("nightcore", 0) == 1)
        {
            nightcore = "nightcore";
            nightcoretog = true;
        }
        me.text = PlayerPrefs.GetInt(nightcore + row+","+column, 0).ToString() + "%";
        if(PlayerPrefs.GetInt(nightcore + "hexagon" + row + "," + column, 0) == 1)
        {
            me.text += "+";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            if (!nightcoretog)
            {
                me.text = PlayerPrefs.GetInt("nightcore" + row + "," + column, 0).ToString() + "%";

                if (PlayerPrefs.GetInt("nightcore" + "hexagon" + row + "," + column, 0) == 1)
                {
                    me.text += "+";
                }

                nightcoretog = true;
            }
            else
            {
                me.text = PlayerPrefs.GetInt(row + "," + column, 0).ToString() + "%";

                if (PlayerPrefs.GetInt("hexagon" + row + "," + column, 0) == 1)
                {
                    me.text += "+";
                }

                nightcoretog = false;
            }

        }
    }

    void percent()
    {
        //me.text = (saveData.loadPercent()[row, column]).ToString()+"%";
    }

    void attempt()
    {
        //me.text = "Attempts: "+(saveData.loadAttempts()[row, column]).ToString();
    }

    public void toggleNightcore()
    {
        if (!nightcoretog)
        {
            me.text = PlayerPrefs.GetInt("nightcore" + row + "," + column, 0).ToString() + "%";

            if (PlayerPrefs.GetInt("nightcore" + "hexagon" + row + "," + column, 0) == 1)
            {
                me.text += "+";
            }

            nightcoretog = true;
        }
        else
        {
            me.text = PlayerPrefs.GetInt(row + "," + column, 0).ToString() + "%";

            if (PlayerPrefs.GetInt("hexagon" + row + "," + column, 0) == 1)
            {
                me.text += "+";
            }

            nightcoretog = false;
        }
    }
}
