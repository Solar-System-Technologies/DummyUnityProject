using UnityEngine;
using System;
using System.Net.Http;
using System.Collections.Generic;

/*
public class DatabaseRunner : MonoBehaviour
{
    // in our case this will always be camera.Main, but it is good practice to 
    // have a data member and initialize /w start() for future flexibility
    public DatabaseViewTest database;
    public static HttpClient client = new HttpClient();
    void Start() 
    {
        Debug.Log("Entered Start() function!");
        string response = TcpConnTest.HttpRequest();
        Debug.Log($"Got response \n{response}");
        /*
        Debug.Log("Creating Database class...");

        database = new DatabaseViewTest("neo4j://localhost:7687", "neo4j", "password");
        Debug.Log("Database class created. \nReading from database");

        List<string> readTitles = database.ReadAllNodeTitles();
        foreach (string title in readTitles)
            Debug.Log($"Read from database, found title {title}");
        
        Debug.Log("Finished reading from database");

        
        string datetime = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
        string toWrite = $"Created new rubbish node on {datetime}";
        
        Debug.Log($"Writing some rubbish: \n{toWrite}");
        database.WriteRubbish(toWrite);
        Debug.Log("Written the rubbish"); 
    }

    void TestHttp()
    {
        Debug.Log("Testing Http requests");
        var response = client.GetAsync("http://google.com").Result; // should be synchronous? probably not tbh
        Debug.Log("Got response from request");
        if (response.IsSuccessStatusCode)
        {
            Debug.Log("Extracting string from response");
            string responseString = response.Content.ReadAsStringAsync().Result;
            Debug.Log($"Extracted response string \n{responseString}");
        }
    } 
} */