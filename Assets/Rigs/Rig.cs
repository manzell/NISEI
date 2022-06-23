using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class Rig : MonoBehaviour
{
    public static UnityEvent<ICE> TraceEvent = new UnityEvent<ICE>();

    public new string name; 
    public intRef memory; // Determines number of installed programs
    public intRef busWidth; // Determines size of draw
    public intRef clockSpeed; // Determines number of cycles per turn, energy
    public int availableMemory => memory - installedPrograms.Sum(program => program.data.memoryCost);

    public List<Program> installedPrograms = new List<Program>();
    public List<Card> hand, drawDeck, discard, trash;

    [SerializeField] List<CardData> startingCards = new List<CardData>();
    public List<Executable> programExecutionStack { get; private set; } = new List<Executable>();

    [HideInInspector] public UnityEvent<Executable> 
        enqueueEvent = new UnityEvent<Executable>(),
        executeEvent = new UnityEvent<Executable>(),
        dequeueEvent = new UnityEvent<Executable>();
    [HideInInspector] public UnityEvent<Program> 
        installEvent = new UnityEvent<Program>(),
        uninstallEvent = new UnityEvent<Program>();
    [HideInInspector] public UnityEvent<Card> 
        cardDrawEvent = new UnityEvent<Card>(),
        cardPlayEvent = new UnityEvent<Card>(),
        cardDiscardEvent = new UnityEvent<Card>(),
        cardTrashEvent = new UnityEvent<Card>();

    public void Awake()
    {
        CreateStartingDrawDeck();
        ServerManager.turnStartEvent.AddListener(DrawHand);
        cardPlayEvent.AddListener(OnPlayCard); 
    }

    [ContextMenu("Start Turn")]
    public void StartTurn() => ServerManager.turnStartEvent.Invoke(); 

    private void CreateStartingDrawDeck()
    {
        foreach (CardData cardData in startingCards)
            drawDeck.Add(new Card(cardData));

        drawDeck = drawDeck.OrderBy(card => Random.value).ToList();
    }

    private void DrawHand()
    {
        int drawNum = 5; // TODO make this dynamic

        for(int i = 0; i < drawNum; i++)
        {
            if (drawDeck.Count > 0)
            {
                Card card = drawDeck[0];
                drawDeck.Remove(card);
                hand.Add(card);
                cardDrawEvent.Invoke(card); 
            }
            else break; 
        }
    }

    void OnPlayCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            discard.Add(card); 
        }
    }

    public virtual void InstallProgram(ProgramData programData)
    {
        Program program = new Program(programData); 
        program.rig = this; 
        installedPrograms.Add(program);
        installEvent.Invoke(program);        
    }

    public void Enqueue(Executable exe)
    {
        if (programExecutionStack.Sum(e => e.cycles.Value) + exe.cycles.Value <= clockSpeed)
        {
            programExecutionStack.Add(exe);
            enqueueEvent.Invoke(exe);
        }
    }

    public void Dequeue(Executable exe)
    {
        programExecutionStack.Remove(exe);
        dequeueEvent.Invoke(exe); 
    }

    public void Execute()
    {
        List<Executable> dequeList = new List<Executable>(); 

        for(int i = 0; i < programExecutionStack.Count; i++)
        {
            Executable exe = programExecutionStack[i];

            //TODO: Charge cycles
            exe.Execute();
            executeEvent.Invoke(exe);

            if (exe.flushable)
                dequeList.Add(exe); 
        }

        foreach (Executable exe in dequeList)
            Dequeue(exe);

        ServerManager.turnEndEvent.Invoke(); 
    }

    public void RemoveCard(Card card)
    {
        hand.Remove(card);
        discard.Remove(card);
        drawDeck.Remove(card);
        trash.Add(card);

        cardTrashEvent.Invoke(card); 
    }
}
