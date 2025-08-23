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

        private ObservableCollection<Person> _persons { get; set; }

        public ObservableCollection<Person> Persons
        {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
                OnPropertyChanged(nameof(Persons));
            }
        }

        private ObservableCollection<Person> _filtredPersons { get; set; }


        public ObservableCollection<Person> FiltredPersons
        {
            get
            {
                return _filtredPersons;
            }
            set
            {
                _filtredPersons = value;
                OnPropertyChanged(nameof(FiltredPersons));
            }
        }

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
                OnPropertyChanged(nameof(Person));
            }
        }

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
            ApplyFilter();

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

                ApplyFilter();
            }
        }

        private void ExcludePerson(object person)
        {
            if (person is Person p)
            {
                _personService.PersonExclude(p);
                Persons.Remove(p);

                ApplyFilter();
            }
        }


        private void ApplyFilter()
        {
            var filterName = FilterName ?? "";
            var filterCPF = FilterCPF ?? "";

            var filteredList = Persons.Where(p => (p.Name != null && p.Name.ToLower().Contains(filterName.ToLower())) &&
                                                   (p.CPF != null && p.CPF.ToLower().Contains(filterCPF.ToLower()))).ToList();

            FiltredPersons.Clear();
            foreach (var person in filteredList)
            {
                FiltredPersons.Add(person);
            }

            OnPropertyChanged(nameof(FiltredPersons));
        }
    }
}
