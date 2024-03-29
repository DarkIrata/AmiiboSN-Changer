﻿using System;
using System.IO;
using System.Windows.Media.Imaging;
using ASNC.Data;

namespace ASNC.Services
{
    public class ServiceProvider
    {
        public Config Config { get; }

        public LibAmiibo.LibAmiibo LibAmiibo { get; private set; }

        public BitmapImage EmptyImage { get; private set; }

        public ServiceProvider(Config config)
        {
            this.Config = config;
            this.EmptyImage = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Empty_Light.png"));
            this.LibAmiibo = new LibAmiibo.LibAmiibo();
            this.ReinitializeLibAmiibo();
        }

        internal void ReinitializeLibAmiibo()
        {
            if (File.Exists(this.Config?.RetailKeyPath))
            {
                this.LibAmiibo = new LibAmiibo.LibAmiibo(this.Config!.RetailKeyPath);
            }
            else
            {
                this.LibAmiibo = new LibAmiibo.LibAmiibo();
            }

            this.LibAmiibo.RefreshInfoDataProvider();
        }
    }
}
