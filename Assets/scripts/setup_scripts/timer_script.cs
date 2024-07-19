using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer_script : MonoBehaviour
{
    public float timer_time = 60.0f;
    public bool start_timer = true;

    [SerializeField] manadger_script manadger_script;
    public Text timer_text;

    private void Start()
    {
        timer_text = GetComponent<Text>();
        timer_text.text = "60";
    }

    void Update()
    {
        if (start_timer)
        {
            timer_time -= Time.deltaTime;

            if (timer_time <= 0.0f)
            {
                DOTween.KillAll();
                SceneManager.LoadScene(0);
            }

            timer_text.text = Mathf.Round(timer_time).ToString();
        }
    }
}
