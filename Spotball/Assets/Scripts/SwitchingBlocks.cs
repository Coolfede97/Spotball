using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingBlocks : MonoBehaviour
{
    [Header("¡SOLO MODIFICAR SI ES FATHER!!!!!!")]
    [SerializeField] bool father;
    Dictionary<int, List<SwitchingBlocks>> blocksInTurns = new Dictionary<int, List<SwitchingBlocks>>();
    [SerializeField] float intervalTime;
    int currentTurn;
    int turnsLength;
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
            yield return new WaitForSeconds(intervalTime);
            foreach (SwitchingBlocks block in blocksInTurns[currentTurn])
            {
                block.gameObject.SetActive(false);
            }
            currentTurn = currentTurn == turnsLength - 1 ? 0 : currentTurn + 1;
            foreach (SwitchingBlocks block in blocksInTurns[currentTurn])
            {
                block.gameObject.SetActive(true);
            }
        }
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
