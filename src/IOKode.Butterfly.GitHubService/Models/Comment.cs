namespace IOKode.Butterfly.GitHubService.Models;

public class Comment
{
    public DateTime CreatedAt { get; init; }
    public Author Author { get; init; }
    public string BodyHtml { get; init; }
    public IEnumerable<CommentReply> Replies { get; init; }
    public bool IsByAuthor => Author.Username == "montyclt";
}