using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text;
using System.Globalization;
using System.Xml;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class baselevel : shooterv2 {

    public TextAsset levelfile;
    public AudioSource audiothing;
    public float startpos = 0;
    public float startloc = 0;
    public Text levelname;
    string[] commas = { "," };

    bool lowEffect = false;
    bool nightcore = false;

    public bool debug;
    
	// Use this for initialization
	void Start () {

        percentage.text = "100%";
        StartCoroutine(startup());
        if(PlayerPrefs.GetInt("loweffect", 0) == 1)
        {
            lowEffect = true;
        }

        if (PlayerPrefs.GetInt("nightcore", 0) == 1)
        {
            nightcore = true;
        }
    }

    IEnumerator startup()
    {
        if (PlayerPrefs.GetInt("practice", 0) == 1 && !debug)
        {
            startpos = PlayerPrefs.GetFloat("checkpointtime", 0);
            startloc = PlayerPrefs.GetFloat("checkpointloc", 0);
        }



        playerObject.transform.RotateAround(Vector3.zero, Vector3.forward, startloc);
        levelfile = Resources.Load<TextAsset>("Levels/" + PlayerPrefs.GetString("level", "liftoff"));
        string data = levelfile.text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(data));

        XmlNodeList myNodeListInfo = xmlDoc.SelectNodes("//song/info");
        foreach (XmlNode infonode in myNodeListInfo)
        {
            setLevel(infonode.Attributes["world"].Value, infonode.Attributes["level"].Value);
            audiothing.clip = Resources.Load<AudioClip>("Music/" + infonode.Attributes["MP3Name"].Value);
            levelname.text = infonode.Attributes["name"].Value;
            audiothing.Play();
        }

        yield return new WaitWhile(() => !(audiothing.clip.loadState.Equals(AudioDataLoadState.Loaded)));

        if (startpos > 0)
        {
            audiothing.time = startpos;
        }

        XmlNodeList myNodeListObject = xmlDoc.SelectNodes("//song/object");
        foreach (XmlNode objectnode in myNodeListObject)
        {
            if (lowEffect && objectnode.Attributes["highDetail"] != null)
            {

            }
            else
            {
                switch (objectnode.Attributes["type"].Value)
                {
                    case "create":
                        float time = (float)Convert.ToDouble(objectnode.Attributes["time"].Value);
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos)
                        {
                            time = 0;
                        }
                        else
                        {
                            time = time - startpos;
                        }
                        bool blending = false;
                        if (objectnode.Attributes["blending"] != null && objectnode.Attributes["blending"].Value == "true")
                        {
                            blending = true;
                        }
                        if (objectnode.Attributes["group"] == null)
                        {
                            StartCoroutine(createObject((float)Convert.ToDouble(objectnode.Attributes["xpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["ypos"].Value), (float)Convert.ToDouble(objectnode.Attributes["zpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["length"].Value), (float)Convert.ToDouble(objectnode.Attributes["height"].Value), (float)Convert.ToDouble(objectnode.Attributes["rotation"].Value), (float)Convert.ToDouble(objectnode.Attributes["red"].Value), (float)Convert.ToDouble(objectnode.Attributes["green"].Value), (float)Convert.ToDouble(objectnode.Attributes["blue"].Value), (float)Convert.ToDouble(objectnode.Attributes["alpha"].Value), Convert.ToInt32(objectnode.Attributes["id"].Value), time, blending));
                        }
                        else
                        {
                            StartCoroutine(createObject((float)Convert.ToDouble(objectnode.Attributes["xpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["ypos"].Value), (float)Convert.ToDouble(objectnode.Attributes["zpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["length"].Value), (float)Convert.ToDouble(objectnode.Attributes["height"].Value), (float)Convert.ToDouble(objectnode.Attributes["rotation"].Value), (float)Convert.ToDouble(objectnode.Attributes["red"].Value), (float)Convert.ToDouble(objectnode.Attributes["green"].Value), (float)Convert.ToDouble(objectnode.Attributes["blue"].Value), (float)Convert.ToDouble(objectnode.Attributes["alpha"].Value), Convert.ToInt32(objectnode.Attributes["id"].Value), time, objectnode.Attributes["group"].Value.Split(commas, 99, StringSplitOptions.RemoveEmptyEntries), blending));
                        }

                        break;
                    case "createText":
                        float time2 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value);
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos)
                        {
                            time2 = 0;
                        }
                        else
                        {
                            time2 = time2 - startpos;
                        }
                        if (objectnode.Attributes["group"] == null)
                        { StartCoroutine(createText((float)Convert.ToDouble(objectnode.Attributes["xpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["ypos"].Value), (float)Convert.ToDouble(objectnode.Attributes["zpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["rotation"].Value), (float)Convert.ToDouble(objectnode.Attributes["red"].Value), (float)Convert.ToDouble(objectnode.Attributes["green"].Value), (float)Convert.ToDouble(objectnode.Attributes["blue"].Value), (float)Convert.ToDouble(objectnode.Attributes["alpha"].Value), time2, objectnode.Attributes["text"].Value)); }
                        else
                        { StartCoroutine(createText((float)Convert.ToDouble(objectnode.Attributes["xpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["ypos"].Value), (float)Convert.ToDouble(objectnode.Attributes["zpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["rotation"].Value), (float)Convert.ToDouble(objectnode.Attributes["red"].Value), (float)Convert.ToDouble(objectnode.Attributes["green"].Value), (float)Convert.ToDouble(objectnode.Attributes["blue"].Value), (float)Convert.ToDouble(objectnode.Attributes["alpha"].Value), time2, objectnode.Attributes["text"].Value, objectnode.Attributes["group"].Value.Split(commas, 99, StringSplitOptions.RemoveEmptyEntries))); }
                        break;
                    case "bulletcollision":
                        enableBulletCollision();
                        break;
                    case "style":
                        float time3 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value);
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos)
                        {
                            time3 = 0;
                        }
                        else
                        {
                            time3 = time3 - startpos;
                        }
                        StartCoroutine(changeStyle(Convert.ToInt32(objectnode.Attributes["id"].Value), time3));
                        break;
                    case "delete":
                        float time4 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value);
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos) { time4 = 0; }
                        else { time4 = time4 - startpos; }
                        StartCoroutine(deleteObject(Convert.ToInt32(objectnode.Attributes["upper"].Value), Convert.ToInt32(objectnode.Attributes["lower"].Value), time4));
                        break;
                    case "rotateSelf":
                        float time5 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value);
                        float duration5 = (float)Convert.ToDouble(objectnode.Attributes["duration"].Value);
                        float degree5 = (float)Convert.ToDouble(objectnode.Attributes["degrees"].Value);
                        if (duration5 == 0) { }
                        else if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos && (float)Convert.ToDouble(objectnode.Attributes["time"].Value) + (float)Convert.ToDouble(objectnode.Attributes["duration"].Value) > startpos) { time5 = 0; duration5 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos + (float)Convert.ToDouble(objectnode.Attributes["duration"].Value); degree5 = degree5 * (duration5 / (float)Convert.ToDouble(objectnode.Attributes["duration"].Value)); }
                        else { time5 = time5 - startpos; }
                        StartCoroutine(rotateGroupSelf(time5, duration5, Convert.ToInt32(objectnode.Attributes["id"].Value), degree5));
                        break;
                    case "rotateAround":
                        float time6 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value);
                        float duration6 = (float)Convert.ToDouble(objectnode.Attributes["duration"].Value);
                        float degree6 = (float)Convert.ToDouble(objectnode.Attributes["degrees"].Value);
                        if (duration6 == 0) { }
                        else if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos && (float)Convert.ToDouble(objectnode.Attributes["time"].Value) + (float)Convert.ToDouble(objectnode.Attributes["duration"].Value) > startpos) { time6 = 0; duration6 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos + (float)Convert.ToDouble(objectnode.Attributes["duration"].Value); degree6 = degree6 * (duration6 / (float)Convert.ToDouble(objectnode.Attributes["duration"].Value)); }
                        else { time6 = time6 - startpos; }
                        StartCoroutine(rotateGroupAround(time6, duration6, Convert.ToInt32(objectnode.Attributes["id"].Value), degree6, (float)Convert.ToDouble(objectnode.Attributes["xpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["ypos"].Value)));
                        break;
                    case "toggle":
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos) { StartCoroutine(toggleGroup(Convert.ToInt32(objectnode.Attributes["id"].Value), 0, objectnode.Attributes["state"].Value)); }
                        else
                        {
                            StartCoroutine(toggleGroup(Convert.ToInt32(objectnode.Attributes["id"].Value), (float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos, objectnode.Attributes["state"].Value));
                        }
                        break;
                    case "move":
                        float time7 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value);
                        float duration7 = (float)Convert.ToDouble(objectnode.Attributes["duration"].Value);
                        float xDisplacement = (float)Convert.ToDouble(objectnode.Attributes["xDisplacement"].Value);
                        float yDisplacement = (float)Convert.ToDouble(objectnode.Attributes["yDisplacement"].Value);
                        if (duration7 == 0) { if (time7 < startpos) { time7 = .01f; } }
                        else if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos && (float)Convert.ToDouble(objectnode.Attributes["time"].Value) + (float)Convert.ToDouble(objectnode.Attributes["duration"].Value) > startpos) { time7 = 0; duration7 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos + (float)Convert.ToDouble(objectnode.Attributes["duration"].Value); xDisplacement = xDisplacement * (duration7 / (float)Convert.ToDouble(objectnode.Attributes["duration"].Value)); yDisplacement = yDisplacement * (duration7 / (float)Convert.ToDouble(objectnode.Attributes["duration"].Value)); }
                        else if (time7 + duration7 < startpos) { time7 = 0.01f; duration7 = .01f; }
                        else { time7 = time7 - startpos; }
                        StartCoroutine(moveGroup(time7, duration7, Convert.ToInt32(objectnode.Attributes["id"].Value), xDisplacement, yDisplacement));
                        break;
                    case "scale":
                        float time8 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value);
                        float duration8 = (float)Convert.ToDouble(objectnode.Attributes["duration"].Value);
                        float xScale = (float)Convert.ToDouble(objectnode.Attributes["xScale"].Value);
                        float yScale = (float)Convert.ToDouble(objectnode.Attributes["yScale"].Value);
                        if (duration8 == 0) { }
                        else if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos && (float)Convert.ToDouble(objectnode.Attributes["time"].Value) + (float)Convert.ToDouble(objectnode.Attributes["duration"].Value) > startpos) { time8 = 0; duration8 = (float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos + (float)Convert.ToDouble(objectnode.Attributes["duration"].Value); xScale = xScale * (duration8 / (float)Convert.ToDouble(objectnode.Attributes["duration"].Value)); yScale = yScale * (duration8 / (float)Convert.ToDouble(objectnode.Attributes["duration"].Value)); }
                        else if (time8 + duration8 < startpos) { time8 = 0.01f; duration8 = .01f; }
                        else { time8 = time8 - startpos; }
                        StartCoroutine(scaleGroup(time8, duration8, Convert.ToInt32(objectnode.Attributes["id"].Value), xScale, yScale));
                        break;
                    case "mask":
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) < startpos) { StartCoroutine(maskGroup(0, Convert.ToInt32(objectnode.Attributes["id"].Value), (float)Convert.ToDouble(objectnode.Attributes["zposStart"].Value), (float)Convert.ToDouble(objectnode.Attributes["zposEnd"].Value))); }
                        else
                        {
                            StartCoroutine(maskGroup((float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos, Convert.ToInt32(objectnode.Attributes["id"].Value), (float)Convert.ToDouble(objectnode.Attributes["zposStart"].Value), (float)Convert.ToDouble(objectnode.Attributes["zposEnd"].Value)));
                        }
                        break;
                    case "swap":
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) >= startpos)
                        {
                            StartCoroutine(swapSetting((float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos, objectnode.Attributes["enable"].Value));
                        }
                        else
                        {
                            Debug.Log("poop");
                            StartCoroutine(swapSetting(0.1f, objectnode.Attributes["enable"].Value));
                        }
                        break;
                    case "invisibleTeleport":
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) >= startpos)
                        {
                            StartCoroutine(invisibleTeleport((float)Convert.ToDouble(objectnode.Attributes["leftAngle"].Value), (float)Convert.ToDouble(objectnode.Attributes["rightAngle"].Value), (float)Convert.ToDouble(objectnode.Attributes["destination"].Value), (float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos));
                        }
                        break;
                    case "alpha":
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) >= startpos)
                        {
                            StartCoroutine(alphaGroup((float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos, (float)Convert.ToDouble(objectnode.Attributes["fadetime"].Value), Convert.ToInt32(objectnode.Attributes["id"].Value), (float)Convert.ToDouble(objectnode.Attributes["alpha"].Value)));
                        }
                        else
                        {
                            Debug.Log("ween");
                            StartCoroutine(alphaGroup(0, 0, Convert.ToInt32(objectnode.Attributes["id"].Value), (float)Convert.ToDouble(objectnode.Attributes["alpha"].Value)));
                        }
                        break;
                    case "gameplayRotate":
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) >= startpos)
                        {
                            StartCoroutine(rotateGameplay((float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos, (float)Convert.ToDouble(objectnode.Attributes["duration"].Value), (float)Convert.ToDouble(objectnode.Attributes["degrees"].Value), (float)Convert.ToDouble(objectnode.Attributes["xpos"].Value), (float)Convert.ToDouble(objectnode.Attributes["ypos"].Value)));
                        }
                        break;
                    case "gameplayVelocity":
                        if ((float)Convert.ToDouble(objectnode.Attributes["time"].Value) >= startpos)
                        {
                            StartCoroutine(slowGameplay((float)Convert.ToDouble(objectnode.Attributes["time"].Value) - startpos, (float)Convert.ToDouble(objectnode.Attributes["speed"].Value)));
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        XmlNodeList myNodeList = xmlDoc.SelectNodes("//song/shoot");
        foreach (XmlNode node in myNodeList)
        {
            if ((float)Convert.ToDouble(node.Attributes["time"].Value) < startpos)
            {

            }
            else switch (node.Attributes["type"].Value)
                {
                    case "single":
                        StartCoroutine(shoot((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos));
                        break;
                    case "multishot":
                        StartCoroutine(multishot((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, Convert.ToInt32(node.Attributes["amount"].Value), (float)Convert.ToDouble(node.Attributes["delay"].Value)));
                        break;
                    case "spread":
                        StartCoroutine(spread((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, Convert.ToInt32(node.Attributes["amount"].Value), (float)Convert.ToDouble(node.Attributes["delay"].Value), node.Attributes["direction"].Value, (float)Convert.ToDouble(node.Attributes["spacing"].Value)));
                        break;
                    case "zigshot":
                        StartCoroutine(zigshot((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, Convert.ToInt32(node.Attributes["amount"].Value), (float)Convert.ToDouble(node.Attributes["delay"].Value), (float)Convert.ToDouble(node.Attributes["spacing"].Value)));
                        break;
                    case "aim":
                        StartCoroutine(aimshoot((float)Convert.ToDouble(node.Attributes["buffer"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos));
                        break;
                    case "teleporter":
                        StartCoroutine(teleporter((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["angle2"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos));
                        break;
                    case "spammer":
                        StartCoroutine(spammer((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, (float)Convert.ToDouble(node.Attributes["duration"].Value), Convert.ToInt32(node.Attributes["amount"].Value), Convert.ToInt32(node.Attributes["red"].Value), Convert.ToInt32(node.Attributes["green"].Value), Convert.ToInt32(node.Attributes["blue"].Value), node.Attributes["blending"].Value));
                        break;
                    case "localspammer":
                        StartCoroutine(localspammer((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, (float)Convert.ToDouble(node.Attributes["duration"].Value), Convert.ToInt32(node.Attributes["amount"].Value), Convert.ToInt32(node.Attributes["red"].Value), Convert.ToInt32(node.Attributes["green"].Value), Convert.ToInt32(node.Attributes["blue"].Value), node.Attributes["blending"].Value));
                        break;
                    case "holdcircle":
                        StartCoroutine(holdCircle((float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, (float)Convert.ToDouble(node.Attributes["aliveDuration"].Value), (float)Convert.ToDouble(node.Attributes["needDuration"].Value), (float)Convert.ToDouble(node.Attributes["xpos"].Value), (float)Convert.ToDouble(node.Attributes["ypos"].Value), (float)Convert.ToDouble(node.Attributes["size"].Value)));
                        break;
                    case "hexagon":
                        if (!nightcore)
                        {
                            StartCoroutine(hexagon((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos));
                        }
                        break;
                    case "heptagon":
                        if (nightcore)
                        {
                            StartCoroutine(heptagon((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos));
                        }
                        break;
                    default:
                        break;
                }
        }

        XmlNodeList myNodeListSpike = xmlDoc.SelectNodes("//song/spike");
        foreach (XmlNode node in myNodeListSpike)
        {
            if (node.Attributes["amount"] != null && node.Attributes["delay"] != null && (float)Convert.ToDouble(node.Attributes["time"].Value) < startpos && (float)Convert.ToDouble(node.Attributes["time"].Value) + ((float)Convert.ToDouble(node.Attributes["amount"].Value) - 1f) * (float)Convert.ToDouble(node.Attributes["delay"].Value) > startpos)
            {
                int amount = (int)(((float)Convert.ToDouble(node.Attributes["time"].Value) + ((float)Convert.ToDouble(node.Attributes["amount"].Value) - 1f) * (float)Convert.ToDouble(node.Attributes["delay"].Value) - startpos) / (float)Convert.ToDouble(node.Attributes["delay"].Value));
                switch (node.Attributes["type"].Value)
                {
                    case "multishot":
                        StartCoroutine(multishotspikes((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), 0, amount, (float)Convert.ToDouble(node.Attributes["delay"].Value)));
                        break;
                    case "spread":
                        StartCoroutine(spreadspikes((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), 0, amount, (float)Convert.ToDouble(node.Attributes["delay"].Value), node.Attributes["direction"].Value, (float)Convert.ToDouble(node.Attributes["spacing"].Value)));
                        break;
                    case "zigshot":
                        StartCoroutine(zigshotspikes((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), 0, amount, (float)Convert.ToDouble(node.Attributes["delay"].Value), (float)Convert.ToDouble(node.Attributes["spacing"].Value)));
                        break;

                }
            }
            else if ((float)Convert.ToDouble(node.Attributes["time"].Value) < startpos)
            {
                switch (node.Attributes["type"].Value)
                {
                    case "sticky":
                        if ((float)Convert.ToDouble(node.Attributes["time"].Value) < startpos && (float)Convert.ToDouble(node.Attributes["time"].Value) + (float)Convert.ToDouble(node.Attributes["duration"].Value) > startpos)
                        {
                            StartCoroutine(instantSticky((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["duration"].Value) - (startpos - (float)Convert.ToDouble(node.Attributes["time"].Value))));
                        }
                        break;
                }
            }
            else switch (node.Attributes["type"].Value)
                {
                    case "single":
                        StartCoroutine(shootspikes((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos));
                        break;
                    case "multishot":
                        StartCoroutine(multishotspikes((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, Convert.ToInt32(node.Attributes["amount"].Value), (float)Convert.ToDouble(node.Attributes["delay"].Value)));
                        break;
                    case "spread":
                        StartCoroutine(spreadspikes((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, Convert.ToInt32(node.Attributes["amount"].Value), (float)Convert.ToDouble(node.Attributes["delay"].Value), node.Attributes["direction"].Value, (float)Convert.ToDouble(node.Attributes["spacing"].Value)));
                        break;
                    case "zigshot":
                        StartCoroutine(zigshotspikes((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, Convert.ToInt32(node.Attributes["amount"].Value), (float)Convert.ToDouble(node.Attributes["delay"].Value), (float)Convert.ToDouble(node.Attributes["spacing"].Value)));
                        break;
                    case "aim":
                        StartCoroutine(aimspikes((float)Convert.ToDouble(node.Attributes["buffer"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos));
                        break;
                    case "cone":
                        if (node.Attributes["angleFromPlayer"] == null)
                        {
                            StartCoroutine(conespikes((float)Convert.ToDouble(node.Attributes["buffer"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, Convert.ToInt32(node.Attributes["amount"].Value)));
                        }
                        else { StartCoroutine(conespikes((float)Convert.ToDouble(node.Attributes["buffer"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, Convert.ToInt32(node.Attributes["amount"].Value), (float)Convert.ToDouble(node.Attributes["angleFromPlayer"].Value))); }

                        break;
                    case "sticky":
                        StartCoroutine(shootsticky((float)Convert.ToDouble(node.Attributes["angle"].Value), (float)Convert.ToDouble(node.Attributes["speed"].Value), (float)Convert.ToDouble(node.Attributes["time"].Value) - startpos, (float)Convert.ToDouble(node.Attributes["duration"].Value)));
                        break;
                    default:
                        break;
                }
        }



        XmlNodeList myNodeListColor = xmlDoc.SelectNodes("//song/color");
        foreach (XmlNode colornode in myNodeListColor)
        {
            float red = (float)Convert.ToDouble(colornode.Attributes["red"].Value);
            float green = (float)Convert.ToDouble(colornode.Attributes["green"].Value);
            float blue = (float)Convert.ToDouble(colornode.Attributes["blue"].Value);
            float fadetime = (float)Convert.ToDouble(colornode.Attributes["fadetime"].Value);
            float time = (float)Convert.ToDouble(colornode.Attributes["time"].Value);
            if (startpos > 0)
            {
                if (time + fadetime < startpos)
                {
                    time = 0;
                    fadetime = 0;
                }
                else if(time < startpos)
                {
                    fadetime = (time + fadetime) - startpos - .2f;
                    time = 0.2f;
                }
                else
                {
                    time = time - startpos;
                }

            }
            switch (colornode.Attributes["object"].Value)
            {
                case "circle":
                    StartCoroutine(colorChangeCircle(red, green, blue, fadetime, time));
                    break;
                case "bullets":
                    StartCoroutine(colorChangeBullets(red, green, blue, fadetime, time));
                    break;
                case "background":
                    StartCoroutine(colorChangeBackground(red, green, blue, fadetime, time));
                    break;
                case "shooter":
                    if(colornode.Attributes["alpha"] == null)
                    {
                        StartCoroutine(colorChangeShooter(red, green, blue, fadetime, time, 255));
                    }
                    else
                    {
                        StartCoroutine(colorChangeShooter(red, green, blue, fadetime, time, (float)Convert.ToDouble(colornode.Attributes["alpha"].Value)));
                    }
                    break;
                case "overlay":
                    StartCoroutine(colorChangeOverlay(red, green, blue, fadetime, time, (float)Convert.ToDouble(colornode.Attributes["alpha"].Value)));
                    break;
                case "inner":
                    StartCoroutine(colorChangeInner(red, green, blue, fadetime, time, (float)Convert.ToDouble(colornode.Attributes["alpha"].Value)));
                    break;
                case "object":
                    StartCoroutine(colorChangeObject(red, green, blue, (float)Convert.ToDouble(colornode.Attributes["alpha"].Value), fadetime, time, Convert.ToInt32(colornode.Attributes["id"].Value)));
                    break;
                case "group":
                    StartCoroutine(colorGroup(time, fadetime, Convert.ToInt32(colornode.Attributes["id"].Value), red, green, blue, (float)Convert.ToDouble(colornode.Attributes["alpha"].Value)));
                    break;
                case "theme":
                    StartCoroutine(colorChangeCircle(red, green, blue, fadetime, time));
                    StartCoroutine(colorChangeBullets(red, green, blue, fadetime, time));
                    StartCoroutine(colorChangeShooter(red, green, blue, fadetime, time, 255));
                    break;
                default:
                    break;
            }
        }

        foreach (XmlNode infonode in myNodeListInfo)
        {
            StartCoroutine(gonnaWinLevel((float)Convert.ToDouble(infonode.Attributes["endtime"].Value), startpos));
        }
    }

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        progress();

    }
}
