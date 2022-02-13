namespace IOKode.Butterfly.GitHubService.Models;

public class CommentReply
{
    public DateTime CreatedAt { get; init; }
    public Author Author { get; init; }
    public string BodyHtml { get; init; }
    public bool IsByAuthor => Author.Username == "montyclt";
}