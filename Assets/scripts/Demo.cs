using UnityEngine;

public class Demo : MonoBehaviour
{
	[SerializeField] SandClock clock;
	
	void Start ()
	{
		clock.onRoundStart += OnRoundStart;
		clock.onRoundEnd += OnRoundEnd;
		clock.onAllRoundsCompleted += OnAllRoundsCompleted;
	
		clock.Begin ();
	}
	
	void OnRoundStart (int round)
	{
		Debug.Log ("Round Start " + round);
	}
	
	void OnRoundEnd (int round)
	{
		Debug.Log ("Round End " + round);
	}
	
	void OnAllRoundsCompleted ()
	{
		Debug.Log ("......Time is over...........");
	}
	
	void OnDestroy ()
	{
		clock.onRoundStart -= OnRoundStart;
		clock.onRoundEnd -= OnRoundEnd;
		clock.onAllRoundsCompleted -= OnAllRoundsCompleted;
	}

    void Update()
    {
        // Exemplo: Aumentar o tempo do cronômetro quando o jogador coletar um item.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            clock.IncreaseTime(5); // Adiciona 5 segundos ao tempo atual do cronômetro.
			Debug.Log("Entrou");
        }
    }

}
