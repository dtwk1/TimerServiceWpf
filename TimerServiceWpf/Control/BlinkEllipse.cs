using System;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace TimerServiceWpf
{
    using System.Reactive.Linq;
    using System.Runtime.CompilerServices;

    using ReactiveUI;

    public class BlinkEllipse : Control
    {

        private Ellipse blinkEllipse;

        public static readonly DependencyProperty RateProperty = DependencyProperty.Register("Rate", typeof(double), typeof(BlinkEllipse), new PropertyMetadata(1d));

        public static readonly DependencyProperty IsBlinkingProperty = DependencyProperty.Register("IsBlinking", typeof(bool), typeof(BlinkEllipse), new PropertyMetadata(true));

        public double Rate
        {
            get { return (double)GetValue(RateProperty); }
            set { SetValue(RateProperty, value); }
        }
        public bool IsBlinking
        {
            get { return (bool)GetValue(IsBlinkingProperty); }
            set { SetValue(IsBlinkingProperty, value); }
        }


        static BlinkEllipse()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BlinkEllipse), new FrameworkPropertyMetadata(typeof(BlinkEllipse)));
        }
        public override void OnApplyTemplate()
        {
            blinkEllipse = this.GetTemplateChild(nameof(blinkEllipse)) as Ellipse;
            RateBlinkingChange(IsBlinking, Rate);
        }

        public BlinkEllipse()
        {

            this.WhenAnyValue(_ => _.IsBlinking)
                .CombineLatest(this.WhenAnyValue(_ => _.Rate).Throttle(TimeSpan.FromSeconds(1)), RateBlinkingChange)
                .SubscribeOnDispatcher()
                .Subscribe(_ => { });
        }

        private object RateBlinkingChange(bool isBlinking, double rate)
        {
            Storyboard sb = (Storyboard)blinkEllipse?.FindResource("BlinkStoryboard");
            if (sb != null & rate > 0)
                this.Dispatcher.Invoke(() =>
            {
                sb.RepeatBehavior = RepeatBehavior.Forever; ;
                sb.Duration = TimeSpan.FromSeconds(1d / rate);
                sb.Stop();
                if (isBlinking)
                    sb.Begin();

            });
            return sb;
        }



    }
}


