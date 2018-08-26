﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiModpackBuilder.Globals
{
    static class LinqExtension
    {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunks)
        {
            var count = source.Count();

            return source.Select((x, i) => new { value = x, index = i })
                .GroupBy(x => x.index / (int)Math.Ceiling(count / (double)chunks))
                .Select(x => x.Select(z => z.value).ToArray()).ToArray();
        }
    }
        
}
