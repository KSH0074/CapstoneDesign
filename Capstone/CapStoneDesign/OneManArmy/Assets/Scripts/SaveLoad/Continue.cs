using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    void Start()
    {
        SaveLoad.instance.LoadData(1);
        SaveLoad.instance.SaveData();
    }
}
