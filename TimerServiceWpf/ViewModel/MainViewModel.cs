using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerServiceWpf
{
    using System.Collections.ObjectModel;

    using ReactiveUI;

    public class MainViewModel
    {
        public ObservableCollection<TimerViewModel> TimerViewModels { get; } =new ObservableCollection<TimerViewModel>();
        public ObservableCollection<SubscriberViewModel> SubscriberViewModels { get; } 
        public MainViewModel()
        {
            var tvm1 = new TimerViewModel { Rate = 60 };
            var tvm2 = new TimerViewModel { Rate = 30 };
            TimerViewModels.Add(tvm1);
            TimerViewModels.Add(tvm2);

            var svm1 = new SubscriberViewModel(tvm1.WhenAnyValue(_=>(object)_.Number));
            var svm2 = new SubscriberViewModel(tvm1.WhenAnyValue(_ => (object)_.Number));
            var svm3 = new SubscriberViewModel(tvm2.WhenAnyValue(_ => (object)_.Number));

            this.SubscriberViewModels=new ObservableCollection<SubscriberViewModel>(new [] { svm1,svm2,svm3});



        }
    }
}
