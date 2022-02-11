
/*using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


public class DatabaseViewTest : IDisposable
{
    private bool _disposed = false;
    private IDriver _driver;

    public DatabaseViewTest(string uri, string username, string password)
        => _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(username, password));

    ~DatabaseViewTest()
        => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
            _driver?.Dispose();

        _disposed = true;
    }

    public List<string> ReadAllNodeTitles()
    {
        string query = "MATCH (node :NODE) RETURN node.title";
        using var session = _driver.Session();
        return session.ReadTransaction(tx => 
        {
            var result = tx.Run(query);
            List<string> titles = new List<string>();
            foreach (var record in result)
                titles.Add(record[0].As<string>());
            
            return titles;
        });
    }

    
    public void WriteRubbish(string rubbish)
    {
        string query = $"CREATE ( :RUBBISH {{rubbish: '{rubbish}'}} )";
        using var session = _driver.Session();
        session.WriteTransaction(tx => 
        {
            return tx.Run(query).Consume();
        });
    }
} */