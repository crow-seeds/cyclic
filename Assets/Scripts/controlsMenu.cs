using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlsMenu : MonoBehaviour
{
    int mode = 0; //0 is none selected, 1 is left, 2 is right, 3 is swap
    public inputManager meme;
    Event keyEvent;
    public Text left;
    public Text right;
    public Text swap;

    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(meme.left.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            refresh();
            started = true;
        }
        
        
    }

    void OnGUI()
    {
        if (mode != 0)
        {
            keyEvent = Event.current;
            if (keyEvent != null && (keyEvent.isMouse || keyEvent.isKey))
            {
                if (keyEvent.isMouse)
                {
                    meme.addKey(mode,((KeyCode)System.Enum.Parse(typeof(KeyCode), "mouse" + Event.current.button, true)));
                    Debug.Log("roof");
                }
                else
                {
                    meme.addKey(mode, keyEvent.keyCode);
                }
                refresh();
                mode = 0;
            }
        }
        
    }

    public void refresh()
    {
        left.text = "Left Controls: ";
        right.text = "Right Controls: ";
        swap.text = "Swap Controls: ";
        for (int i = 0; i < meme.left.Count - 1; i++)
        {
            left.text += "[" + meme.left[i].ToString() + "]" + ", ";
        }
        for (int i = 0; i < meme.right.Count - 1; i++)
        {
            right.text += "[" + meme.right[i].ToString() + "]" + ", ";
        }
        for (int i = 0; i < meme.swap.Count - 1; i++)
        {
            swap.text += "[" + meme.swap[i].ToString() + "]" + ", ";
        }

        left.text += "[" + meme.left[meme.left.Count - 1].ToString() + "]";
        right.text += "[" + meme.right[meme.right.Count - 1].ToString() + "]";
        swap.text += "[" + meme.swap[meme.swap.Count - 1].ToString() + "]";
    }

    public void changeMode(int i)
    {
        mode = i;
        Debug.Log(mode);
    }

    public void removeKey(int i)
    {
        meme.removeKey(i);
        refresh();
    }
}
