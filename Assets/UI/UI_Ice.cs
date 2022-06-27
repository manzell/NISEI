using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using TMPro;
using System.Linq; 

public class UI_Ice : MonoBehaviour
{
    public ICE ice;
    [SerializeField] TextMeshProUGUI iceName;
    [SerializeField] GameObject bitPanel;
    [SerializeField] GameObject bitPrefab;
    [SerializeField] Outline outline;

    private void Start()
    {
        if (ice != null)
            Setup(ice);
    }

    public void Setup(ICE ice)
    {
        this.ice = ice;
        iceName.text = ice.name;
        gameObject.name = ice.name; 

        foreach (Transform t in bitPanel.transform)
            Destroy(t.gameObject);

        foreach(Bit bit in ice.bits)
        {
            GameObject _bitPrefab = Instantiate(bitPrefab, bitPanel.transform);
            _bitPrefab.GetComponent<UI_Bit>().Setup(bit); 
        }
    }
}
