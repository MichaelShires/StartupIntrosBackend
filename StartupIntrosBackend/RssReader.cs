using System.ServiceModel.Syndication;
using System.Xml;

namespace StartupIntrosBackend;

public class RssReader
{
    // Asynchronous entry point for pulling the RSS feed
    public static async Task ReadRss()
    {
        // The RSS feed URL; update this to the feed you want to use.
        string rssFeedUrl = "https://techcrunch.com/feed/";

        try
        {
            // Create an instance of HttpClient (ideally use IHttpClientFactory in larger applications)
            using HttpClient client = new HttpClient();
            
            // Retrieve the RSS feed stream asynchronously
            using var stream = await client.GetStreamAsync(rssFeedUrl);

            // Create an XmlReader from the stream
            using var reader = XmlReader.Create(stream);

            // Load the feed into a SyndicationFeed object
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            
            // Ensure that resources are cleaned up properly
            reader.Close();

            // Output the feed title
            Console.WriteLine("Feed Title: " + feed.Title.Text);
            Console.WriteLine(new string('-', 40));

            // Iterate over all feed items
            foreach (var item in feed.Items)
            {
                Console.WriteLine("Item Title: " + item.Title.Text);
                Console.WriteLine("Publish Date: " + item.PublishDate.ToString("f"));

                // The Summary might be null so check first
                if (item.Summary != null)
                {
                    Console.WriteLine("Summary: " + item.Summary.Text);
                }
                else
                {
                    Console.WriteLine("Summary: (No summary available)");
                }
                
                Console.WriteLine(new string('-', 40));
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions such as network errors or parsing issues
            Console.WriteLine("An error occurred while retrieving or parsing the RSS feed:");
            Console.WriteLine(ex.Message);
        }
    }
}