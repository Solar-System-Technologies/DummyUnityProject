using UnityEngine;
using System;
using System.Collections.Generic;

public class DatabaseRunner : MonoBehaviour
{
    // in our case this will always be camera.Main, but it is good practice to 
    // have a data member and initialize /w start() for future flexibility
    public DatabaseViewTest database;
    void Start() 
    {
        database = new DatabaseViewTest("neo4j://cloud-vm-42-36.doc.ic.ac.uk:7687", "neo4j", "s3cr3t");
        List<string> readTitles = database.ReadAllNodeTitles();
        foreach (string title in readTitles)
            Debug.Log($"Found title {title}");

        string datetime = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss");
        string toWrite = $"Created new rubbish node on {datetime}";
        Debug.Log($"Writing some rubbish: \n{toWrite}");
        database.WriteRubbish(toWrite);
    }
}