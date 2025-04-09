using System.Text;

namespace StartupIntrosBackend;

public class Post
{
  private readonly string _title;
  private readonly string _content;
  private readonly DateTime _date;
  private readonly string _author;

  public Post(string title, string content, DateTime date, string author)
  {
    this._title = title;
    this._content = content;
    this._date = date;
    this._author = author;
  }

  public string Serialize()
  {
    StringBuilder sb = new StringBuilder();
    sb.Append($"title: {_title}\n");
    sb.Append($"content: {_content}\n");
    sb.Append($"date: {_date}\n");
    sb.Append($"author: {_author}\n");
    return sb.ToString();
  }
}