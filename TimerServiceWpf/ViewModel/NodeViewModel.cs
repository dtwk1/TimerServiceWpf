using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace TimerServiceWpf
{
    using System.Collections.ObjectModel;
    using System.Reactive.Linq;

    using DynamicData;

    using ReactiveUI;



    public class SubscriberViewModel
    {
        private ReadOnlyObservableCollection<object> _data;

        public ReadOnlyObservableCollection<object> Data => this._data;

        public SubscriberViewModel(IObservable<object> observable)
        {
            observable
                .ObserveOnDispatcher()
                
                .ToObservableChangeSet()
                .Sort(new Comparer())
                .Bind(out _data)
                .DisposeMany()
                .Subscribe();
        }

    }


    class Comparer : IComparer<object>
    {
        public int Compare(object x, object y)
        {
            return -Convert.ToInt32(x) + Convert.ToInt32(y);
        }
    }
    public class TimerViewModel : ReactiveObject
    {
        //public IObservable<DateTime> Dates { get;  }
        public TimerViewModel()
        {

            number = this.WhenAnyValue(x => x.Rate)
                .Select(x => x > 0 ? Observable.Interval(TimeSpan.FromSeconds(60d / x)) : Observable.Empty<long>())
                .Switch()
                 .ToProperty(this, x => x.Number);
        }

        private readonly ObservableAsPropertyHelper<long> number;
        public long Number
        {
            get { return number.Value; }
        }

        double rate = 60;
        public double Rate
        {
            get { return rate; }
            set { this.RaiseAndSetIfChanged(ref rate, value); }
        }


    }



    //class PublisherViewModel: ReactiveObject
    //{
    //    public PublisherViewModel()
    //    {
    //        HasMessage = this.WhenAny(x => x.Message)
    //            .ToProperty(this, x => x.HasMessage);
    //    }

    //    readonly ObservableAsPropertyHelper<Message> message;
    //    public Message Message
    //    {
    //        get { return message.Value; }
    //    }

    //}


    //public struct Message
    //{
    //    public DateTime DateTime { get; set; }

    //    public int Number { get; set; }
    //}
}
