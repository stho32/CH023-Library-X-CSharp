namespace ArgsAsRoutes.Interfaces.ArgLang;

public interface IArgLangParser
{
    IRoute Parse(string code);
}