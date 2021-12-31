using ArgsAsRoutes.BL.TokenScanners;
using ArgsAsRoutes.Interfaces.ArgLang;

namespace ArgsAsRoutes.BL.Scanner;

public class ArgLangScanner : ScannerBase
{
    private const string AlphanumericCharacters = "qwertzuiopasdfghjklyxcvbnm1234567890";
    
    public ArgLangScanner() : 
        base(new ITokenScanner[]
        {
            new CollectionOfValidCharsButDifferentStartTokenScanner("variable", "$", AlphanumericCharacters + "_-"),
            new CollectionOfValidCharsTokenScanner("argument", AlphanumericCharacters + "-"),
            new CollectionOfValidCharsTokenScanner("whitespace", " \t\n\r"),
            new CollectionOfValidCharsTokenScanner("opening square bracket", "["),
            new CollectionOfValidCharsTokenScanner("closing square bracket", "]")
        })
    {
    }

    public override IToken[] Scan(string code)
    {
        /* We do not need any whitespace information. */
        var result = 
            base.
                Scan(code)
                .Where(x => x.TypeName != "whitespace")
                .ToArray();

        return result;
    }
}