using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer_script : MonoBehaviour
{
    public float timer_time = 60.0f;

    [SerializeField] manadger_script manadger_script;
    [SerializeField] Text timer_text;

    void Update()
    {
        timer_time -= Time.deltaTime;

        if (timer_time <= 0.0f)
        {
            SceneManager.LoadScene(0);
        }

        timer_text.text = Mathf.Round(timer_time).ToString();
    }
}
