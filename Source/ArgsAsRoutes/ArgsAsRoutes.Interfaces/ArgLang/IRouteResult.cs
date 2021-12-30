namespace ArgsAsRoutes.Interfaces.ArgLang;

public interface IRouteResult
{
    string Code { get; }
    IToken[] Tokens { get; }
    
    bool IsFulFilled { get; }
    Dictionary<string, string> ParsedArguments { get; }
}