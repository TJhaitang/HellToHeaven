using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraController : MonoBehaviour
{
    public Transform whiteSlime;
    public Transform blackSlime;
    public float smooth;

    // Start is called before the first frame update
    void Start()
    {
        whiteSlime = GameObject.Find("whiteSlime").GetComponent<Transform>();
        blackSlime = GameObject.Find("blackSlime").GetComponent<Transform>();
        smooth = 0.1f;
    }
    void LateUpdate()
    {
        if (whiteSlime != null)
        {
            if (transform.position.y != whiteSlime.position.y)
            {
                Vector3 targetPos = whiteSlime.position;
                targetPos.x = transform.position.x;
                transform.position = Vector3.Lerp(transform.position, targetPos, smooth);
            }
        }
        if (blackSlime != null)
        {
            if (GameObject.Find("blackFllower").transform.position.y != blackSlime.position.y)
            {
                Vector3 targetPos = blackSlime.position;
                targetPos.x = GameObject.Find("blackFllower").transform.position.x;
                GameObject.Find("blackFllower").transform.position = Vector3.Lerp(GameObject.Find("blackFllower").transform.position, targetPos, smooth);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
