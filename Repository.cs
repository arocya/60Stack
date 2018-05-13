using System.Runtime.Serialization;

namespace _60stack
{
    [DataContract(Name="repo")]
    public class Repository
    {
        [DataMember(Name="name")]
        public string Name { get; set; }
    }
}
