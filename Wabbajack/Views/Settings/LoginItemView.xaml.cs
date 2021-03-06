﻿using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace Wabbajack
{
    public partial class LoginItemView
    {
        public LoginItemView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                this.OneWayBindStrict(ViewModel, x => x.Login.SiteName, x => x.SiteNameText.Text)
                    .DisposeWith(disposable);

                if (!ViewModel.UsesCredentials)
                {
                    this.OneWayBindStrict(ViewModel, x => x.Login.TriggerLogin, x => x.LoginButton.Command)
                        .DisposeWith(disposable);
                }
                else
                {
                    this.OneWayBindStrict(ViewModel, x => x.TriggerCredentialsLogin, x => x.LoginButton.Command)
                    .DisposeWith(disposable);
                }
                    
                this.OneWayBindStrict(ViewModel, x => x.Login.ClearLogin, x => x.LogoutButton.Command)
                    .DisposeWith(disposable);
                this.OneWayBindStrict(ViewModel, x => x.MetaInfo, x => x.MetaText.Text)
                    .DisposeWith(disposable);

                // Modify label state
                this.WhenAny(x => x.ViewModel.Login.TriggerLogin.CanExecute)
                    .Switch()
                    .Subscribe(x =>
                    {
                        LoginButton.Content = x ? "Login" : "Logged In";
                    });

                this.WhenAny(x => x.ViewModel.Login.ClearLogin.CanExecute)
                    .Switch()
                    .Subscribe(x =>
                    {
                        LogoutButton.Content = x ? "Logout" : "Logged Out";
                    });
            });
        }
    }
}
