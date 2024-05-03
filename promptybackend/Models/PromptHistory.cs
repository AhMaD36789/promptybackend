namespace Prompty.Server.Models
{
    public class PromptHistory
    {
        public List<PromptItem> PromptHistoryItems { get; set; }
    }

    public class PromptItem
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

}
