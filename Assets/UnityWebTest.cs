using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class UnityWebTest : MonoBehaviour
{
    public float WaitTime = 10f;

    IEnumerator Start()
    {
        // A correct website page.
        CoroutineWithData routine = new CoroutineWithData(this, GetRequest("http://cloud-vm-42-36.doc.ic.ac.uk:7474/"));
        yield return routine.coroutine;
        Debug.Log($"Start() function received response \n {routine.result}");
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
}