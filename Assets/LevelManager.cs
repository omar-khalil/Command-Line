using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public static LevelManager instance;

    public Text levelText;
    public Level[] levels;


    public int currentLevelIndex = 0;
    private Level currentLevel;
    private bool loading = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        LoadLevel();
    }

    public void ButtonNextLevel()
    {
        LoadNextLevel(1, true);
        SoundManager.instance.PlaySound(SoundManager.instance.clips[8], false);
    }

    public void ButtonLastLevel()
    {
        LoadNextLevel(-1, true);
        SoundManager.instance.PlaySound(SoundManager.instance.clips[8], false);
    }

    public void ButtonRestart()
    {
        LoadNextLevel(0, true);
        SoundManager.instance.PlaySound(SoundManager.instance.clips[9], false);
    }

    public void LoadLevel()
    {
        if (currentLevelIndex == 0)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
        } else if (currentLevelIndex == levels.Length - 1)
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(false);
        } else
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
        }
        levelText.text = (currentLevelIndex + 1) + "";
        currentLevel = GameObject.Instantiate(levels[currentLevelIndex]).GetComponent<Level>();
        foreach (EnterExit o in currentLevel.gameObject.GetComponentsInChildren<EnterExit>())
        {
            o.Enter();
        }
    }

    public void LoadNextLevel(int increment, bool button)
    {
        if (!loading)
        {
            loading = true;
            currentLevelIndex += increment;
            foreach (EnterExit o in currentLevel.gameObject.GetComponentsInChildren<EnterExit>())
            {
                if (o.GetComponent<Collider>() != null)
                {
                    o.GetComponent<Collider>().enabled = false;
                }
                o.Exit(button);
            }
            StartCoroutine(WaitLoad());
        }
    }

    IEnumerator WaitLoad()
    {

        yield return new WaitForSeconds(1.9f);

        currentLevel.gameObject.SetActive(false);
        loading = false;
        LoadLevel();
    }
}
