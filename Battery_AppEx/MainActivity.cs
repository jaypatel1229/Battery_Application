using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace Battery_AppEx
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    class MainActivity : Activity
    {
        private Button batteryState;
        private Button batterypowerSource;
        private TextView textV1;
        private TextView textV2;
        private TextView textV3;
        private TextView textV4;
        private TextView textV5;
        private TextView textV6;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            UIReference();
            UIClickEVents();
        }

        private void UIClickEVents()
        {
            batteryState.Click += BatteryState_Click;
            batterypowerSource.Click += BatterypowerSource_Click;
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            Battery.EnergySaverStatusChanged += Battery_EnergySaverStatusChanged;

        }

        private void Battery_EnergySaverStatusChanged(object sender, EnergySaverStatusChangedEventArgs e)
        {
            var status = e.EnergySaverStatus;
            Toast.MakeText(this, "Energy Saver: " + status + ".", ToastLength.Short).Show();
        }

        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            var level = e.ChargeLevel;
            var state = e.State;
            var source = e.PowerSource;
            Toast.MakeText(this, "Level: " + level + ", State: " + state + ", Source: " + source + ".", ToastLength.Short).Show();
        }

        private void BatterypowerSource_Click(object sender, EventArgs e)
        {
            GetBatterySource();
        }

        private void GetBatterySource()
        {
            var powerSource = Battery.PowerSource;
            switch (powerSource)
            {
                case BatteryPowerSource.Battery:
                    textV2.Text = $"Being Powered by Battery";
                    break;

                case BatteryPowerSource.Usb:
                    textV2.Text = $"Battery Powered by USB";
                    break;

                case BatteryPowerSource.AC:
                    textV2.Text = $"Batter Powered by A/C unit";
                    break;

                case BatteryPowerSource.Wireless:
                    textV2.Text = $"Powered via Wireless charging";
                    break;

                case BatteryPowerSource.Unknown:
                    textV2.Text = $"Unable to detect Power source";
                    break;

            }

        }

        private void BatteryState_Click(object sender, EventArgs e)
        {
            GetBatteryState();
        }

        private void GetBatteryState()
        {
            var state = Battery.State;
            switch (state)
            {
                case BatteryState.Charging:
                    textV1.Text = $"Currently Charging";
                    break;

                case BatteryState.Full:
                    textV1.Text = $"Battery is full";
                    break;

                case BatteryState.Discharging:
                case BatteryState.NotCharging:
                    textV1.Text = $"CurrentlyDischarging Battery or not being Charged";
                    break;

                case BatteryState.NotPresent:
                    textV1.Text = $"Battery doesnt exist in Device";
                    break;

                case BatteryState.Unknown:
                    textV1.Text = $"Battery Unable to detect battery State";
                    break;

            }

        }

        private void UIReference()
        {
            batteryState = FindViewById<Button>(Resource.Id.bState);
            batterypowerSource = FindViewById<Button>(Resource.Id.bSource);
            textV1 = FindViewById<TextView>(Resource.Id.text1);
            textV2 = FindViewById<TextView>(Resource.Id.text2);

        }
    }
}