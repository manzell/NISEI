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
    [SerializeField] ICEType barrier, codeGate, sentry; 

    public void Start()
    {
        if (bit != null) Setup(bit); 
    }

    public void Setup(Bit bit)
    {
        if(bit.bitType == barrier)
            outline.effectColor = Color.cyan;
        if (bit.bitType == barrier)
            outline.effectColor = Color.green;
        if (bit.bitType == barrier)
            outline.effectColor = Color.red;

        this.bit = bit;
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
