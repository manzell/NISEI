using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq; 

public class UI_ExecutionStack : MonoBehaviour
{
    [SerializeField] GameObject executionPrefab, stackArea;
    [SerializeField] Button executeButton;
    [SerializeField] TextMeshProUGUI heading;

    UnityAction<Executable> updateHeading;

    private void Start()
    {
        updateHeading = exe => UpdateHeading();
        Setup(ServerManager.currentRig); 
    }

    void UpdateHeading()
    {
        heading.text = "Execution Stack > " +
            $"{stackArea.GetComponentsInChildren<UI_Executable>().Sum(_e => _e.exe.cycles.Value)}/{(string)ServerManager.currentRig.clockSpeed}";
    }

    public void Setup(Rig rig)
    {
        foreach (Transform t in stackArea.transform)
            Destroy(t.gameObject);

        RemoveListeners(rig);
        RegisterListeners(rig);

        UpdateHeading();
    }

    public void Execute() => ServerManager.currentRig?.Execute();

    private void RegisterListeners(Rig rig)
    {
        rig.enqueueEvent.AddListener(Enqueue);
        rig.enqueueEvent.AddListener(updateHeading);
        rig.dequeueEvent.AddListener(Dequeue);
        rig.dequeueEvent.AddListener(updateHeading);
        ServerManager.turnStartEvent.AddListener(UpdateHeading);
        ServerManager.turnStartEvent.AddListener(SetExecuteButtonStatus);
    }

    private void RemoveListeners(Rig rig)
    {
        rig.enqueueEvent.RemoveListener(Enqueue);
        rig.enqueueEvent.RemoveListener(updateHeading);
        rig.dequeueEvent.RemoveListener(Dequeue);
        rig.dequeueEvent.RemoveListener(updateHeading);
        ServerManager.turnStartEvent.RemoveListener(UpdateHeading);
        ServerManager.turnStartEvent.RemoveListener(SetExecuteButtonStatus);
    }

    public void Enqueue(Executable exe)
    {
        exe.updateExe.AddListener(UpdateHeading);

        Instantiate(executionPrefab, stackArea.transform)
            .GetComponent<UI_Executable>().Setup(exe); // TODO - styling, custom prefabs. 

        SetExecuteButtonStatus();
    }

    public void Dequeue(Executable exe)
    {
        exe.updateExe.RemoveListener(UpdateHeading);

        foreach (Transform t in stackArea.transform)
            if (t.GetComponent<UI_Executable>()?.exe == exe)
                Destroy(t.gameObject);

        SetExecuteButtonStatus(); 
    }

    void SetExecuteButtonStatus() => executeButton.interactable = stackArea.transform.childCount > 0;
}