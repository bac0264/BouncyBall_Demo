using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float screenHeight = Camera.main.orthographicSize * 10f;
        float bgHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        transform.localScale = new Vector3(screenHeight / bgHeight, screenHeight / bgHeight);
        Debug.Log(bgHeight);
        Debug.Log(screenHeight);
    }
}
