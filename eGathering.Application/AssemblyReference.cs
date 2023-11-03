﻿using System.Reflection;

namespace eGathering.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    public static readonly string Namespace = typeof(AssemblyReference).Namespace ?? throw new InvalidOperationException();
}