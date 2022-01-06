using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExit : MonoBehaviour
{

    public AnimationCurve distanceOverTime;
    private float duration;
    private float rate;
    private Vector3 initialPosition;
    private Vector3 start;
    private Vector3 end;
    private float interval;
    private bool moving = false;
    private float moveTime;

    // Use this for initialization
    void Start()
    {
        duration = Random.Range(0.6f, 1.5f);
        rate = 1f / duration;

        initialPosition = transform.position;
        float randomHeight = Random.Range(20f, 30f);
        transform.position = new Vector3(transform.position.x, randomHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            moveTime += Time.deltaTime * rate;
            float animationTime = distanceOverTime.Evaluate(moveTime);
            transform.position = Vector3.Lerp(start, end, animationTime);
            if (moveTime >= 1)
            {
                moving = false;
                moveTime = 0;
            }
        }
    }


    public void Enter()
    {
        StartCoroutine(WaitEnter());
    }

    private IEnumerator WaitEnter()
    {
        yield return new WaitForSeconds(0.5f);
        PEnter();
    }

    private void PEnter()
    {
        start = transform.position;
        end = initialPosition;

        moving = true;
    }

    public void Exit(bool button)
    {
        StartCoroutine(WaitExit(button));
    }

    private IEnumerator WaitExit(bool button)
    {
        yield return new WaitForSeconds(button ? 0.1f : 1f);
        PExit();
    }

    private void PExit()
    {
        start = transform.position;
        float randomHeight = Random.Range(20f, 30f);
        end = new Vector3(transform.position.x, randomHeight, transform.position.z);
        moving = true;
    }
}
