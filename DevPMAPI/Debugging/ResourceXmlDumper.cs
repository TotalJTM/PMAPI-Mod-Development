﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using MelonLoader;

namespace PMAPI.Debugging
{
	/// <summary>
	/// Used for dumping the resources used by Primitier
	/// </summary>
	public static class ResourceXmlDumper
	{
		public static string FilePath = "ResourceDump.xml";

		/// <summary>
		/// Dumps all resources to ResourceXmlDumper.FilePath
		/// </summary>
		public static void DumpAllToFile()
		{
			MelonLogger.Msg("Starting ResourceDump Dump");
			XmlDocument document = new XmlDocument();
			var rootElement = document.CreateElement("Resources");
			DumpMaterials(rootElement);
			DumpSubstances(rootElement);
			DumpSounds(rootElement);

			document.AppendChild(rootElement);

			document.Save(FilePath);
			MelonLogger.Msg($"Dump complete saved at '{Path.Combine(Environment.CurrentDirectory, FilePath)}'");

		}

		/// <summary>
		/// Dumps all the substances to a string
		/// </summary>
		/// <param name="substance"></param>
		/// <returns></returns>
		public static string DumpSubstanceToString(Il2Cpp.SubstanceParameters.Param substance)
		{
			XmlDocument document = new XmlDocument();
			DumpSubstance(substance, document);
			return document.OuterXml;
		}

		private static void DumpSubstance(Il2Cpp.SubstanceParameters.Param substance, XmlNode parentNode)
		{
			var document = parentNode.OwnerDocument;
			if (document == null)
			{
				document = (XmlDocument)parentNode;
			}

			if (substance == null)
			{
				var nullNode = document.CreateElement("Null");

				parentNode.AppendChild(nullNode);
				return;
			}

			var substanceNode = document.CreateElement("Substance");
			substanceNode.SetAttribute("Name", substance.displayNameKey);

			XmlHelper.DeserializeFieldsToXml(substance, substanceNode);

			parentNode.AppendChild(substanceNode);
		}


		private static void DumpSubstances(XmlNode parentNode)
		{
			var document = parentNode.OwnerDocument;
			var substancesNode = document.CreateElement("Substances");

			if (Il2Cpp.SubstanceManager.instance == null)
			{
                Il2Cpp.SubstanceManager.instance = Resources.Load<Il2Cpp.SubstanceParameters>(Il2Cpp.SubstanceManager.ScriptableObjectPath);
			}

			for (int i = 0; i < Il2Cpp.SubstanceManager.instance.param.Count; i++)
			{
				DumpSubstance(Il2Cpp.SubstanceManager.instance.param[i], substancesNode);

			}
		

			parentNode.AppendChild(substancesNode);

		}

		private static void DumpSounds(XmlNode parentNode)
		{
			var document = parentNode.OwnerDocument;
			var soundsNode = document.CreateElement("Sounds");

			var resources = Resources.LoadAll(Il2Cpp.SoundManager.soundPath);
			for (int i = 0; i < resources.Count; i++)
			{
				DumpSound(resources[i].Cast<AudioClip>(), soundsNode, document);

			}

			parentNode.AppendChild(soundsNode);
		}
		private static void DumpSound(AudioClip sound, XmlNode parentNode, XmlDocument document)
		{
			var matNode = document.CreateElement("Sound");
			matNode.SetAttribute("Name", sound.name);

			parentNode.AppendChild(matNode);
		}



		private static void DumpMaterials(XmlNode parentNode)
		{
			var document = parentNode.OwnerDocument;
			var materialsNode = document.CreateElement("Materials");


			var resources = Resources.LoadAll(Il2Cpp.SubstanceManager.MaterialDirectory);
			for (int i = 0; i < resources.Count; i++)
			{
				DumpMat(resources[i].Cast<Material>(), materialsNode, document);

			}

			parentNode.AppendChild(materialsNode);
		}

		private static void DumpMat(UnityEngine.Material mat, XmlNode parentNode, XmlDocument document)
		{
			var matNode = document.CreateElement("Material");
			matNode.SetAttribute("Name", mat.name);
			matNode.SetAttribute("Color", mat.color.ToString());
			matNode.SetAttribute("Shader", mat.shader.name);

			parentNode.AppendChild(matNode);
		}


	}
}
