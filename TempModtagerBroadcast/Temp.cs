﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TempModtagerBroadcast
{
    [DataContract]
    public class Temp
    {
        [DataMember]
        public float Temps;

        [DataMember]
        public int Id;
    }
}