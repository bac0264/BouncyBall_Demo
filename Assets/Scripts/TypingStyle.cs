using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingStyle : MonoBehaviour
{
    public static TypingStyle instance;
    Text thisText;
    string defaultText;
    string strText;
    void Start()
    {
        if (instance == null) {
            instance = this;
        }
        _setup();
    }
    public void _setup() {
        thisText = GetComponent<Text>();
        strText = thisText.text;
        defaultText = thisText.text;
        Debug.Log(strText);
        thisText.text = "";
        StartCoroutine(showText(strText));
    }
    public void _reset() {
        thisText.text = "";
        strText = defaultText;
        Debug.Log(strText);
        StartCoroutine(showText(strText));
    }
    IEnumerator showText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            thisText.text += text[i];
            yield return new WaitForSeconds(0.1f);
        }
    }
}
