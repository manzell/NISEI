using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_Bit : MonoBehaviour
{
    Bit bit; 
    [SerializeField] Outline outline;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI bitValueLabel; 

    public void Start()
    {
        if (bit != null) Setup(bit); 
    }

    public void Setup(Bit bit)
    {
        this.bit = bit; 

        switch(bit.bitType)
        {
            case Bit.BitType.Barrier:
                outline.effectColor = Color.cyan;
                break;
            case Bit.BitType.CodeGate:
                outline.effectColor = Color.green;
                break;
            case Bit.BitType.Sentry:
                outline.effectColor = Color.red;
                break; 
        }

        image.color = bit.decrypted ? outline.effectColor : Color.black; 

        bitValueLabel.text = bit.value.ToString(); 
        bitValueLabel.enabled = bit.decrypted;

        bit.decryptEvent.AddListener(OnDecrypt); 
    }

    void OnDecrypt()
    {
        image.color = outline.effectColor;
    }
}
