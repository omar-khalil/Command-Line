using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSphere : MonoBehaviour
{

    public Emitter.Command holdingCommand;

    private Vector3 initialPosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Plane plane;

    //for moving back
    private bool movingBack;
    private bool foundEmitter;
    private Emitter foundEmitterObject;
    private Vector3 endMovement;
    private Vector3 velocity;
    private float speedTime = 0.1f;

    void Awake()
    {
        initialPosition = transform.position;
    }

    // Use this for initialization
    void Start()
    {
        foundEmitter = false;
        movingBack = false;
        plane = new Plane(Vector3.up, Vector3.up * initialPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingBack)
        {
            transform.position = Vector3.SmoothDamp(transform.position, endMovement, ref velocity, speedTime);
            if (transform.position == endMovement)
            {
                movingBack = false;
            }
        }
    }

    void OnMouseDown()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.clips[2], true);
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 pointalongplane = ray.origin + (ray.direction * distance);
            transform.position = pointalongplane;
        }

        RaycastHit rayHit;
        if (Physics.Raycast(transform.position, ray.direction, out rayHit))
        {
            //'used' is an unnecessary bool. Can just check if stored command != null
            if (rayHit.transform.gameObject.GetComponent<Emitter>() != null && !rayHit.transform.gameObject.GetComponent<CubeControls>().occupyingTile.blocker &&
                !rayHit.transform.gameObject.GetComponent<Emitter>().used)
            {
                foundEmitter = true;
                foundEmitterObject = rayHit.transform.GetComponent<Emitter>();
                endMovement = rayHit.transform.position;
            }
            else
            {
                foundEmitter = false;
                if (foundEmitterObject != null)
                {
                    foundEmitterObject = null;
                }
            }
        }
    }

    public void OnMouseUp()
    {
        if (!foundEmitter)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.clips[7], true);
            endMovement = initialPosition;
        }
        else
        {
            transform.position = endMovement + Vector3.up * 2;
            transform.parent = foundEmitterObject.transform;
            foundEmitterObject.used = true;
            foundEmitterObject.storedCommand = holdingCommand;
            SoundManager.instance.PlaySound(SoundManager.instance.clips[3], true);
            foundEmitterObject.Shoot();
        }
        movingBack = true;
    }
}
