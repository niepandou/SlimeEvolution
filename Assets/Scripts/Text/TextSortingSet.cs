using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSortingSet : MonoBehaviour
{
    private TextMeshPro text;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        
    }

    private void Start()
    {
        text.sortingLayerID = SortingLayer.NameToID("HelpText");
    }
}
