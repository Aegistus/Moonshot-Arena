using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopupUI : MonoBehaviour
{
    public Text popup;
    public float popupHideTime = 3f;

    private int lastScore = 0;
    private int currentlyShowing = 0;
    private bool popupShowing = false;

    private void Awake()
    {
        ScoreManager.OnScoreChange += ShowPopup;
        popup.gameObject.SetActive(false);
    }

    private void ShowPopup(int newScore)
    {
        if (!popupShowing)
        {
            popup.text = "+" + (newScore - lastScore);
            currentlyShowing = newScore - lastScore;
        }
        else
        {
            popup.text = "+" + (newScore - lastScore + currentlyShowing);
            currentlyShowing = newScore - lastScore + currentlyShowing;
            StopCoroutine("ReHidePopup");
        }
        popupShowing = true;
        popup.gameObject.SetActive(true);
        lastScore = newScore;
        StartCoroutine("ReHidePopup");
    }

    private IEnumerator ReHidePopup()
    {
        yield return new WaitForSeconds(popupHideTime);
        popup.gameObject.SetActive(false);
        popupShowing = false;
    }
}
