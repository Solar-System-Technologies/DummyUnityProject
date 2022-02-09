using System.Collections;
using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.IO.Compression;
using UnityEngine;

/*
public class TcpConnTest
{
    public static string HttpRequest()
    {
        string result = string.Empty;

        Debug.Log("Creating TCP client");
        using var tcp = new TcpClient("http://www.google.com", 80);
        Debug.Log("TCP client created");
        using var stream = tcp.GetStream();

        tcp.SendTimeout = 500;
        tcp.ReceiveTimeout = 1000;
        // Send request headers
        var builder = new StringBuilder();
        builder.AppendLine("GET /?scope=images&nr=1 HTTP/1.1");
        builder.AppendLine("Host: www.bing.com");
        //builder.AppendLine("Content-Length: " + data.Length);   // only for POST request
        builder.AppendLine("Connection: close");
        builder.AppendLine();
        var header = Encoding.ASCII.GetBytes(builder.ToString());
        
        Debug.Log("Writing stream");
        stream.Write(header, 0, header.Length);
        Debug.Log("Stream written to");

        // Send payload data if you are POST request
        //await stream.WriteAsync(data, 0, data.Length);

        // receive data
        Debug.Log("Getting result");
        using var memory = new MemoryStream();
        stream.CopyTo(memory); //await stream.CopyToAsync(memory);
        memory.Position = 0;
        var data = memory.ToArray();

        var index = BinaryMatch(data, Encoding.ASCII.GetBytes("\r\n\r\n")) + 4;
        var headers = Encoding.ASCII.GetString(data, 0, index);
        memory.Position = index;

        if (headers.IndexOf("Content-Encoding: gzip") > 0)
        {
            using GZipStream decompressionStream = new GZipStream(memory, CompressionMode.Decompress);
            using var decompressedMemory = new MemoryStream();

            decompressionStream.CopyTo(decompressedMemory);
            decompressedMemory.Position = 0;
            Debug.Log("Result obtained");
            result = Encoding.UTF8.GetString(decompressedMemory.ToArray());
        }
        else
        {
            Debug.Log("Result obtained");
            result = Encoding.UTF8.GetString(data, index, data.Length - index);
            //result = Encoding.GetEncoding("gbk").GetString(data, index, data.Length - index);
        }

        //Debug.WriteLine(result);
        return result;
    }

    private static int BinaryMatch(byte[] input, byte[] pattern)
    {
        int sLen = input.Length - pattern.Length + 1;
        for (int i = 0; i < sLen; ++i)
        {
            bool match = true;
            for (int j = 0; j < pattern.Length; ++j)
            {
                if (input[i + j] != pattern[j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                return i;
            }
        }
        return -1;
    }
} */