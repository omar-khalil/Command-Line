using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{

    //Wrong place to declar this
    public enum Command
    {
        Move,
        RotateR,
        RotateL,
        ContinuousMove,
        Reverse
    }

    public bool used = false;
    [HideInInspector]
    public Command storedCommand;

    private TileManager tileManager;
    private LineRenderer line;
    private float lineDistance;

    void Start()
    {
        tileManager = transform.parent.GetComponentInChildren<TileManager>();
        line = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 2.5f);
    }

    public void Shoot()
    {
        RaycastHit rayHit;

        if (Physics.Raycast(transform.position, transform.forward, out rayHit, tileManager.receiverLayer))
        {
            Receiver receiver = rayHit.transform.GetComponent<Receiver>();
            if (receiver != null)
            {
                Receiver r = rayHit.transform.GetComponent<Receiver>();
                //rayHit.transform.GetComponent<Receiver>().Action(storedCommand);
                StartCoroutine(WaitAction(r));
                lineDistance = rayHit.distance;
            }

        }
        else
        {
            print("Missed");
            lineDistance = 10f;
        }
        StartCoroutine(Laser());
    }

    IEnumerator Laser()
    {
        line.SetPosition(1, Vector3.forward * (lineDistance + 0.5f));
        yield return new WaitForSeconds(0.2f);
        line.SetPosition(1, Vector3.zero);
    }

    IEnumerator WaitAction(Receiver receiver)
    {
        yield return new WaitForSeconds(0.3f);
        receiver.Action(storedCommand);
    }
}
