using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Text;
using Android.Text.Style;
using Android.Flexbox;

namespace MaterialChipView.SampleApp
{
    [Activity(Label = "MaterialChipView.SampleApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Chip _tenisChip;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var footballChip = FindViewById<Chip>(Resource.Id.football_chip);
            footballChip.Click += FootballChipOnClick;
            footballChip.IconClick += FootballChipOnIconClick;
            footballChip.Select += FootballChipOnSelect;


            var span = new SpannableString("My Football");
            span.SetSpan(new ForegroundColorSpan(Color.Red), 0, 2, 0);  // "My" is red
            span.SetSpan(new ForegroundColorSpan(Color.Blue), 3, 11, 0); // "Football" is blue
            footballChip.ChipSpanText = span;


            _tenisChip = FindViewById<Chip>(Resource.Id.tennis_chip);
            _tenisChip.Close += TenisChipOnClose;
        }

        private void TenisChipOnClose(object sender, EventArgs e)
        {
            var flexbox = FindViewById<FlexboxLayout>(Resource.Id.flexbox);
            flexbox.RemoveView(_tenisChip);
        }

        private void FootballChipOnSelect(object sender, EventArgs e)
        {
        }

        private void FootballChipOnIconClick(object sender, EventArgs e)
        {
        }

        private void FootballChipOnClick(object sender, EventArgs e)
        {
        }
    }
}

