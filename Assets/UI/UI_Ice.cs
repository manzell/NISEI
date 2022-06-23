using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using TMPro;
using System.Linq; 

public class UI_Ice : MonoBehaviour, IPointerClickHandler
{
    public ICE ice;
    [SerializeField] TextMeshProUGUI iceName;
    [SerializeField] GameObject bitPanel;
    [SerializeField] GameObject bitPrefab;
    [SerializeField] Outline outline;

    public void OnPointerClick(PointerEventData eventData)
    {
        ServerManager.currentIce = outline.enabled ? null : ice;
        outline.enabled = !outline.enabled; 

        foreach (UI_Ice uiICE in FindObjectsOfType<UI_Ice>().Where(ui => ui != this))
            uiICE.outline.enabled = false; 
    }

    private void Start()
    {
        if (ice != null)
            Setup(ice);
    }

    public void Setup(ICE ice)
    {
        this.ice = ice;
        iceName.text = ice.data.name;
        gameObject.name = ice.data.name; 

        foreach (Transform t in bitPanel.transform)
            Destroy(t.gameObject);

        foreach(Bit bit in ice.data.bits)
        {
            GameObject _bitPrefab = Instantiate(bitPrefab, bitPanel.transform);
            _bitPrefab.GetComponent<UI_Bit>().Setup(bit); 
        }
    }
}
