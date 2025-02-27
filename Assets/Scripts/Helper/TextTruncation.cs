
public static class TextTruncation
{
    public static string TruncateText(string originalText, int maxCharacters = 20, string ellipsis = "...", int showCharacters = 5)
    {
        if (string.IsNullOrEmpty(originalText) || originalText.Length <= maxCharacters)
        {
            return originalText; // No truncation needed
        }

        // Construct the truncated text with ellipsis
        string truncated = originalText.Substring(0, showCharacters) + ellipsis +
                           originalText.Substring(originalText.Length - showCharacters);
        return truncated; // Return the truncated text
    }


}
