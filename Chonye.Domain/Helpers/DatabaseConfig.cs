using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chonye.Domain.Helpers
{
    public class DatabaseConfig
    {
        public int MaxSize { get; set; }

        public int MaxDepth { get; set; }

        public int DefaultSize { get; set; }

        public bool MarkDbReferenceAsUnchanged { get; set; }

        public bool AllowSetOfCollections { get; set; }

        public bool StopOnError { get; set; }

        public bool DetachWithChangeTracker { get; set; }

        public bool DepthEnabled { get; set; }

        public bool LoadcollectionsEnabled { get; set; }

        public bool IncludeEnabled { get; set; }

        public bool OnDeleteMarkAsUnchanged { get; set; }

        public bool EntityUtil_IncludeRelatedCollectionsAlways { get; set; }
    }
}
