using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightcorelocking : MonoBehaviour
{
    public GameObject container;
    public bool nightcoretog = false;

    public int world;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("nightcore") == 1)
        {
            nightcoretog = true;
            container.SetActive(true);
        }

        if(PlayerPrefs.GetInt(world + "," + level) >= 90)
        {
            container.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            if (nightcoretog == false)
            {
                nightcoretog = true;
                container.SetActive(true);

                if (PlayerPrefs.GetInt(world + "," + level) >= 90)
                {
                    container.SetActive(false);
                }
            }
            else
            {
                nightcoretog = false;
                container.SetActive(false);
            }
        }
    }

    public void toggleNightcore()
    {
        if (nightcoretog == false)
        {
            nightcoretog = true;
            container.SetActive(true);

            if (PlayerPrefs.GetInt(world + "," + level) >= 90)
            {
                container.SetActive(false);
            }
        }
        else
        {
            nightcoretog = false;
            container.SetActive(false);
        }
    }
}
