namespace PollStar.Core.ErrorCodes;

public abstract class PollStarErrorCode
{
    public abstract string Code { get; }
    public virtual string TranslationKey => $"{Namespace}.{Code}";
    public virtual string Namespace => GetType().ToString();
}