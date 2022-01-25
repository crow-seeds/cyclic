using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    public List<KeyCode> left;
    public List<KeyCode> right;
    public List<KeyCode> swap;

    public static inputManager thing;

    List<string> leftDefaults = new List<string>{ "LeftArrow", "Mouse0", "None", "None", "None" };
    List<string> rightDefaults = new List<string> { "RightArrow", "Mouse1", "None", "None", "None"};
    List<string> swapDefaults = new List<string> { "UpArrow", "Space", "Mouse2", "None", "None" };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            left.Add((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftControl" + i.ToString(), leftDefaults[i])));
            right.Add((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightControl" + i.ToString(), rightDefaults[i])));
            swap.Add((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("swapControl" + i.ToString(), swapDefaults[i])));
        }

        for (int i = 0; i < 4; i++)
        {
            left.Remove(KeyCode.None);
            right.Remove(KeyCode.None);
            swap.Remove(KeyCode.None);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addKey(int p, KeyCode k)
    {
        if(p == 1)
        {
            if(left.Count < 4)
            {
                left.Add(k);
                PlayerPrefs.SetString("leftControl" + (left.Count - 1).ToString(), k.ToString());
            }
            else
            {
                left.RemoveAt(3);
                left.Add(k);
                PlayerPrefs.SetString("leftControl" + (left.Count - 1).ToString(), k.ToString());
            }
        }else if(p == 2)
        {
            if (right.Count < 4)
            {
                right.Add(k);
            }
            else
            {
                right.RemoveAt(3);
                right.Add(k);
            }
            PlayerPrefs.SetString("rightControl" + (right.Count - 1).ToString(), k.ToString());
        }
        else
        {
            if (swap.Count < 4)
            {
                swap.Add(k);
            }
            else
            {
                swap.RemoveAt(3);
                swap.Add(k);
            }
            PlayerPrefs.SetString("swapControl" + (swap.Count - 1).ToString(), k.ToString());
        }
               
    }

    public void removeKey(int p)
    {
        if(p == 1)
        {
            left.RemoveAt(left.Count - 1);
            PlayerPrefs.SetString("leftControl" + (left.Count).ToString(), "None");
        }
        else if(p == 2)
        {
            right.RemoveAt(right.Count - 1);
            PlayerPrefs.SetString("rightControl" + (right.Count).ToString(), "None");
        }
        else
        {
            swap.RemoveAt(swap.Count - 1);
            PlayerPrefs.SetString("swapControl" + (swap.Count).ToString(), "None");
        }
    }
}
