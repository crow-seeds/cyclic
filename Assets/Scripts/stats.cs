using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour {

    public string type;

	// Use this for initialization
	void Start () {
		if(type == "balls")
        {
            this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text + " " + PlayerPrefs.GetInt("ballCollection", 0).ToString();
        }

        if (type == "perfects")
        {
            this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text + " " + PlayerPrefs.GetInt("perfectCount", 0).ToString();
        }

        if (type == "percent")
        {
            this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text + " " + options.calculatePercent().ToString() + "%";
        }

        if (type == "distance")
        {
            this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text + " " + ((int)(PlayerPrefs.GetFloat("distanceTraveled", 0) * Mathf.Deg2Rad)).ToString() + " rad";
        }

        if (type == "spikes")
        {
            this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text + " " + PlayerPrefs.GetInt("spikeCollection", 0).ToString();
        }

        if (type == "nightcore")
        {
            this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text + " " + PlayerPrefs.GetInt("nightcoreCount", 0).ToString();
        }

        if (type == "hexagon")
        {
            this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text + " " + PlayerPrefs.GetInt("hexagonCount", 0).ToString();
        }

        if (type == "heptagon")
        {
            this.gameObject.GetComponent<Text>().text = this.gameObject.GetComponent<Text>().text + " " + PlayerPrefs.GetInt("nightcorehexagonCount", 0).ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
