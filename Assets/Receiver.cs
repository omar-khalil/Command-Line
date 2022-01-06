using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour {

    private CubeControls cube;

    public void Start()
    {
        cube = GetComponent<CubeControls>();
    }

    public void Action(Emitter.Command command)
    {
        if (command == Emitter.Command.Move)
        {
            cube.Move(transform.forward);
        }
        if (command == Emitter.Command.Reverse)
        {
            cube.Move(-transform.forward);
        }
        if (command == Emitter.Command.RotateR)
        {
            cube.Rotate(true);
        }
        if (command == Emitter.Command.RotateL)
        {
            cube.Rotate(false);
        }
        if (command == Emitter.Command.ContinuousMove)
        {
            cube.ContinuousMove();
        }
    }

}
