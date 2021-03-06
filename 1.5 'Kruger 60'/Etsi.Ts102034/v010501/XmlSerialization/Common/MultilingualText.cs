﻿// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.Common
{
    /// <summary>
    /// Specifies an element containing a textual message, which has a Language attribute specifying the language of the
    /// string, using the ISO 639-2 three letter language code.
    /// </summary>
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class MultilingualText
    {
        [XmlAttribute]
        public string Language;

        [XmlText]
        public string Value;
    } // class MultilingualText
} // namespace
