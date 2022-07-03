using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UI_Bit : MonoBehaviour
{
    Bit bit; 
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
            image.color = Color.cyan;
        if (bit.bitType == barrier)
            image.color = Color.green;
        if (bit.bitType == barrier)
            image.color = Color.red;

        this.bit = bit;

        bit.decryptEvent.AddListener(OnDecrypt); 
    }

    void OnDecrypt()
    {
        bitValueLabel.text = bit.value.ToString();
        bitValueLabel.enabled = bit.decrypted;
    }

    IEnumerator UpdateBitValue()
    {
        string ruby = "qwertyiuop[]asdfghjkl;'zxcvbnm,./1234567890-=`~!@#$%^&*()_+QWERTYUIOP{}\\|ASDFGHJKL:ZXCVBNM<>?";
        bitValueLabel.text = ruby[Random.Range(0, ruby.Length)].ToString();

        yield return new WaitForSeconds(0.1f);
        UpdateBitValue(); 
    }
}
