using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class FilterDTO
    {
        public int Alphabetically { get; set; }
        public int Students { get; set; }
        public int Date { get; set; }

        public override bool Equals(object? obj)
        {
            FilterDTO? filter = obj as FilterDTO;

            if(obj is not null)
            {
                if(filter.Alphabetically == this.Alphabetically 
                    && filter.Students == this.Students
                    && filter.Date == this.Date)
                    return true;
            }

            return false;
        }
    }
}
