using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json.Linq;

public class UnityWebTest : MonoBehaviour
{

    IEnumerator Start()
    {
        // A correct website page.
        //var routine = new CoroutineWithData(this, GetRequest("http://cloud-vm-42-36.doc.ic.ac.uk:7474/"));
        yield return StartCoroutine(
            SendWriteTransactions("CREATE (x :RUBBISH {title: 'hi'})")
        );

        yield return StartCoroutine(
            SendReadTransaction("MATCH (x :RUBBISH) RETURN x", 
            r => Debug.Log(r)
        ));
    }

    IEnumerator GetRequest(string uri)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        yield return webRequest.SendWebRequest();        

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError($"Uri: {uri}: connection error: \n{webRequest.error}");
                yield return webRequest.error;
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError($"Uri: {uri}: data processing error: \n{webRequest.error}");
                yield return webRequest.error;
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError($"Uri: {uri}: protocol error: \n{webRequest.error}");
                yield return webRequest.error;
                break;

            case UnityWebRequest.Result.Success: 
            default:
                yield return webRequest.downloadHandler.text;
                break;
        }
    }

    IEnumerator PostRequest(string uri, string postJson, Action<string> processResponse = null)
    {
        using var webRequest = new UnityWebRequest(uri, "POST");

        string username = "neo4j";
        string password = "s3cr3t";
        string toEncode = $"{username}:{password}";
        byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes(toEncode);
        string encodedString = System.Convert.ToBase64String(encodedBytes);

        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Accept", "application/json;charset=UTF-8");
        webRequest.SetRequestHeader("Authorization", $"Basic {encodedString}");

        byte[] postData = new System.Text.UTF8Encoding().GetBytes(postJson);
        webRequest.uploadHandler = new UploadHandlerRaw(postData);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return webRequest.SendWebRequest();
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError($"Uri: {uri}: connection error: \n{webRequest.error}");
                yield return webRequest.error;
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError($"Uri: {uri}: data processing error: \n{webRequest.error}");
                yield return webRequest.error;
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError($"Uri: {uri}: protocol error: \n{webRequest.error}");
                yield return webRequest.error;
                break;

            case UnityWebRequest.Result.Success: 
            default:
                yield return webRequest.downloadHandler.text;
                if (processResponse != null)
                    processResponse(webRequest.downloadHandler.text);
                break;
        }
    }


    IEnumerator SendReadTransaction(string query, Action<JObject> processResponse = null)
    {
        string transactionUrl = $"http://cloud-vm-42-36.doc.ic.ac.uk:7474/db/neo4j/tx/commit";
        string queryJson = @"{
            ""statements"": [{
                ""statement"": "" " + query + @" ""
            }]
        }";

        string response = "";
        yield return PostRequest(transactionUrl, queryJson, (r) => response = r);
        var responseJson = JObject.Parse(response);

        HandleErrorsFromResponse(query, responseJson);
        Debug.Log($"Read query response = {response}");

        if (processResponse != null)
            processResponse.Invoke(responseJson);
    }


    IEnumerator SendWriteTransactions(params string[] queries)
    {
        string transactionUrl = $"http://cloud-vm-42-36.doc.ic.ac.uk:7474/db/neo4j/tx/"; // will need to be updated to ".../neo4j/tx/{transaction id number}"
        string commitUrl = ""; 
        bool updatedUrls = false;

        foreach (string query in queries)
        {
            string queryJson = @"{
            ""statements"": [{ 
                    ""statement"":  "" " + query + @" ""
                }]
            }";

            string response = "";
            yield return PostRequest(transactionUrl, queryJson, (r) => response = r);
            var responseJson = JObject.Parse(response);
            HandleErrorsFromResponse(query, responseJson);
                
            if (!updatedUrls)
            {
                commitUrl = (string) responseJson["commit"];
                transactionUrl = Before(commitUrl, "/commit");                
                updatedUrls = true;
            }
        }

        string commitJson = @"{ 
            ""statements"": []
        }"; 
        string commitResponse = "";

        yield return PostRequest(commitUrl, commitJson, (r) => commitResponse = r);
        Debug.Log($"Write commit response = {commitResponse}");
    }

    private static void HandleErrorsFromResponse(string query, JObject responseJson)
    {            
        List<string> errors = responseJson["errors"]
            .Select(entry => (string) entry)
            .ToList();
        
        if (errors.Any())
        {
            string errorLog = $"Error running query {query}\n" + string.Join("\n", errors);
            Debug.LogError(errorLog);
            throw new InvalidOperationException(errorLog);
        }
    }

    private static string Before(string body, string section)
        => body.Split(new string[] {section}, StringSplitOptions.None)[0];
}