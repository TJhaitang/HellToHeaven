using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class realCamaraFllower : MonoBehaviour
{
    public float smooth = 0.1f;
    public Transform whiteSlime;
    public Transform blackSlime;
    // Start is called before the first frame update
    void Start()
    {
        whiteSlime = GameObject.Find("whiteSlime").GetComponent<Transform>();
        blackSlime = GameObject.Find("blackSlime").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        if (whiteSlime != null && blackSlime != null)
        {
            if (transform.position.y != (whiteSlime.position.y + blackSlime.position.y) / 2)
            {
                Vector3 tarpos = new Vector3(transform.position.x, (whiteSlime.position.y + blackSlime.position.y) / 2, -10);
                transform.position = Vector3.Lerp(transform.position, tarpos, smooth);
                float scale = 5f * Math.Max((Math.Max(Math.Abs(whiteSlime.position.y - blackSlime.position.y), 5) + 6) / 11, 2f / (Screen.width * 1f / Screen.height * 1f));
                GetComponent<Camera>().orthographicSize = scale;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
