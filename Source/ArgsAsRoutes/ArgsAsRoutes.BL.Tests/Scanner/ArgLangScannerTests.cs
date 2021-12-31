using ArgsAsRoutes.BL.Scanner;
using Xunit;

namespace ArgsAsRoutes.BL.Tests.Scanner;

public class ArgLangScannerTests
{
    [Fact]
    public void detection_of_single_argument_works()
    {
        var scanner = new ArgLangScanner();
        var result = scanner.Scan("test");
        
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal("test", result[0].Content);
        Assert.Equal("argument", result[0].TypeName);
    }

    [Fact]
    public void detection_of_multiple_arguments_works()
    {
        var scanner = new ArgLangScanner();
        var result = scanner.Scan("hello world");
        
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Length);
        Assert.Equal("hello", result[0].Content);
        Assert.Equal("world", result[1].Content);
    }

    [Fact]
    public void detection_of_a_variable_works()
    {
        var scanner = new ArgLangScanner();
        var result = scanner.Scan("$name");
        
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal("$name", result[0].Content);        
        Assert.Equal("variable", result[0].TypeName);        
    }

    [Fact]
    public void detection_of_an_optional_argument_works()
    {
        var scanner = new ArgLangScanner();
        var result = scanner.Scan("[orange]");
        
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Length);
        Assert.Equal("[", result[0].Content);        
        Assert.Equal("orange", result[1].Content);        
        Assert.Equal("]", result[2].Content);
    }

    [Fact]
    public void detection_of_this_complex_expression_works_as_expected()
    {
        var scanner = new ArgLangScanner();
        var result = scanner.Scan("describe-dir --local $local-dir [--ignores $ignores] --output $output");
        
        Assert.NotEmpty(result);
        Assert.Equal("describe-dir", result[0].Content);        
        Assert.Equal("--local", result[1].Content);        
        Assert.Equal("$local-dir", result[2].Content);
        Assert.Equal("[", result[3].Content);
        Assert.Equal("--ignores", result[4].Content);
        Assert.Equal("$ignores", result[5].Content);
        Assert.Equal("]", result[6].Content);
        Assert.Equal("--output", result[7].Content);
        Assert.Equal("$output", result[8].Content);
    }
}