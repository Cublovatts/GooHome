using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save File 1", menuName = "Data/New Save File", order = 1)]
public class SaveData : ScriptableObject
{
    public int deaths = 0;
    public int lastCheckpoint = 0;
}
