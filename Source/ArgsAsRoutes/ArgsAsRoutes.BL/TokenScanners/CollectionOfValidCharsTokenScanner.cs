using ArgsAsRoutes.BL.Entities;
using ArgsAsRoutes.Interfaces.ArgLang;

namespace ArgsAsRoutes.BL.TokenScanners;

public class CollectionOfValidCharsTokenScanner : TokenScannerBase
{
    private readonly string _validCharacters;

    public CollectionOfValidCharsTokenScanner(string typeName, string validCharacters) : 
        base(typeName)
    {
        _validCharacters = validCharacters;
    }
    
    public override IToken? GetToken(
        string content, 
        ref int position)
    {
        if (_validCharacters.Contains(content[position]))
        {
            return new Token(
                TypeName,
                Collect(content, ref position)
            );
        }

        return null;
    }

    private string Collect(string content, ref int position)
    {
        var result = "";
        
        while (position < content.Length && _validCharacters.Contains(content[position]))
        {
            result += content[position];
            position += 1;
        }

        return result;
    }
}