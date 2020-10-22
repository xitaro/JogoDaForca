using UnityEngine;

public class Tip : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gm;
    [SerializeField] private Money money;

    [SerializeField] private GameObject buttonNoMoney;

    private int price = 3;
    private int rand;
    private string letter;

    private bool isTrying = true;

    public void AskForTip()
    {
        if (money.GetMoney() >= price)
        {
           while (isTrying)
           {
                //sorteio um numero
                rand = Random.Range(0, gm.word.Length);
                Debug.Log(rand);
                //salvo a letra em uma variavel
                letter = gm.word[rand].ToString().ToUpper();
                Debug.Log(letter);
                Debug.Log(gm.letters[rand].text);

                //Se é _
                if (gm.letters[rand].text == "_")
                {
                    isTrying = false;
                    //atualiza letra
                    gm.letters[rand].text = letter;
                    gm.discoveredLetters += 1;
                    gm.correctLetters.Add(letter);
                    gm.VerifyFullWord();
                    money.BuyStuff(price);
                    return;
                }                 
                else
                {
                    isTrying = true;
                }
            }

        }
        else
        {
            buttonNoMoney.SetActive(true);
        }
    }
}
