﻿namespace Realtime_D3.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "BootstrapBlazor Maui App" };
        }
    }
}
