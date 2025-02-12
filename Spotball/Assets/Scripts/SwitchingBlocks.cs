using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using System;
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
    public string[] componentsToDisable;
    void Start()
    {
        if (father)
        {
            if (numberOfBlinks % 2 != 0)
            {
                numberOfBlinks++;
                Debug.Log("Acordate de poner un numero par en number of blinks");
            }
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

            foreach (SwitchingBlocks block in blocksInTurns[currentTurn])
            {
                if (blocksInTurns.Count==1 && block.gameObject.activeSelf == false) break;
                StartCoroutine(Blink(block.GetComponent<SpriteRenderer>(), true));
            }
            currentTurn = currentTurn == turnsLength - 1 ? 0 : currentTurn + 1;
            foreach (SwitchingBlocks block in blocksInTurns[currentTurn])
            {
                if (blocksInTurns.Count == 1 && block.gameObject.activeSelf == true) break;
                block.gameObject.SetActive(true);
                StartCoroutine(Blink(block.GetComponent<SpriteRenderer>(), false));
            }
            yield return new WaitForSecondsRealtime(intervalTime*timeBeforeSwitchPercentage);
        }
    }
    IEnumerator Blink(SpriteRenderer sr, bool hide)
    {
        if (!hide) SetComponents(sr.gameObject, false);
        float timeForEveryBlink = (intervalTime*timeBeforeSwitchPercentage)/numberOfBlinks;
        int originalAlpha = hide ? 1 : 0;
        int finalAlpha = hide ? 0 : 1;
        for (int i = 0; i < numberOfBlinks; i++) 
        {
            if (i % 2 == 0) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, blinkAlpha);
            else sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, originalAlpha);
            yield return new WaitForSecondsRealtime(timeForEveryBlink);
        }
        if (hide)
        {
            sr.gameObject.SetActive(false);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, finalAlpha);
        }
        else
        {
            sr.gameObject.SetActive(true);
            SetComponents(sr.gameObject,true);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, finalAlpha);
        }
    }

    void SetComponents(GameObject target,bool enabling)
    {
        string[] targetComponentsToDisable = target.GetComponent<SwitchingBlocks>().componentsToDisable;
        foreach (string component in targetComponentsToDisable)
        {
            Type typeA = Type.GetType("UnityEngine." + component + ", UnityEngine");
            Type typeB = Type.GetType(component);
            if (typeA != null) EnableComponent(target,typeA, enabling);
            else if (typeB != null) EnableComponent(target, typeB, enabling);
            else GF.DebugFedeError("No se encontró el tipo: " + component);
        }
    }
    private void EnableComponent(GameObject target,Type type, bool enabling)
    {
        // Agarrar el componente de este gameObject
        Component comp = target.GetComponent(type);

        if (comp != null)
        {
            // Agarrar la propiedad enabled del tipo de componente
            var enabledProperty = type.GetProperty("enabled");
            if (enabledProperty != null)
            {
                // Desactivar o activar el componente
                enabledProperty.SetValue(comp, enabling);
            }
            else
            {
                GF.DebugFedeError("NO EXISTE PROPIEDAD");
            }
        }
        else GF.DebugFedeError("COMPONENT NULL");
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
}
