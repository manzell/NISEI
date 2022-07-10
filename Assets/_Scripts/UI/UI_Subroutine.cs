using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Linq; 

public class UI_Subroutine : MonoBehaviour
{
    public Subroutine subroutine { get; private set; } 
    [SerializeField] TextMeshProUGUI subName, subDescription;
    [SerializeField] GameObject bitPrefab, bitArea; 

    public void Setup(Subroutine subroutine)
    {
        this.subroutine = subroutine;
        subName.text = $"{subroutine.name}"; 
        subDescription.text = subroutine.description;

        foreach (Transform t in bitArea.transform)
            Destroy(t.gameObject); 

        foreach(Bit bit in subroutine.bits)
            Instantiate(bitPrefab, bitArea.transform).GetComponent<UI_Bit>().Setup(bit); 
    }
}
