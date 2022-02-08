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
        Debug.Log("Entered Start() function!");
        Debug.Log("Creating Database class...");

        database = new DatabaseViewTest("neo4j://cloud-vm-42-36.doc.ic.ac.uk:7687", "neo4j", "s3cr3t");
        Debug.Log("Database class created. \nReading from database");

        List<string> readTitles = database.ReadAllNodeTitles();
        foreach (string title in readTitles)
            Debug.Log($"Read from database, found title {title}");
        
        Debug.Log("Finished reading from database");

        /*
        string datetime = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
        string toWrite = $"Created new rubbish node on {datetime}";
        
        Debug.Log($"Writing some rubbish: \n{toWrite}");
        database.WriteRubbish(toWrite);

        Debug.Log("Written the rubbish"); */
    }
}