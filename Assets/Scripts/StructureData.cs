using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StructureData", menuName = "Scriptable Objects/StructureData")]
public class StructureData : ScriptableObject
{
    [Header("Información del NPC")]
    public string npcName = "npc";

    [Header("Lineas de Diálogo")]
    public List<string> dialogueLines = new List<string>();

    [Header("Imágenes")]
    public List<int> images = new List<int>();
}
