using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;

public class SandClock : MonoBehaviour
{
	[SerializeField] Image fillTopImage;
	[SerializeField] Image fillBottomImage;
	[SerializeField] TextMeshProUGUI roundText;
	[SerializeField] Image sandDotsImage;
	//[SerializeField] RectTransform sandPyramidRect;

	//Events
	[HideInInspector] public UnityAction onAllRoundsCompleted;
	[HideInInspector] public UnityAction<int> onRoundStart;
	[HideInInspector] public UnityAction<int> onRoundEnd;

    private bool _timeAudio = true;

    [SerializeField] private AudioClip _time;
    [SerializeField] private AudioSource _source;

    [Space (30f)]
	public float roundDuration = 1f;
	public float totalRounds = 3;

	//float defaultSandPyramidYPos;
	int currentRound = 0;

	void Awake ()
	{
		SetRoundText (Mathf.FloorToInt(totalRounds));
		sandDotsImage.DOFade (0f, 0f);
	}

    private void Start()
    {
        Begin();
    }

    public void ShortTime()
    {

        totalRounds -= Time.deltaTime;

        if (totalRounds <= 5 && _timeAudio)
        {
            _timeAudio = false;
            _source.clip = _time;
            _source.loop = true;

            // Inicie a reprodução.
            _source.Play();
        }
        else if (totalRounds > 5 && !_timeAudio)
        {
            _timeAudio = true;
            _source.clip = _time;
            _source.Stop();
        }
    }

    public void Begin ()
	{
		++currentRound;

		//start event
		if (onRoundStart != null)
			onRoundStart.Invoke (currentRound);
		
		
		sandDotsImage.DOFade (1f, .8f);
		sandDotsImage.material.DOOffset (Vector2.down * -roundDuration, roundDuration).From (Vector2.zero).SetEase (Ease.Linear);

		ResetClock ();
		
		roundText.DOFade (1f, .8f);
		
		fillTopImage
			.DOFillAmount (0, roundDuration)
			.SetEase (Ease.Linear)
			.OnUpdate (OnTimeUpdate)
			.OnComplete (OnRoundTimeComplete);
	}

	void OnTimeUpdate ()
	{
		fillBottomImage.fillAmount = 1f - fillTopImage.fillAmount;
	}

    void OnRoundTimeComplete()
    {
        // round end event
        if (onRoundEnd != null)
            onRoundEnd.Invoke(currentRound);

        sandDotsImage.DOFade(0f, 0f);

        if (totalRounds > 0)
        {
            // there is more rounds
            roundText.DOFade(0f, 0f);

            transform
            .DORotate(Vector3.forward * 180f, .8f, RotateMode.FastBeyond360)
            .From(Vector3.zero)
            .SetEase(Ease.InOutBack)
            .OnComplete(() => {
                SetRoundText(Mathf.FloorToInt(totalRounds));
                Begin();
            });

        }
        else
        {
            // finished all rounds or totalRounds reached 0

            // all rounds completed event
            if (onAllRoundsCompleted != null)
                onAllRoundsCompleted.Invoke();

            SetRoundText(0);
            transform.DOShakeScale(.8f, .3f, 10, 90f, true);
        }
    }


    void SetRoundText (int value)
	{
		roundText.text = value.ToString ();
	}

	public void ResetClock ()
	{
		transform.rotation = Quaternion.Euler (Vector3.zero);
		fillTopImage.fillAmount = 1f;
		fillBottomImage.fillAmount = 0f;
	}

    public void IncreaseTime(int additionalTime)
    {
        totalRounds += additionalTime;
    }

    void Update()
    {
        ShortTime();
    }
}
