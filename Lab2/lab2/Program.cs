using System.Diagnostics;

string syncTask(string url)
{
    HttpClient httpClient = new HttpClient();

    try
    {
        var res = httpClient.GetAsync(url).GetAwaiter().GetResult();

        if (res.IsSuccessStatusCode)
        {
            return res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
        else
        {
            Console.WriteLine($"\nError: {res.StatusCode}");
            return null;
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException caught!");
        Console.WriteLine($"Message :{e.Message} ");
        return null;
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine("\nError!");
        Console.WriteLine($"Message :{e.Message} ");
        return null;
    }
}
void SyncTask()
{
    Stopwatch st = new Stopwatch();
    st.Start();

    var urls = new[]
        {
            "https://jsonplaceholder.typicode.com/todos/1",
            "https://official-joke-api.appspot.com/random_joke",
            "https://randomfox.ca/floof/"
        };

    foreach (var url in urls)
    {
        var response = syncTask(url);
        if (response != null)
        {
            Console.WriteLine(response);
        }
    }

    st.Stop();
    Console.WriteLine($"Time for executing requests using the synchronous method: {st.ElapsedMilliseconds} ms.");
}
async Task<string> asyncTask(string url)
{
    HttpClient httpClient = new HttpClient();

    try
    {
        var res = await httpClient.GetAsync(url);

        if (res.IsSuccessStatusCode)
        {
            return await res.Content.ReadAsStringAsync();
        }
        else
        {
            Console.WriteLine($"\nError: {res.StatusCode}");
            return null;
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException caught!");
        Console.WriteLine($"Message :{e.Message} ");
        return null;
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine("\nError!");
        Console.WriteLine($"Message :{e.Message} ");
        return null;
    }
}

async Task AsyncTask()
{
    Stopwatch st = new Stopwatch(); //
    st.Start(); //

    var urls = new[]
        {
            "https://jsonplaceholder.typicode.com/todos/1",
            "https://official-joke-api.appspot.com/random_joke",
            "https://randomfox.ca/floof/"
        };

    var tasks = new Task<string>[urls.Length];

    for (int i = 0; i < urls.Length; i++)
    {
        tasks[i] = asyncTask(urls[i]);
    }

    var responses = await Task.WhenAll(tasks);

    foreach (var response in responses)
    {
        if (response != null)
        {
            Console.WriteLine(response);
        }
    }

    st.Stop();
    Console.WriteLine($"Time for executing requests using the asynchronous method: {st.ElapsedMilliseconds} ms.");
}

SyncTask();
await AsyncTask();