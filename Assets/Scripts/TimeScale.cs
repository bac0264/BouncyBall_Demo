using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour {
    [Range(0.1F, 5F)]public float Scale = 2F;
    public static TimeScale instance;
    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start () {
        Time.timeScale = Scale;
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = Scale;
	}
}
