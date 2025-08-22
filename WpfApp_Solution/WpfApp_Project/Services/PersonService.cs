using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WpfApp_Project.Models;

namespace WpfApp_Project.Services
{
    public class PersonService
    {
        public List<Person> persons = new List<Person>();

        private const string caminhoXml = @"..\..\Data\Persons.xml";


        public void SavePerson(List<Person> persons)
        {

            XDocument doc = new XDocument(new XElement("Persons", 
                persons.ConvertAll(p => new XElement("Person",
                    new XElement("Id", p.Id),
                    new XElement("Name", p.Name),
                    new XElement("CPF", p.CPF),
                    new XElement("Address", p.Address)))));
             
            doc.Save(caminhoXml);
        }


        public List<Person> LoadPersonFromXml()
        {

            XDocument doc = XDocument.Load(caminhoXml);

            if (doc.Descendants("Person").Any())
            {
                persons = (from p in doc.Descendants("Person")
                           select new Person
                           {
                               Id = (int)p.Element("Id"),
                               Name = (string)p.Element("Name"),
                               CPF = (string)p.Element("CPF"),
                               Address = (string)p.Element("Address")
                           }).ToList();
            }
            else
            {
                persons = new List<Person>();
            }


            return persons;
        }

        public void PersonEdit(Person editPerson)
        {
            List<Person> persons = LoadPersonFromXml();

            var person = persons.FirstOrDefault(p => p.Id == editPerson.Id);

            if(person != null)
            {
                person.Name = editPerson.Name;
                person.CPF = editPerson.CPF;
                person.Address = editPerson.Address;

                SavePerson(persons);
            }
        }

        public void PersonExclude(Person excludePerson)
        {
            List<Person> persons = LoadPersonFromXml();

            var person = persons.FirstOrDefault(p => p.Id == excludePerson.Id);

            if (person != null)
            {
               persons.Remove(person);

                SavePerson(persons);
            }
        }

        public int GenerateLastId()
        {
            List<Person> persons = LoadPersonFromXml();
            int proximoId = persons.Count > 0 ? persons.Max(p => p.Id) + 1 : 1;
            return proximoId;
        }
    }
}
