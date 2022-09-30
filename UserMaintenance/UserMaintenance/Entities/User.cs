using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMaintenance.Entities
{
    class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Keresztnev { get; set; }
        public string Vezeteknev { get; set; }
        public string TeljesNev
        {
            get
            {
                return string.Format(
                    "{0} {1}",
                    Keresztnev,
                    Vezeteknev);
            }
        }
    }
}
