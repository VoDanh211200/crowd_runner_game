using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FortuneWheelManager : MonoBehaviour
{
    public FortuneWheel fortuneWheel;
    public Text resultLabel;
    void OnEnable()
    {
        resultLabel.text = "";
    }

    public void Spin()
    {
        StartCoroutine(SpinCoroutine());
    }
    IEnumerator SpinCoroutine()
    {
        yield return StartCoroutine(fortuneWheel.StartFortune());

        if(resultLabel == null) yield break;
        resultLabel.text = fortuneWheel.GetLatestResult();
        DataManager.Instance.AddCoins(int.Parse(fortuneWheel.GetLatestResult()));
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
