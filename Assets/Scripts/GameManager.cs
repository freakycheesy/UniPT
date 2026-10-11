using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string GetSavePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }
    public int currentController = 0;
    public GameControl[] controls;

    private void Awake()
    {
        using FileStream stream = new FileStream(GetSavePath("PT-Main"), FileMode.OpenOrCreate, FileAccess.Read);
        using BinaryReader reader = new BinaryReader(stream);
        currentController = reader.ReadInt32();

    }

    public void BeginGame()
    {

    }
}