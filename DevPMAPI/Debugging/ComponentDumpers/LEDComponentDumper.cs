using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace PMAPI.Debugging.ComponentDumpers
{
    public class LEDComponentDumper : ComponentDumper
    {
        public override string TargetComponentFullName => "LED";

        public override void OnDump(Component component, XmlElement xmlElement, ComponentDumperList dumperList)
        {
            var part = component.Cast<Il2Cpp.LED>();

            xmlElement.SetXmlElement("capacity", part.capacity.ToString());
            xmlElement.SetXmlElement("cubeConnector", part.cubeConnector.ToString());
            xmlElement.SetXmlElement("energy", part.energy.ToString());
            xmlElement.SetXmlElement("interval", ElectricPart.interval.ToString());
            xmlElement.SetXmlElement("intervalTimer", part.intervalTimer.ToString());
            xmlElement.SetXmlElement("partType", part.GetPartType(part.cubeConnector).ToString());
            xmlElement.SetXmlElement("flowDirection", part.GetFlowDirection(part.cubeConnector).ToString());

            var spElement = xmlElement.OwnerDocument.CreateElement("sp");
            XmlHelper.DeserializeFieldsToXml(part.cubeBase.sp, spElement);
            xmlElement.AppendChild(spElement);

        }
    }
}

