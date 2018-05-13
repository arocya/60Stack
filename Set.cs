using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace _60stack
{
    [DataContract(Name = "sets")]
    public class Set
    {
        [DataMember(Name = "set")]
        List<SetData> sD = new List<SetData>();

    }
}
