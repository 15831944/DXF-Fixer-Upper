extern alias R12;  //this version of netDxf is from this fork: https://github.com/mfarquhar/netDXF, it supports saving in R12 but is very old
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using netDxf;
using netDxf.Entities;

namespace dxf_fixer
{
	public partial class Form1 : Form
	{

		public Form1()
		{
			InitializeComponent();
			AllowDrop = true;
			DragEnter += new DragEventHandler(dragEnter);
			DragDrop += new DragEventHandler(dragDrop);
		}

		void dragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		void dragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			fixDXF(files);
		}

		void fixDXF(string[] files)
		{
			foreach (string file in files)
			{
				//open in the version that supports saving and opening R12, do all the work in latest version, then save as R12

				string fileName = Path.GetFileName(file);
				string tempFileName = file.Substring(0, file.Length - fileName.Length) + "TMP.DXF";
				Console.WriteLine(fileName.Substring(fileName.Length - 4).ToLower());
				if (fileName == null || fileName.Substring(fileName.Length-4).ToLower() != ".dxf") continue;

				//open as R12 and save in different format for newer library
				R12.netDxf.DxfDocument inputDxf = new R12.netDxf.DxfDocument();
				inputDxf.Load(file);

				inputDxf.Save(tempFileName, R12.netDxf.Header.DxfVersion.AutoCad2010);

				//open in new library and do all the editing
				DxfDocument workingDxf = DxfDocument.Load(tempFileName);

				Arc[] toRemove = new Arc[workingDxf.Arcs.Count()];
				LwPolyline[] toAdd = new LwPolyline[workingDxf.Arcs.Count()];
				int count = 0;
				foreach (Arc i in workingDxf.Arcs)
				{
					toRemove[count] = i;
					toAdd[count] = i.ToPolyline(1000);
					count++;
				}

				for (int i = 0; i < toRemove.Length; i++)
				{
					workingDxf.RemoveEntity(toRemove[i]);
					workingDxf.AddEntity(toAdd[i]);
				}

				workingDxf.Save(tempFileName);

				//open it back in R12 library and save as R12
				R12.netDxf.DxfDocument outputDxf = new R12.netDxf.DxfDocument();
				outputDxf.Load(tempFileName);

				outputDxf.Save(file.Insert(file.Length - 4, "_FIXED"), R12.netDxf.Header.DxfVersion.AutoCad12);
				File.Delete(tempFileName);
			}
		}
	}
}
