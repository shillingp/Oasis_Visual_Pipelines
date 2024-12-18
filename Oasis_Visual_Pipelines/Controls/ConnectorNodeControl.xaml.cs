﻿using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Oasis_Visual_Pipelines.Controls
{
    /// <summary>
    /// Interaction logic for ConnectorNodeControl.xaml
    /// </summary>
    public partial class ConnectorNodeControl : UserControl
    {
        public static int ConnectorNodeSize = 10;

        public ConnectorNodeControl()
        {
            InitializeComponent();
        }

        #region Dependancy Properties
        public Connection Connection
        {
            get { return (Connection)GetValue(ConnectionProperty); }
            set { SetValue(ConnectionProperty, value); }
        }

        public static readonly DependencyProperty ConnectionProperty =
            DependencyProperty.Register(
                nameof(Connection),
                typeof(Connection),
                typeof(ConnectorNodeControl),
                new PropertyMetadata(null));

        public ConnectionSide ConnectionSide
        {
            get { return (ConnectionSide)GetValue(ConnectionSideProperty); }
            set { SetValue(ConnectionSideProperty, value); }
        }

        public static readonly DependencyProperty ConnectionSideProperty =
            DependencyProperty.Register(
                nameof(ConnectionSide),
                typeof(ConnectionSide),
                typeof(ConnectorNodeControl),
                new PropertyMetadata(null));
        #endregion

        #region Events
        public event DragStartedEventHandler DragStarted = (_, _) => { };
        private void ConnectorThumb_DragStarted(object sender, DragStartedEventArgs e) => DragStarted?.Invoke(this, e);

        public event DragDeltaEventHandler DragDelta = (_, _) => { };
        private void ConnectorThumb_DragDelta(object sender, DragDeltaEventArgs e) => DragDelta?.Invoke(this, e);

        public event DragCompletedEventHandler DragCompleted = (_, _) => { };
        private void ConnectorThumb_DragCompleted(object sender, DragCompletedEventArgs e) => DragCompleted?.Invoke(this, e);
        #endregion
    }
}
