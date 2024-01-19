using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace reSENSIUI.Controls.StickSensitivity
{
    public class StickSensitivitySelector : UserControl, IComponentConnector
    {
        private bool _contentLoaded;

        public StickSensitivitySelector()
        {
            InitializeComponent();
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent()
        {
            if (!_contentLoaded)
            {
                _contentLoaded = true;
                Uri resourceLocator = new Uri("/reSENSI;component/controls/sticksensitivity/sticksensitivityselector.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode]
        [GeneratedCode("PresentationBuildTasks", "7.0.5.0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            _contentLoaded = true;
        }
    }
}
