using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace _60stack
{
    public class Person : IExtensibleDataObject
    {
        private ExtensionDataObject extensionDataObject;

        public ExtensionDataObject ExtensionData
        {
            get => extensionDataObject;
            set => extensionDataObject = value;
        }
    

        [DataMember(Name = "CustName")] internal string Name;

        [DataMember(Name = "CustID")] internal int ID;

        public Person(string newName, int newID)
        {
            Name = newName;
            ID = newID;
        }



    }


}
