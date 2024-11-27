using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.MirObjects
{
    class ItemFilter
    {
        public string Name;
        public bool Pick;
        public bool Sell;

        public static ItemFilter FromLine(string s)
        {
            string[] parts = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ItemFilter itemFilter = new ItemFilter();
            if (parts.Length > 0)
                itemFilter.Name = parts[0];

            if (parts.Length > 1)
                bool.TryParse(parts[1], out itemFilter.Pick);

            if (parts.Length > 2)
                bool.TryParse(parts[1], out itemFilter.Sell);

            return itemFilter;
        }

        public string ToText()
        {
            return string.Format("{0} {1} {2}", Name, Pick, Sell);
        }
    }
}
