using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingScript : MonoBehaviour
{
    public GameObject rankingPanel;
    public TMP_Text[] ranks;

    public void ActivePanel()
    {
        rankingPanel.SetActive(true);
    }

    public void SetRank(int score)
    {
        print(score);
        for (int i = 0; i < ranks.Length; i++)
        {
            if(score > System.Int16.Parse(ranks[i].text))
            {
                ranks[i].text = score.ToString();
                break;
            }
        }
    }

}
