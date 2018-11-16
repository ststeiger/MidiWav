/*
 * Created by SharpDevelop.
 * User: Tefik Becirovic
 * Date: 15.09.2008
 * Time: 19:42
 * 
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TB.Instruments
{
	/// <summary>
	/// MainForm for SlidingScale.Test.
	/// </summary>
	public partial class MainForm : Form
	{

		/// <summary>
		/// MainForm constructor.
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			TrackBarsScroll(null,null);
		}

		/// <summary>
		/// Update Label and SlidingScale on change track bars.
		/// </summary>
		void TrackBarsScroll(object sender, EventArgs e)
		{
			slidingScale1.Value = trackBar1.Value+trackBar2.Value/10.0;
			label1.Text = slidingScale1.Value.ToString();
		}
	}
}
