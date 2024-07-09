﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace PMAPI.Debugging.ComponentDumpers
{
	public class MeshFilterDumper : ComponentDumper
	{
		public override string TargetComponentFullName => "UnityEngine.MeshFilter";


		public override void OnDump(Component component, XmlElement xmlElement, ComponentDumperList dumperList)
		{
			var meshFitlerComponent = component.Cast<MeshFilter>();

			var sharedMeshValue = "null";
			if (meshFitlerComponent.sharedMesh != null)
			{
				sharedMeshValue = meshFitlerComponent.sharedMesh.ToString();
			}
			xmlElement.SetAttribute("SharedMesh", sharedMeshValue);


		}
	}
}
