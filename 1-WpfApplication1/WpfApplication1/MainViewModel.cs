using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace WpfApplication1
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }
        #endregion
    }

    public class DelegateCommand : ICommand
    {
        public delegate void ICommandOnExecute(object parameter);
        public delegate bool ICommandOnCanExecute(object parameter);

        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        public DelegateCommand(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        #endregion
    }

    public class MainViewModel : ViewModelBase
    {
        private DelegateCommand exitCommand;

        #region Constructor

        public StudentsModel Students { get; set; }
        public string StudentNameToAdd { get; set; }
        public int StudentScoreToAdd { get; set; }


        public MainViewModel()
        {
            Students = StudentsModel.Current;
        }

        #endregion

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(Exit, CanExecuteCommand1);
                }
                return exitCommand;
            }
        }

        private void Exit(object parameter)
        {
            Application.Current.Shutdown();
        }


        public bool CanExecuteCommand1(object parameter)
        {
            return true;
        }

        private ICommand _AddStudent;
        public ICommand AddStudent
        {
            get
            {
                if (_AddStudent == null)
                {
                    _AddStudent = new DelegateCommand(ExecuteCommand1, CanExecuteCommand1);

                }

                return _AddStudent;
            }
        }

        public void ExecuteCommand1(object parameter)
        {
            StudentNameToAdd.Trim();

            StringBuilder SB = new StringBuilder();
            if (StudentNameToAdd == "")
            {
                SB.Remove(0, SB.Length);
                SB.Append("Please type in a name for the student.");
                throw new ArgumentException(SB.ToString());
            }

            if (StudentNameToAdd.Length < 10)
            {
                SB.Remove(0, SB.Length);
                SB.Append("We only take students whose name is longer than ");
                SB.Append("10 characters.");
                throw new ArgumentException(SB.ToString());
            }
            if ((StudentScoreToAdd < 60) || (StudentScoreToAdd > 100))
            {
                SB.Remove(0, SB.Length);
                SB.Append("We only take students " +
                          "whose score is between 60 and 100. ");
                SB.Append("Please give a valid score");
                throw new ArgumentException(SB.ToString());
            }

            DateTime Now = DateTime.Now;
            SB.Remove(0, SB.Length);
            SB.Append("Student ");
            SB.Append(StudentNameToAdd);
            SB.Append(" is added @ ");
            SB.Append(Now.ToString());

            Students.AddAStudent(StudentNameToAdd,
                StudentScoreToAdd, Now, SB.ToString());
        }        
    }
}
