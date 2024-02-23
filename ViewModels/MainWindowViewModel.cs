using CV18.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CV18.Infrastructure.Commands;
using System.Windows;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Series;
using OxyPlot.Axes;
using CV18.Data.HttpRequest;
using System.Globalization;
using CV18.Data.HttpRequest.Base;

namespace CV18.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private const string httpAdress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        #region Свойства окна
        #region Свойство названия окна
        private string _Title = "Main window";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion
        #region Свойство блока отображения подключения к интернету
        private string _Connect = "Готов!";
        public string Connect
        {
            get => _Connect;
            set => Set(ref _Connect, value);
        }
        #endregion
        #endregion
        #region коллекция точек для диаграммы
        private IEnumerable<DataPoint> _DataPoints;
        public IEnumerable<DataPoint> DataPoints
        {
            get => _DataPoints;
            set => Set(ref _DataPoints, value);
        }
        #endregion
        #region Команды
        #region Команда закрытия приложения
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new ActionCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            var plot = new PlotViewModel("Belarus");
            DataPoints = plot.CreatePlot();
        }
    }
}
