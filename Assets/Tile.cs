using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool occupied;
    public bool blocker;
    public bool goal;
    public bool goalReached;
    public GameObject particles;

    //try 2
    public int i;
    public int j;

    private Level level;

    //Entrance
    public void Start()
    {
        level = GetComponentInParent<Level>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CubeControls>() != null)
        {
            if (!occupied)
            {
                occupied = true;
                other.GetComponent<CubeControls>().occupyingTile = this;
                if (goal && other.GetComponent<CubeControls>().goalSphere)
                {
                    goalReached = true;
                    StartCoroutine(GoalMaintained());

                }
            }
        }
    }

    IEnumerator GoalMaintained()
    {
        yield return new WaitForSeconds(0.3f);
        if (goalReached)
        {
            level.curGoals++;
            SoundManager.instance.PlaySound(SoundManager.instance.clips[4], false);
            ParticleSystem[] particleSystems = particles.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem p in particleSystems)
            {
                ParticleSystem.EmissionModule pe = p.emission;
                pe.enabled = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CubeControls>() != null)
        {
            occupied = false;
            if (goalReached)
            {
                level.curGoals--;
                goalReached = false;
                ParticleSystem[] particleSystems = particles.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem p in particleSystems)
                {
                    ParticleSystem.EmissionModule pe = p.emission;
                    pe.enabled = false;
                }
            }
        }
    }
}
