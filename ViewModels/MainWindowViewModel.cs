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

namespace CV18.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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
        }
    }
}
