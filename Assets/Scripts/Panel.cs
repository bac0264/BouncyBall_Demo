using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public static Panel instance;
    public float speed = 0.5F;
    public GameObject panel;
    public const float MAX = 700;
    public Vector3 defaultPanel;
    public bool contact;
    // Use this for initialization
    private void Start()
    {
        if (instance == null) instance = this;
        defaultPanel = panel.transform.position;
        contact = true;
        Debug.Log("panel: " + defaultPanel);
    }
    // Update is called once per frame
    void Update()
    {
        if (panel.transform.position.x >= MAX)
        {
            _PipeMovement();
        }

    }
    void _PipeMovement()
    {
        Vector3 temp = panel.transform.position; // Take current place
        temp.x = temp.x - speed; // giảm x theo hàm delta time.
        panel.transform.position = temp;
        Debug.Log("temp reset: " + temp);
    }
    public void _resetPanel()
    {
        panel.transform.position = defaultPanel;
        Debug.Log("panel reset: " + panel.transform.position);
    }
}
