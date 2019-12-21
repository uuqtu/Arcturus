using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcturus.ConsoleExtensions.DataTableWriters
{
    public class ConsoleTableBuilderOption
    {
        public ConsoleTableBuilderOption()
        {
            Delimiter = "|";
            DividerString = "-";
            TrimColumn = false;
            MetaRowPosition = MetaRowPosition.None;
            MetaRowFormat = string.Empty;
            MetaRowParams = new[] { "" };
        }

        public MetaRowPosition MetaRowPosition { get; set; }
        public string MetaRowFormat { get; set; }
        public object[] MetaRowParams { get; set; }

        public string Delimiter { get; set; }
        public string DividerString { get; set; }

        /// <summary>
        /// Trim empty columns on right side
        /// </summary>
        public bool TrimColumn { get; set; }
    }

    public enum MetaRowPosition
    {
        None = 0,
        Top = 1,
        Bottom = 2
    }
}
