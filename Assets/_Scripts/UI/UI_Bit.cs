using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_Bit : MonoBehaviour
{
    Bit bit; 
    [SerializeField] Image image;
    [SerializeField] Outline outline; 
    [SerializeField] TextMeshProUGUI bitValueLabel;
    [SerializeField] string ruby = "qwertyiuop[]asdfghjkl;'zxcvbnm,./1234567890-=`~!@#$%^&*()_+QWERTYUIOP{}\\|ASDFGHJKL:ZXCVBNM<>?";
    [SerializeField] BitType barrier, codeGate, sentry; 

    public void Setup(Bit bit)
    {
        this.bit = bit;
        
        image.color = bit.bitType.color; 
        outline.effectColor = bit.bitType.color;

        bit.decryptEvent.AddListener(OnDecrypt);

        StartCoroutine(UpdateBitValue());
    }

    void OnDecrypt()
    {
        bitValueLabel.enabled = bit.decrypted;
        outline.enabled = bit.decrypted;
        image.color = Color.black; 
    }

    IEnumerator UpdateBitValue()
    {
        while(bit.decrypted == false)
        {
            bitValueLabel.text = ruby[Random.Range(0, ruby.Length)].ToString();

            yield return new WaitForSeconds(0.05f + (Random.value / 40));
        }
    }
}
