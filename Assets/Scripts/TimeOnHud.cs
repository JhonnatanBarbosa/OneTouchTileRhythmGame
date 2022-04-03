using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOnHud : MonoBehaviour
{
    private int width, height;
    private Rect rect;
    private GUIStyle labelStyle;
    private string currentTime;

    void Awake() {
        width = Screen.width;
        height = Screen.height;
        rect = new Rect(10, 10, width-20, height-20);
    }

    void OnGUI() {

        labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        labelStyle.alignment = TextAnchor.UpperLeft;

        labelStyle.fontSize = 12*(width/200);

        currentTime = Time.time.ToString("f2");
        currentTime = currentTime + "s";

        GUI.Label(rect,currentTime,labelStyle);
    }
}
