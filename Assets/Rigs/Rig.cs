using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using System.Linq; 

public class Rig : MonoBehaviour
{
    public new string name; 
    public intRef memory; 
    public intRef busWidth;
    public intRef clockSpeed;
    public int availableMemory => memory - installedPrograms.Sum(programData => programData.memoryCost);

    public List<Program> installedPrograms = new List<Program>();
    public List<Card> hand, drawDeck, discard, trash;

    [SerializeField] List<Card> startingCards = new List<Card>();
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
        foreach (Card cardData in startingCards)
            drawDeck.Add(Instantiate(cardData));

        drawDeck = drawDeck.OrderBy(card => Random.value).ToList();
    }

    private void DrawHand()
    {
        int busSpace = (int)busWidth.Value; 

        for(int i = 0; i < drawDeck.Count; i++)
        {
            Card card = drawDeck[0];

            if (busSpace - card.busWidth >= 0)
            {
                drawDeck.Remove(card);
                hand.Add(card);
                cardDrawEvent.Invoke(card);
                busSpace -= card.busWidth; 
            }
            else
                break;
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

    public virtual void InstallProgram(Program program)
    {
        program = Instantiate(program); 

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
