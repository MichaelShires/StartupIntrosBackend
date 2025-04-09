using System.ServiceModel.Syndication;
using System.Xml;
using StartupIntrosBackend.Models;

namespace StartupIntrosBackend.Services;

public class RssReaderService
{
    // Asynchronous entry point for pulling the RSS feed
    public static async Task<List<Post>> ReadRss(RssFeed source)
    {
        List<Post> posts = [];
        try
        {
            // Create an instance of HttpClient (ideally use IHttpClientFactory in larger applications)
            using var client = new HttpClient();
        
            // Retrieve the RSS feed stream asynchronously
            await using var stream = await client.GetStreamAsync(source.Url);

            // Create an XmlReader from the stream
            using var reader = XmlReader.Create(stream);

            // Load the feed into a SyndicationFeed object
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            
            // Iterate over all feed items and map each to a Post
            posts.AddRange(from item in feed.Items 
                let title = item.Title?.Text ?? "No Title" 
                let content = item.Summary?.Text ?? string.Empty 
                let date = item.PublishDate.DateTime 
                select new Post(title, content, date, source));
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while retrieving or parsing the RSS feed:");
            Console.WriteLine(ex.Message);
        }
    
        return posts;
    }
}