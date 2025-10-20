using Xunit;

namespace System;

public class DedentTests
{
    [Fact]
    public void Dedent_NullString_ReturnsNull()
    {
        string? input = null;
        var result = input.Dedent();
        Assert.Null(result);
    }

    [Fact]
    public void Dedent_EmptyString_ReturnsEmptyString()
    {
        var input = "";
        var result = input.Dedent();
        Assert.Equal("", result);
    }

    [Fact]
    public void Dedent_SingleLineNoIndent_ReturnsSame()
    {
        var input = "hello world";
        var result = input.Dedent();
        Assert.Equal("hello world", result);
    }

    [Fact]
    public void Dedent_SingleLineWithIndent_RemovesIndent()
    {
        var input = "  hello world";
        var result = input.Dedent();
        Assert.Equal("hello world", result);
    }

    [Fact]
    public void Dedent_MultiLineNoCommonIndent_ReturnsSame()
    {
        var input = """
            line1
            line2
            """;
        var result = input.Dedent();
        Assert.Equal("""
            line1
            line2
            """, result);
    }

    [Fact]
    public void Dedent_MultiLineWithCommonIndent_RemovesCommonIndent()
    {
        var input = """
              line1
              line2
            """;
        var result = input.Dedent();
        Assert.Equal("""
            line1
            line2
            """, result);
    }

    [Fact]
    public void Dedent_MultiLineDifferentIndents_RemovesMinIndent()
    {
        var input = """
              line1
                line2
            """;
        var result = input.Dedent();
        Assert.Equal("""
            line1
              line2
            """, result);
    }

    [Fact]
    public void Dedent_WithEmptyLinesInMiddle_PreservesEmptyLines()
    {
        var input = """
              line1

              line3
            """;
        var result = input.Dedent();
        Assert.Equal("""
            line1

            line3
            """, result);
    }

    [Fact]
    public void Dedent_WithLeadingAndTrailingEmptyLines_RemovesThem()
    {
        var input = """

              line1
              line2

            """;
        var result = input.Dedent();
        Assert.Equal("""
            line1
            line2
            """, result);
    }

    [Fact]
    public void Dedent_AllEmptyLines_ReturnsEmptyString()
    {
        var input = """

            """;
        var result = input.Dedent();
        Assert.Equal("", result);
    }

    [Fact]
    public void Dedent_WithTabs_RemovesCommonTabIndent()
    {
        var input = "\tline1\n\tline2";
        var result = input.Dedent();
        Assert.Equal("line1\nline2", result);
    }

    [Fact]
    public void Dedent_WithMixedSpacesAndTabs_RemovesMinWhitespace()
    {
        var input = " \tline1\n \tline2";
        var result = input.Dedent();
        Assert.Equal("line1\nline2", result);
    }

    [Fact]
    public void Dedent_CRLFLineEndings_PreservesCRLF()
    {
        var input = "  line1\r\n  line2";
        var result = input.Dedent();
        Assert.Equal("line1\r\nline2", result);
    }

    [Fact]
    public void Dedent_CRLineEndings_PreservesCR()
    {
        var input = "  line1\r  line2";
        var result = input.Dedent();
        Assert.Equal("line1\rline2", result);
    }

    [Fact]
    public void Dedent_LFLineEndings_PreservesLF()
    {
        var input = "  line1\n  line2";
        var result = input.Dedent();
        Assert.Equal("line1\nline2", result);
    }

    [Fact]
    public void Dedent_ExampleFromDoc_WorksAsExpected()
    {
        var input = """
                Line 1
                Line 2
                Line 3
            """;
        var result = input.Dedent();
        Assert.Equal("""
            Line 1
            Line 2
            Line 3
            """, result);
    }

    [Fact]
    public void Dedent_LineShorterThanMinIndent_BecomesEmpty()
    {
        var input = """
              a
             
              b
            """;
        var result = input.Dedent();
        Assert.Equal("""
            a

            b
            """, result);
    }
}
