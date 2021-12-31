namespace ArgsAsRoutes.Interfaces.ArgLang;

public interface IScanner
{
    IToken[] Scan(string code);
}