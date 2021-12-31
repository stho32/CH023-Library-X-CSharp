using ArgsAsRoutes.Interfaces.ArgLang;

namespace ArgsAsRoutes.BL.Scanner;

public abstract class ScannerBase : IScanner
{
    private readonly ITokenScanner[] _tokenScanners;

    protected ScannerBase(ITokenScanner[] tokenScanners)
    {
        _tokenScanners = tokenScanners;
    }

    private IToken[] GetTokens(string code)
    {
        var result = new List<IToken>();

        var position = 0;
        while (position < code.Length)
        {
            var lastPosition = position;
            
            foreach (var tokenScanner in _tokenScanners)
            {
                var token = tokenScanner.GetToken(code, ref position);
                
                if (token != null)
                {
                    result.Add(token);
                    break;
                }
            }

            if (position < code.Length && 
                lastPosition == position)
            {
                var startPosition = position - 20;
                if (startPosition < 0)
                    startPosition = 0;
                var aBitMoreCode = code.Substring(startPosition, 20);

                var row = code.Substring(0, position).Count(x => x == '\n') + 1;
                
                throw new Exception(
                    $"I could not get past position {position}. I am probably missing a token scanner around here. The character is '{code[position]}', in row {row}. Look here '{aBitMoreCode}'");
            }
        }
        
        return result.ToArray();
    }

    public virtual IToken[] Scan(string code)
    {
        return GetTokens(code);
    }
}