﻿// <auto-generated />
#region License
// MIT License
// 
// Copyright (c) Daniel Cazzulino
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// Inspired by https://stackoverflow.com/a/5503565/24684
namespace System
{
    /// <summary>
    /// Provides usability extensions for strings.
    /// </summary>
    static partial class StringExtensions
    {
        static readonly Regex pattern = new Regex(@"(?<!\{)\{(\w+)([^\}]*)\}", RegexOptions.Compiled);

        /// <summary>
        /// Replaces named parameters in a string template.
        /// </summary>
        /// <param name="template">The string template to replace with the given values.</param>
        /// <param name="args">A dictionary of values to replace in the template.</param>
        /// <param name="provider">Optional provider of culture-specific formatting.</param>
        /// <returns>The formatted string.</returns>
        /// <example>
        /// The following example replaces a templatized URL string with values from a dictionary:
        /// <c>
        /// var url = "https://company.com/{path}/{id:n}?value={price:#,#.##}".FormatNamed(new()
        /// {
        ///   { "path", pathValue },
        ///   { "id", Guid.NewId() }
        ///   { "price", 42.42424242 }
        /// });
        /// 
        /// // url will equal something like: "https://company.com/foo/bar/e88884cc041b46729e96dbb1dcb4f5f0/42.42"
        /// </c>
        /// Note the formatting applied to the GUID and price (truncation of decimals), and how 
        /// the syntax for creating the dictionary can use type inference for the dictionary 
        /// type, making it more concise.
        /// </example>
        public static string FormatNamed(this string template, Dictionary<string, object?> args, IFormatProvider? provider = null)
        {
            var map = new Dictionary<string, int>();
            var list = new List<object?>();
            // Replaces named format with positional one, preserving the optional format specifier.
            var format = pattern.Replace(
                template,
                match =>
                {
                    var name = match.Groups[1].Captures[0].Value;
                    if (!map.ContainsKey(name))
                    {
                        map[name] = map.Count;
                        list.Add(args.TryGetValue(name, out var value) ? value : null);
                    }

                    return "{" + map[name] + match.Groups[2].Captures[0].Value + "}";
                });

            return provider == null ?
                string.Format(format, list.ToArray()) :
                string.Format(provider, format, list.ToArray());
        }

        /// <summary>
        /// Replaces named parameters in a string template from the public properties of the 
        /// specified <paramref name="args"/> object.
        /// </summary>
        /// <param name="template">The string template to replace with the given values.</param>
        /// <param name="args">A (potentially anonymous) object to use to format the template. Only its public properties will be used.</param>
        /// <param name="provider">Optional provider of culture-specific formatting.</param>
        /// <returns>The formatted string.</returns>
        /// <example>
        /// The following example replaces a templatized URL string with values from an anonymous object:
        /// <c>
        /// var url = "https://company.com/{path}/{id:n}?value={price:#,#.##}".FormatNamed(new
        /// {
        ///   Path = pathValue,
        ///   ID = Guid.NewId(),
        ///   price = 42.42424242
        /// });
        /// 
        /// // url will equal something like: "https://company.com/foo/bar/e88884cc041b46729e96dbb1dcb4f5f0/42.42"
        /// </c>
        /// Note the formatting applied to the GUID and price (truncation of decimals), and how the properties 
        /// are matched in a case-insensitive manner.
        /// </example>
        public static string FormatNamed(this string template, object args, IFormatProvider? provider = null)
            => FormatNamed(template, args.GetType().GetProperties().ToDictionary(x => x.Name, x => (object?)x.GetValue(args, null), StringComparer.OrdinalIgnoreCase), provider);
    }
}