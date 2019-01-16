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
using netDxf.Blocks;
using netDxf.Tables;

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

		private void Form1_Load(object sender, EventArgs e)
		{

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
				DxfDocument dxf = new DxfDocument();
				dxf.Load(file);

				DxfDocument newDxf = new DxfDocument();

				foreach (Arc i in dxf.Arcs)
				{
					Polyline x = i.ToPolyline(100);

					newDxf.AddEntity(x);
				}

				foreach (IEntityObject i in dxf.Circles)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Ellipses)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Faces3d)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Hatches)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Inserts)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Lines)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.NurbsCurves)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Points)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Polylines)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Solids)
				{
					newDxf.AddEntity(i);
				}
				foreach (IEntityObject i in dxf.Texts)
				{
					newDxf.AddEntity(i);
				}

				dxf.Save(file, netDxf.Header.DxfVersion.AutoCad12);
			}
		}
	}
}
