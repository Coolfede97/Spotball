using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class SwitchingBlocks : MonoBehaviour
{
    [Header("¡SOLO MODIFICAR SI ES FATHER!!!!!!")]
    [SerializeField] bool father;
    Dictionary<int, List<SwitchingBlocks>> blocksInTurns = new Dictionary<int, List<SwitchingBlocks>>();
    [SerializeField] float intervalTime;
    int currentTurn;
    int turnsLength;
    int lastTurn;
    [SerializeField] int numberOfBlinks;
    [SerializeField][Range(0, 1)] float blinkAlpha;
    [SerializeField] [Range(0, 1)] float timeBeforeSwitchPercentage;
    [Header("¡NO MODIFICAR SI ES FATHER!!!!")]
    public int turn=0;
    void Start()
    {
        if (father)
        {
            GetBlocksInTurns();
            for (int i = 1; i < turnsLength; i++)
            {
                if (!blocksInTurns.ContainsKey(i))
                {
                    string errorMessage = $"No existe el turno {i}. ¿Se ha usted salteado un turno al asignar el correspondiente a los hijos?";
                    GF.DebugFedeError(errorMessage);
                    break;
                }
                foreach (SwitchingBlocks block in blocksInTurns[i]) 
                {
                    block.gameObject.SetActive(false);
                }
            }
            StartCoroutine(ActivateSequence());
        }
    }
    void GetBlocksInTurns()
    {
        List<SwitchingBlocks> children = GetChildrenSwitchingBlocks(gameObject);
        foreach (SwitchingBlocks block in children) 
        {
            if (blocksInTurns.ContainsKey(block.turn))
            {
                blocksInTurns[block.turn].Add(block);
            }
            else
            {
                blocksInTurns[block.turn] = new List<SwitchingBlocks>();
                blocksInTurns[block.turn].Add(block);
            }
        }
        turnsLength = blocksInTurns.Count;
    }

    IEnumerator ActivateSequence()
    {
        while(true)
        {
            yield return new WaitForSeconds(intervalTime-intervalTime*timeBeforeSwitchPercentage);

            for (int i = 0; i < numberOfBlinks; i++) 
            {
                for (int j = 0; j < blocksInTurns.Count; j++) 
                {
                    foreach (SwitchingBlocks sb in blocksInTurns[j])
                    {
                        StartCoroutine(Blink(sb.GetComponent<SpriteRenderer>(), i % 2 == 0));
                    }
                }
            }
            foreach (SwitchingBlocks block in blocksInTurns[currentTurn])
            {
                StartCoroutine(Blink(block.GetComponent<SpriteRenderer>(), true));
            }
            currentTurn = currentTurn == turnsLength - 1 ? 0 : currentTurn + 1;
            foreach (SwitchingBlocks block in blocksInTurns[currentTurn])
            {
                StartCoroutine(Blink(block.GetComponent<SpriteRenderer>(), false));
            }
        }
    }
    IEnumerator Blink(SpriteRenderer sr, bool hide)
    {
        float timeForEveryBlink = (intervalTime*timeBeforeSwitchPercentage)/numberOfBlinks;
        int alpha = hide ? 1 : 0;
        for (int i = 0; i < numberOfBlinks; i++) 
        {
            if (i % 2 == 0) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, blinkAlpha);
            else sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            yield return new WaitForSecondsRealtime(timeForEveryBlink);
        }
        if (hide) sr.gameObject.SetActive(false);
        else sr.gameObject.SetActive(true);
    }
    List<SwitchingBlocks> GetChildrenSwitchingBlocks(GameObject parent)
    {
        List<SwitchingBlocks> list = new List<SwitchingBlocks>();
        foreach (Transform child in parent.transform)
        {
            list.Add(child.GetComponent<SwitchingBlocks>());
        }
        return list;
    }
    void Update()
    {
        
    }
}
