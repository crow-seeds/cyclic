using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class group : MonoBehaviour {

    public List<int> id = new List<int>();

    public group(List<int> i)
    {
        id = i;
    }

    public List<int> getId()
    {
        return id;
    }

    public void setId(string[] i)
    {
        int[] nums = new int[i.Length];
        for (int t = 0; t < i.Length; t++)
        {
            nums[t] = int.Parse(i[t]);
        }
        id = new List<int>(nums);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
