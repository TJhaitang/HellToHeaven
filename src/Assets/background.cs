using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class background : MonoBehaviour
{
    private Transform camera_position;
    private Camera camera1;
    public float maxHeight = 80;

    // Start is called before the first frame update
    void Start()
    {
        camera1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.camera_position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cm = camera_position.position;
        Vector3 bg = new Vector3(0, 0, 0);
        float scale = camera1.orthographicSize * Screen.width / Screen.height / 5f;
        bg.y = 1f * cm.y - 13 * scale * (cm.y * 1f / maxHeight - 0.5f);
        transform.position = bg;
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
