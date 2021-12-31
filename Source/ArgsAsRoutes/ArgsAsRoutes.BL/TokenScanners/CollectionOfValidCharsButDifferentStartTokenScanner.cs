using ArgsAsRoutes.BL.Entities;
using ArgsAsRoutes.Interfaces.ArgLang;

namespace ArgsAsRoutes.BL.TokenScanners;

public class CollectionOfValidCharsButDifferentStartTokenScanner : TokenScannerBase
{
    private readonly string _validFirstCharacters;
    private readonly string _validNextCharacters;

    public CollectionOfValidCharsButDifferentStartTokenScanner(string typeName,
        string validFirstCharacters,
        string validNextCharacters) : 
        base(typeName)
    {
        _validFirstCharacters = validFirstCharacters;
        _validNextCharacters = validNextCharacters;
    }
    
    public override IToken? GetToken(string content, ref int position)
    {
        if (_validFirstCharacters.Contains(content[position]))
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
        var result = content[position].ToString();
        position += 1; 
        
        while (position < content.Length && _validNextCharacters.Contains(content[position]))
        {
            result += content[position];
            position += 1;
        }

        return result;
    }
}