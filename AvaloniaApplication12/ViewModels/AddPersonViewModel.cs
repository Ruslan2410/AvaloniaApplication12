using AvaloniaApplication12.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication12.ViewModels
{
    public class AddPersonViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Person?> SavePerson { get; }
        public Person PersonItem;
        public string mode = "insert";

        public AddPersonViewModel()
        {
            SavePerson = ReactiveCommand.Create(() =>
            {
                PersonItem = new Person();
                PersonItem.FirstName = FirstName;
                PersonItem.LastName = LastName;
                PersonItem.DepartmentNumber = DepartmentNumber;
                PersonItem.DeskLocation = DeskLocation;
                return PersonItem;
            });
        }

        string _firstName= "";
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    this.RaiseAndSetIfChanged(ref _firstName, value);
                }
            }
        }

        string _lastName = "";
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    this.RaiseAndSetIfChanged(ref _lastName, value);
                }
            }
        }

        int _departmentNumber = 0;
        public int DepartmentNumber
        {
            get { return _departmentNumber; }
            set
            {
                if (_departmentNumber != value)
                {
                    this.RaiseAndSetIfChanged(ref _departmentNumber, value);
                }
            }
        }

        string _deskLocation = "";
        public string DeskLocation
        {
            get { return _deskLocation; }
            set
            {
                if (_deskLocation != value)
                {
                    this.RaiseAndSetIfChanged(ref _deskLocation, value);
                }
            }
        }

    }
}
