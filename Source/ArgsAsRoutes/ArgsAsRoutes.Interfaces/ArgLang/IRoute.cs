namespace ArgsAsRoutes.Interfaces.ArgLang;

public interface IRoute
{
    string Code { get; }
    IToken[] Tokens { get; }
    IRouteResult RunOn(string[] args);
}