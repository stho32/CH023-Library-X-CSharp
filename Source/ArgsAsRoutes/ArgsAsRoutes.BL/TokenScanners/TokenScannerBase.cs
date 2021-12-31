using ArgsAsRoutes.Interfaces.ArgLang;

namespace ArgsAsRoutes.BL.TokenScanners;

public abstract class TokenScannerBase : ITokenScanner
{
    protected readonly string TypeName;

    protected TokenScannerBase(string typeName)
    {
        TypeName = typeName;
    }
    
    public abstract IToken? GetToken(string content, ref int position);

    protected bool PeekingFromEquals(string content, int position, string expectedContent)
    {
        if (position > content.Length-1)
            return false;

        if (position + expectedContent.Length > content.Length)
            return false;
        
        return content.Substring(position, expectedContent.Length) == expectedContent;
    }
}