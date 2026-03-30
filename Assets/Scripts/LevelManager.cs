using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private bool gameActive;
    public float timer;

    public float waitToShowEndScreen = 1f;
    void Start()
    {
        gameActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            timer += Time.deltaTime;
            UiController.instance.UpdateTimer(timer);
        }

    }

    public void EndLevel()
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {

        yield return new WaitForSeconds(waitToShowEndScreen);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UiController.instance.endTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00" + " secs ");

        UiController.instance.levelEndScreen.SetActive(true);
    }
}
