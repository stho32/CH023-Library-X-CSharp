using ArgsAsRoutes.Interfaces.ArgLang;

namespace ArgsAsRoutes.BL.Entities;

public class Token : IToken
{
    public string TypeName { get; }
    public string Content { get; }

    public Token(string typeName, string content)
    {
        TypeName = typeName;
        Content = content;
    }
}