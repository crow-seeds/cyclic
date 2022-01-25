using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ring : MonoBehaviour {

    public double missed = 0;
    public double collect = 0;
    public NewBehaviourScript thing;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public double getCollect()
    {
        return missed;
    }

    public void spike()
    {
        collect++;
    }

    public void ball()
    {
        missed++;
        thing.loseHexagon();
        if (PlayerPrefs.GetInt("seppukuMode", 0) == 1)
        {
            PlayerPrefs.SetFloat("distanceTraveled", PlayerPrefs.GetFloat("distanceTraveled", 0) + thing.distance());
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
