using ArgsAsRoutes.BL.Entities;
using ArgsAsRoutes.Interfaces.ArgLang;

namespace ArgsAsRoutes.BL.TokenScanners;

public class EnframingTokenScanner : TokenScannerBase
{
    private readonly string _startsWith;
    private readonly string _endsWith;
    private readonly string _escapingCharacter;
    private readonly string _skipSequence;
    private readonly bool _putStartAndEndIntoTheResult;

    public EnframingTokenScanner(
        string typeName, 
        string startsWith, 
        string endsWith,
        string escapingCharacter,
        string skipSequence,
        bool putStartAndEndIntoTheResult) : base(typeName)
    {
        _startsWith = startsWith;
        _endsWith = endsWith;
        _escapingCharacter = escapingCharacter;
        _skipSequence = skipSequence;
        _putStartAndEndIntoTheResult = putStartAndEndIntoTheResult;
    }

    public override IToken? GetToken(string content, ref int position)
    {
        if (PeekingFromEquals(content, position, _startsWith))
        {
            if (_putStartAndEndIntoTheResult)
            {
                return new Token(
                    TypeName,
                    _startsWith + Collect(content, ref position) + _endsWith
                );
            }

            return new Token(
                TypeName,
                Collect(content, ref position)
            );
        }

        return null;
    }

    private string Collect(string content, ref int position)
    {
        position += _startsWith.Length;

        var result = "";
        var inEscapingMode = false;
        
        while (position < content.Length)
        {
            // When we can see the end, we better check, if the end is not also an escaping char.
            // E.g. verbatim string in C# : " can end the thing, but when " is part of "" it is actually a
            // skip sequence... 
            if (!string.IsNullOrEmpty(_skipSequence) &&
                DetectSkipSequence(content, position, _skipSequence))
            {
                MoveForwardBy(_skipSequence, ref position, ref result, _putStartAndEndIntoTheResult);
                continue;
            }
            
            // When we reach the end and it is not escaped, then it is the end alright..
            if (PeekingFromEquals(content, position, _endsWith) && !inEscapingMode)
            {
                break;
            }
            
            if (!string.IsNullOrEmpty(_escapingCharacter) && 
                PeekingFromEquals(content, position, _escapingCharacter))
            {
                inEscapingMode = true;
                MoveForwardBy(_escapingCharacter, ref position, ref result, _putStartAndEndIntoTheResult);
                continue;
            }

            result += content[position];
            position += 1;

            if (inEscapingMode)
            {
                inEscapingMode = false;
            }
        }

        position += _endsWith.Length;
        
        return result;
    }

    private void MoveForwardBy(string value, ref int position, ref string result, bool collectAsResult)
    {
        position += value.Length;
        if (collectAsResult)
        {
            result += value;
        }
    }

    private bool DetectSkipSequence(string content, int position, string skipSequence)
    {
        return PeekingFromEquals(content, position, skipSequence);
    }
}