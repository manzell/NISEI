using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro; 

public class UI_Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;
    [SerializeField] TextMeshProUGUI cardTitle, cardDescription;
    [SerializeField] Color hoverBGColor, hoverTextColor, textNormalColor; 
    [SerializeField] RawImage image;

    public void Setup(Card card)
    {
        this.card = card;
        cardTitle.text = card.name;
        cardDescription.text = card.description;

        OnPointerExit(null); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(card != null)
        {
            if(card.Try(ServerManager.currentRig))
                Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<UI_Debug>().SetMessage(card.description); 
        image.color = hoverBGColor;
        cardTitle.color = hoverTextColor; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<UI_Debug>().SetMessage(string.Empty);
        image.color = Color.clear;
        cardTitle.color = textNormalColor; 
    }
}