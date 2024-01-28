using CV18.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV18.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _Title = "Main window";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
    }
}
