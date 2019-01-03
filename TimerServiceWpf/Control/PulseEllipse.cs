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

    public class PulseEllipse : Control
    {

        private Ellipse blinkEllipse;

      
        public static readonly DependencyProperty PulseProperty = DependencyProperty.Register("Pulse", typeof(object), typeof(PulseEllipse), new PropertyMetadata(true));

        public object Pulse
        {
            get { return (object)GetValue(PulseProperty); }
            set { SetValue(PulseProperty, value); }
        }


        static PulseEllipse()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PulseEllipse), new FrameworkPropertyMetadata(typeof(PulseEllipse)));
        }
        public override void OnApplyTemplate()
        {
            //ResourceDictionary myDictionary = Application.LoadComponent(new Uri("/TimerServiceWpf;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;
            blinkEllipse = this.GetTemplateChild(nameof(blinkEllipse)) as Ellipse;
        }

        public PulseEllipse()
        {
            this.WhenAnyValue(_ => _.Pulse)
                .Where(_=>
                    this.blinkEllipse!=null)
                .Subscribe(_ => RateBlinkingChange());
        }

        private Storyboard sb;
        private object RateBlinkingChange()
        {
            blinkEllipse.Fill = Brushes.WhiteSmoke;
            sb = sb ?? getStoryboard();
             if (sb != null)
                this.Dispatcher.Invoke(() =>sb.Begin());
            return sb;
        }

        private Storyboard getStoryboard()
        {
            var sb = (Storyboard)blinkEllipse?.FindResource("BlinkStoryboard");
            if (sb != null)
            {
                sb.Duration = TimeSpan.FromMilliseconds(1000);
                return sb;
            }

            return null;
        }

    }
}


