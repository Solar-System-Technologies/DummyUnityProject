using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class UnityWebTest : MonoBehaviour
{

    IEnumerator Start()
    {
        // A correct website page.
        //var routine = new CoroutineWithData(this, GetRequest("http://cloud-vm-42-36.doc.ic.ac.uk:7474/"));
        string postUrl = "http://cloud-vm-42-36.doc.ic.ac.uk:7474/db/neo4j/tx";
        string postJson = @"{
  ""statements"": [
    {
      ""statement"": ""CREATE (n) RETURN n""
    }
  ]
}";

        var routine = new CoroutineWithData(this, PostRequest(postUrl, postJson));
        yield return routine.coroutine;
        Debug.Log($"Start() function received response \n {routine.GetResult<string>()}");
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

    IEnumerator PostRequest(string uri, string postJson)
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
                break;
        }
    }
}