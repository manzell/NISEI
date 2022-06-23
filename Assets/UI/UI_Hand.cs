using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_Hand : MonoBehaviour
{
    [SerializeField] Rig rig; 
    [SerializeField] GameObject cardPrefab, cardArea;
    [SerializeField] Button button; 

    private void Start()
    {
        rig.cardDrawEvent.AddListener(OnCardDraw);
        ServerManager.turnStartEvent.AddListener(UpdateButton); 
    }

    void OnCardDraw(Card card)
    {
        GameObject c = Instantiate(cardPrefab, cardArea.transform);
        c.GetComponent<UI_Card>().Setup(card); 
    }

    void UpdateButton()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "|RELOAD >";
        ServerManager.turnStartEvent.RemoveListener(UpdateButton);
    }
}
