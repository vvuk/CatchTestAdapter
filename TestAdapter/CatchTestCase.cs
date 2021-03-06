﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CatchTestAdapter.Tests
{
    [XmlRoot("Catch")]
    public class Catch
    {
        [XmlArray("Group")]
        [XmlArrayItem("TestCase", typeof(TestCase))]
        public TestCase[] TestCases { get; set; }
    }

    [XmlRoot("TestCase")]
    public class TestCase
    {
        private Expression[] _expressions = new Expression[] { };
        private TestCase[] _sections = new TestCase[] { };

        [XmlAttribute("name")]
        public string Name { get; set; } = "";
        [XmlAttribute("tags")]
        public string Tags { get; set; } = "";
        [XmlAttribute("filename")]
        public string Filename { get; set; } = "";
        [XmlAttribute("line")]
        public string Line { get; set; } = "";
        [XmlElement("Expression", typeof(Expression))]
        public Expression[] Expressions {
            get { return _expressions; }
            set { if (value != null) _expressions = value; }
        }
        [XmlElement("Warning")]
        public string Warning { get; set; } = "";
        [XmlElement("Info")]
        public string Info { get; set; } = "";
        [XmlElement("Failure", typeof(Failure))]
        public Failure Failure { get; set; }
        [XmlElement("Section", typeof(TestCase))]
        public TestCase[] Sections {
            get { return _sections; }
            set { if (value != null) _sections = value; }
        }
        [XmlElement("OverallResult", typeof(OverallResult))]
        public OverallResult Result { get; set; }
    }
    public class Failure
    {
        [XmlAttribute("filename")]
        public string Filename { get; set; } = "";
        [XmlAttribute("line")]
        public string Line { get; set; } = "";
        [XmlText]
        public string text;
    }
    public class Expression
    {
        [XmlAttribute("success")]
        public string Success { get; set; } = "";
        [XmlAttribute("type")]
        public string Type { get; set; } = "";
        [XmlAttribute("filename")]
        public string Filename { get; set; } = "";
        [XmlAttribute("line")]
        public string Line { get; set; } = "";
        [XmlElement("Original")]
        public string Original = "";
        [XmlElement("Expanded")]
        public string Expanded = "";
    }

    public class OverallResult
    {
        [XmlAttribute("success")]
        public string Success = "";
        [XmlAttribute("durationInSeconds")]
        public string Duration = "";
    }
}
