using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControls : MonoBehaviour
{
    public bool goalSphere;
    public Tile occupyingTile;

    //Smooth movement
    public AnimationCurve failCurve;
    public AnimationCurve successCurve;
    public AnimationCurve distanceOverTime;
    private float duration = 0.5f;
    private float rate;

    //Moving
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float interval;
    private bool moving;
    private float moveTime;

    //Rotating
    private Vector3 startRotation;
    private Vector3 endRotation;
    private bool rotating;
    private float rotateTime;

    private TileManager tileManager;

    void Start()
    {
        tileManager = transform.parent.GetComponentInChildren<TileManager>();
        interval = TileManager.interval;
        rate = 1f / duration;
        moving = false;
        //transform.localEulerAngles = new Vector3((int)transform.localEulerAngles.x, (int)transform.localEulerAngles.y, (int)transform.localEulerAngles.z);
    }

    void Update()
    {

        if (moving)
        {
            moveTime += Time.deltaTime * rate;
            float animationTime = distanceOverTime.Evaluate(moveTime);
            transform.position = Vector3.Lerp(startPosition, endPosition, animationTime);
            if (moveTime >= 1)
            {
                moving = false;
                moveTime = 0;
            }
        }

        if (rotating)
        {
            distanceOverTime = successCurve;
            rotateTime += Time.deltaTime * rate;
            float animationTime = distanceOverTime.Evaluate(rotateTime);
            transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, animationTime);
            if (rotateTime >= 1)
            {
                rotating = false;
                rotateTime = 0;
            }
        }
    }

    public void Move(Vector3 movement)
    {
        Tile targetTile = tileManager.GetAdjacentTile(occupyingTile, movement);

        float i = movement.x;
        float j = movement.z;
        startPosition = transform.position;
        endPosition = transform.position + new Vector3(i, 0f, j) * interval;
        if (targetTile == null || targetTile.occupied)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.clips[6], true);
            distanceOverTime = failCurve;
        }
        else {
            SoundManager.instance.PlaySound(SoundManager.instance.clips[5], true);
            distanceOverTime = successCurve;
        }
        moving = true;
    }

    public void Rotate(bool right)
    {
        startRotation = transform.localEulerAngles;
        endRotation = transform.localEulerAngles + new Vector3(0f, (int)(right ? 90 : -90), 0f);
        rotating = true;
        SoundManager.instance.PlaySound(SoundManager.instance.clips[5], true);
    }

    public void ContinuousMove()
    {
        Tile targetTile = tileManager.FindLastUnoccupiedTile(occupyingTile, transform.forward);
        Move((targetTile.transform.position - transform.position) / interval);
    }
}
