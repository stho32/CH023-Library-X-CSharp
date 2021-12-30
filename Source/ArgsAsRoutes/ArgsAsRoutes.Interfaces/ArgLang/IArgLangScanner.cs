namespace ArgsAsRoutes.Interfaces.ArgLang;

public interface IArgLangScanner
{
    IToken[] Scan(string code);
}