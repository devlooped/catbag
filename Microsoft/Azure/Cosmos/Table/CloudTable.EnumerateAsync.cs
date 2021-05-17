// <auto-generated />
// Dependencies:
// @nuget: Microsoft.Azure.Cosmos.Table
// @nuget: Microsoft.Bcl.AsyncInterfaces (netstandard2.0;net472)
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
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Azure.Cosmos.Table
{
    /// <summary>
    /// Usability extension methods for <see cref="CloudTable"/>.
    /// </summary>
    static partial class CloudTableExtensions
    {
        /// <summary>
        /// Executes a <see cref="CloudTable.ExecuteQuerySegmentedAsync(TableQuery, TableContinuationToken, CancellationToken)"/> 
        /// segmented query and asynchronously enumerates the results.
        /// </summary>
        /// <typeparam name="T">Type of entity being queried, typically inferred from the received <paramref name="query"/>.</typeparam>
        /// <param name="table">The table to execute the query against.</param>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellation">Optional cancellation token to stop the enumeration process.</param>
        /// <returns>An asynchronous enumerable that can be used with <c>async foreach</c>.</returns>
        public static async IAsyncEnumerable<T> EnumerateAsync<T>(this CloudTable table, TableQuery<T> query, [EnumeratorCancellation] CancellationToken cancellation = default)
            where T : ITableEntity, new()
        {
            TableContinuationToken? continuation = null;
            do
            {
                var segment = await table.ExecuteQuerySegmentedAsync(query, continuation, cancellation)
                    .ConfigureAwait(false);

                foreach (var entity in segment)
                    if (entity != null)
                        yield return entity;

            } while (continuation != null && !cancellation.IsCancellationRequested);
        }
    }
}
