using UnityEngine;
using System.Collections;

public class SlerpTowards : MonoBehaviour
{
    public AnimationCurve distanceOverTime;
    public float duration;

    private Vector3 target;
    private float rate;
    private Vector3 start;
    private bool moving;
    private bool rotating;
    private float time;

    public void Start()
    {
        time = 0;
        moving = false;
        rate = 1f / duration;
    }

    public void Update()
    {
        if (!moving && !rotating && Input.GetKeyDown(KeyCode.Z))
        {
            moving = true;
            start = transform.position;
            target = transform.position + transform.forward;
        }
        if (moving)
        {
            time += Time.deltaTime * rate;
            float moveTime = distanceOverTime.Evaluate(time);
            transform.position = Vector3.Lerp(start, target, moveTime);
            if (moveTime >= 1)
            {
                moving = false;
                time = 0;
            }
        }
        if (!rotating && !moving && Input.GetKeyDown(KeyCode.X))
        {
            rotating = true;
            start = transform.localEulerAngles;
            target = transform.localEulerAngles + Vector3.up * 90f;
        }
        if (rotating)
        {
            time += Time.deltaTime * rate;
            float moveTime = distanceOverTime.Evaluate(time);
            transform.localEulerAngles = Vector3.Lerp(start, target, moveTime);
            if (moveTime >= 1)
            {
                rotating = false;
                time = 0;
            }
        }
    }
}