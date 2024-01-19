using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using DiscSoft.NET.Common.View.Controls;
using DiscSoft.NET.Common.View.Controls.Buttons;
using DiscSoft.NET.Common.View.SecondaryWindows.Base;
using reSENSIUI.DataModels;
using reSENSIUI.Utils;
using reSENSIUI.ViewModels.SecondaryWindows;

namespace reSENSIUI.Views.SecondaryWindows
{
	public partial class AddImportConfig : BaseSecondaryWindow
	{
		public AddImportConfig(ConfigVM configVM)
		{
			AddImportConfigViewModel addImportConfigViewModel = new AddImportConfigViewModel
			{
				ConfigVM = configVM
			};
			base.DataContext = addImportConfigViewModel;
			this.InitializeComponent();
		}

		protected override void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (XBValidators.ValidateConfigName(this.ConfigNameTextBox.Text.Trim()))
			{
				this.WindowResult = MessageBoxResult.OK;
				base.Close();
			}
		}
	}
}
