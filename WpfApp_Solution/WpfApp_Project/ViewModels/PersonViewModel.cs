using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfApp_Project.command;
using WpfApp_Project.Models;
using WpfApp_Project.Services;
using WpfApp_Project.ViewModels.baseModel;

namespace WpfApp_Project.ViewModels
{
    public class PersonViewModel : BaseViewModel
    {
        private PersonService _personService;
        private Person _person;
        private string _filterName;
        private string _filterCPF;

        public PersonViewModel()
        {
            _personService = new PersonService();
            Person = new Person();
            Persons = new ObservableCollection<Person>(_personService.LoadPersonFromXml());
            FiltredPersons = new ObservableCollection<Person>(Persons);


            SaveCommand = new RelayCommand(SavePersons);
            EditCommand = new RelayCommand(EditPerson);
            ExcludeCommand = new RelayCommand(ExcludePerson);
        }

        public Person Person
        {
            get { return _person; }
            set 
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Person> Persons { get; set; }
        public ObservableCollection<Person> FiltredPersons { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ExcludeCommand { get; }

        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public string FilterCPF
        {
            get { return _filterCPF; }
            set
            {
                _filterCPF = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private void SavePersons(object paramater)
        {

            Person.Id = _personService.GenerateLastId();

            Persons.Add(new Person { Id = Person.Id,  Name = Person.Name, CPF = Person.CPF, Address = Person.Address });

            _personService.SavePerson(new List<Person>(Persons));

            Person = new Person();
        }

        private void EditPerson(object person)
        {
            if(person is Person p)
            {
                _personService.PersonEdit(p);
                var index = Persons.IndexOf(p);
                if(index >= 0)
                {
                    Persons[index] = p;
                }
            }
        }

        private void ExcludePerson(object person)
        {
            if (person is Person p)
            {
                _personService.PersonExclude(p);
                Persons.Remove(p);
            }
        }


        private void ApplyFilter()
        {
            var filterName = FilterName ?? "";
            var filterCPF = FilterCPF ?? "";

            FiltredPersons = new ObservableCollection<Person>(
                Persons.Where(p => p.Name.ToLower().Contains(filterName.ToLower()) &&
                                   p.CPF.ToLower().Contains(filterCPF.ToLower()))
                );

            OnPropertyChanged(nameof(FiltredPersons));
        }
    }
}
