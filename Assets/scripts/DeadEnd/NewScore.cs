using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewScore : MonoBehaviour
{
    public TextMeshProUGUI newScore;
    public TextMeshProUGUI OldScore;

    private static int highScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        newScore.text = GetPickup.count.ToString();
        PutHighScore();
    }

    private void PutHighScore() {

        if (highScore < GetPickup.count)
        {
            highScore = GetPickup.count;
            OldScore.text = GetPickup.count.ToString();
        }
        else {
            OldScore.text = highScore.ToString();
        }
    }
}
