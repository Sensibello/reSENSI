﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;
using DiscSoft.NET.Common.Utils;
using DiscSoft.NET.Common.View.Controls.Buttons;
using DiscSoft.NET.Common.View.RecolorableImages;

namespace reSENSIUI.Views.SecondaryWindows.AddExternalDeviceWizard
{
	public class GimxStage1 : UserControl, IComponentConnector
	{
		public GimxStage1()
		{
			this.InitializeComponent();
		}

		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			DSUtils.GoUrl(e.Uri);
			e.Handled = true;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "7.0.5.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/reSENSI;component/views/secondarywindows/addexternaldevicewizard/addgimx/gimxstage1.xaml", UriKind.Relative);
			Application.LoadComponent(this, uri);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "7.0.5.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.WaitingForAdaptorTB = (TextBlock)target;
				return;
			case 2:
				this.WaitingConnectDescrTB = (TextBlock)target;
				return;
			case 3:
				this.WaitingConnectImg = (RecolorableSVG)target;
				return;
			case 4:
				this.BackButton = (ColoredButton)target;
				return;
			case 5:
				this.NextButton = (ColoredButton)target;
				return;
			case 6:
				this.CancelButton = (ColoredButton)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		internal TextBlock WaitingForAdaptorTB;

		internal TextBlock WaitingConnectDescrTB;

		internal RecolorableSVG WaitingConnectImg;

		internal ColoredButton BackButton;

		internal ColoredButton NextButton;

		internal ColoredButton CancelButton;

		private bool _contentLoaded;
	}
}
