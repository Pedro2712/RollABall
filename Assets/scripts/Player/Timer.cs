using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Timer : MonoBehaviour
{
    private float timer = 10;
    private bool _timeAudio = true;

    public TextMeshProUGUI timeText;

    [SerializeField] private AudioClip _time;
    [SerializeField] private AudioSource _source;

    public void PassTime()
    {

        timer -= Time.deltaTime;
        SetTimeText();

        if (timer <= 5 && _timeAudio)
        {
            _timeAudio = false;
            _source.clip = _time;
            _source.loop = true;

            // Inicie a reprodução.
            _source.Play();
        }
        else if (timer > 5 && !_timeAudio)
        {
            _timeAudio = true;
            _source.clip = _time;
            _source.Stop();
        }
    }

    void SetTimeText()
    {
        timeText.text = "Time: " + timer.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        PassTime();
    }
}
