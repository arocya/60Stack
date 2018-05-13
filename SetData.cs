using System.Runtime.Serialization;

namespace _60stack
{
    [DataContract(Name = "set")]
    public class SetData
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "ptcgoCode")]
        public string PtcgoCode { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "series")]
        public string Series { get; set; }

        [DataMember(Name = "totalCards")]
        public int TotalCards { get; set; }

        [DataMember(Name = "standardLegal")]
        public bool StandardLegal { get; set; }

        [DataMember(Name = "expandedLegal")]
        public bool ExpandedLegal { get; set; }

        [DataMember(Name = "releaseDate")]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "sybolUrl")]
        public string SymbolUrl { get; set;}

        [DataMember(Name = "logoUrl")]
        public string LogoUrl { get; set; }

        [DataMember(Name = "updateAt")]
        public string UpdateDate { get; set; }
    }
}
