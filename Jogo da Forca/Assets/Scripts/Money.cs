using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private int money;

    private void Awake()
    {
        money = PlayerPrefs.GetInt("money");
    }

    public void SetMoney(int _money)
    {
        money = _money;
        PlayerPrefs.SetInt("money", money);
    }

}
