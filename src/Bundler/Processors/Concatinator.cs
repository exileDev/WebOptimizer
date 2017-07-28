﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Bundler.Processors
{
    /// <summary>
    /// Concatinates multiple files into a single string.
    /// </summary>
    /// <seealso cref="Bundler.Processors.IProcessor" />
    public class Concatinator : IProcessor
    {
        /// <summary>
        /// Gets the custom key that should be used when calculating the memory cache key.
        /// </summary>
        public string CacheKey(HttpContext context) => string.Empty;

        /// <summary>
        /// Executes the processor on the specified configuration.
        /// </summary>
        public void Execute(IAssetContext context)
        {
            IFileProvider fileProvider = AssetManager.Environment.WebRootFileProvider;
            IEnumerable<string> absolutes = context.Asset.SourceFiles.Select(f => fileProvider.GetFileInfo(f).PhysicalPath);
            var sb = new StringBuilder();

            foreach (string absolute in absolutes)
            {
                sb.AppendLine(System.IO.File.ReadAllText(absolute));
            }

            context.Content = sb.ToString();
        }
    }
}