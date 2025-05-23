using System.Collections;
using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cardDescriptionText;
    [SerializeField] private GameObject _cardPanel;

    public void ShowCard(Card card)
    {
        _cardDescriptionText.text = card.Description;
        _cardPanel.SetActive(true);

        StartCoroutine(HideCardAfterDelay(3f));
    }

    private IEnumerator HideCardAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _cardPanel.SetActive(false);
    }
}
