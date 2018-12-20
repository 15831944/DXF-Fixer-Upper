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
				//read file into string array
				string[] fileTxt = File.ReadAllLines(file);
				
				foreach (string line in fileTxt)
				{
					//TODO: Everything else
				}
			}
		}
	}
}
