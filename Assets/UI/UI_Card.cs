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
    [SerializeField] RawImage image;
    [SerializeField] Outline outline; 

    void Start()
    {
        if (card != null) Setup(card); 
    }

    public void Setup(Card card)
    {
        this.card = card;
        cardTitle.text = card.cardData.name;
        cardDescription.text = "hmm";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(card != null)
        {
            Rig rig = FindObjectOfType<Rig>(); // TODO Get a better reference for Rig
            
            card.Play(rig);
            rig.cardPlayEvent.Invoke(card);

            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) => outline.enabled = true;
    public void OnPointerExit(PointerEventData eventData) => outline.enabled = false; 
}