using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nightcoretext : MonoBehaviour
{
    public Text me;
    public bool nightcore = false;

    public int world;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("nightcore", 0) == 1)
        {
            me.text = "Nightcore";
            nightcore = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            if (nightcore == false)
            {
                nightcore = true;
                me.text = "Nightcore";
            }
            else
            {
                nightcore = false;
                me.text = "";
            }
        }
    }

    public void toggleNightcore()
    {
        if (nightcore == false)
        {
            nightcore = true;
            me.text = "Nightcore";
        }
        else
        {
            nightcore = false;
            me.text = "";
        }
    }
}
