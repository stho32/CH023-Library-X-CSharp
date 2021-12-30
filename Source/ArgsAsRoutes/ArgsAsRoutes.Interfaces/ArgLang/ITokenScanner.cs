namespace ArgsAsRoutes.Interfaces.ArgLang;

public interface ITokenScanner
{
    IToken? GetToken(string content, ref int position);
}