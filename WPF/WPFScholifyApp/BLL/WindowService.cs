// <copyright file="WindowService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System;
    using System.Windows;
    using Microsoft.Extensions.DependencyInjection;

    public interface IWindowService
    {
        public void Show<TWindow>(Action<TWindow>? configure = null)
            where TWindow : Window;
    }

    public class WindowService : IWindowService
    {
        private readonly IServiceProvider services;

        public WindowService(IServiceProvider services)
        {
            this.services = services;
        }

        public void Show<TWindow>(Action<TWindow>? configure = null)
            where TWindow : Window
        {
            var window = this.services.GetService<TWindow>();
            if (configure != null)
            {
                configure(window!);
            }

            window!.Show();
        }
    }
}
