using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    int score ;
    // Start is called before the first frame update
    void Start()
    {
        score =0;
    }

    // Update is called once per frame
    public void ScoreUp()
    {
        score++;
        GetComponent<Text>().text = score.ToString();
    }
}
