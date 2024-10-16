﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;

#pragma warning disable CA1050 // Declare types in namespaces
public class SecretsFactAttribute : FactAttribute
{
    public SecretsFactAttribute(params string[] secrets)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets<SecretsFactAttribute>()
            .Build();

        var missing = new HashSet<string>();

        foreach (var secret in secrets)
        {
            if (string.IsNullOrEmpty(configuration[secret]))
                missing.Add(secret);
        }

        if (missing.Count > 0)
            Skip = "Missing user secrets: " + string.Join(',', missing);
    }
}

public class LocalFactAttribute : SecretsFactAttribute
{
    public LocalFactAttribute(params string[] secrets) : base(secrets)
    {
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")))
            Skip = "Non-CI test";
    }
}

public class CIFactAttribute : FactAttribute
{
    public CIFactAttribute()
    {
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")))
            Skip = "CI-only test";
    }
}

public class SecretsTheoryAttribute : TheoryAttribute
{
    public SecretsTheoryAttribute(params string[] secrets)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<SecretsTheoryAttribute>()
            .Build();

        var missing = new HashSet<string>();

        foreach (var secret in secrets)
        {
            if (string.IsNullOrEmpty(configuration[secret]))
                missing.Add(secret);
        }

        if (missing.Count > 0)
            Skip = "Missing user secrets: " + string.Join(',', missing);
    }
}

public class LocalTheoryAttribute : TheoryAttribute
{
    public LocalTheoryAttribute()
    {
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")))
            Skip = "Non-CI test";
    }
}

public class CITheoryAttribute : TheoryAttribute
{
    public CITheoryAttribute()
    {
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")))
            Skip = "CI-only test";
    }
}