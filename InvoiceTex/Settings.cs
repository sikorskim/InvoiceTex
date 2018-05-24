using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InvoiceTex
{
    class Settings
    {
        public Company Company { get; set; }
        public List<Contractor> Contractors { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public List<UnitOfMeasure> UnitOfMeasures { get; set; }
        public List<Item> Items { get; set; }
        public string Path { get; set; }

        public Settings(string path)
        {
            Path = path;
            Contractors = new List<Contractor>();
            PaymentMethods = new List<PaymentMethod>();
            UnitOfMeasures = new List<UnitOfMeasure>();
            Items = new List<Item>();
            Company = new Company();
        }

        public bool importFromXML()
        {
            //try
            //{
                XDocument doc = XDocument.Load(Path);
                XElement elemCompany = doc.Element("Settings").Element("Company");
                XElement elemContractors = doc.Element("Settings").Element("Contractors");
                XElement elemPaymentMethods = doc.Element("Settings").Element("PaymentMethods");
                XElement elemUnitOfMeasures = doc.Element("Settings").Element("UnitOfMeasures");

                Company.Id = Int32.Parse(elemCompany.Attribute("Id").Value);
                Company.FullName = elemCompany.Attribute("FullName").Value;
                Company.Name=elemCompany.Attribute("Name").Value;
                Company.NIP= elemCompany.Attribute("NIP").Value;
                Company.REGON=elemCompany.Attribute("REGON").Value;
                Company.Street= elemCompany.Attribute("Street").Value;
                Company.BuldingNo= elemCompany.Attribute("BuldingNo").Value;
                Company.PostalCode= elemCompany.Attribute("PostalCode").Value;
                Company.City=elemCompany.Attribute("City").Value;
                Company.Email = elemCompany.Attribute("Email").Value;
                Company.Phone = elemCompany.Attribute("Phone").Value;
                Company.Website= elemCompany.Attribute("Website").Value;
                Company.BankName = elemCompany.Attribute("BankName").Value;
                Company.BankAccount = elemCompany.Attribute("BankAccount").Value;
                Company.IssuerName = elemCompany.Attribute("IssuerName").Value;


                foreach (XElement xContractor in elemContractors.Elements("Contractor"))
                {
                    Contractor contractor = new Contractor();
                    contractor.Id = Int32.Parse(elemCompany.Attribute("Id").Value);
                    contractor.FullName = elemCompany.Attribute("FullName").Value;
                    contractor.NIP = elemCompany.Attribute("NIP").Value;
                    contractor.Street = elemCompany.Attribute("Street").Value;
                    contractor.BuldingNo = elemCompany.Attribute("BuldingNo").Value;
                    contractor.PostalCode = elemCompany.Attribute("PostalCode").Value;
                    contractor.City = elemCompany.Attribute("City").Value;
                    Contractors.Add(contractor);
                }

                foreach (XElement xPaymentMethod in elemPaymentMethods.Elements("PaymentMethod"))
                {
                    PaymentMethod paymentMethod = new PaymentMethod();
                    paymentMethod.Id=Int32.Parse(xPaymentMethod.Attribute("Id").Value);
                    paymentMethod.Name = xPaymentMethod.Attribute("Name").Value;
                    paymentMethod.DueDate = Int32.Parse(xPaymentMethod.Attribute("DueDate").Value);
                    PaymentMethods.Add(paymentMethod);
                }

                foreach (XElement xUnitOfMeasures in elemUnitOfMeasures.Elements("UnitOfMeasure"))
                {
                    UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                    unitOfMeasure.Id=Int32.Parse(xUnitOfMeasures.Attribute("Id").Value);
                    unitOfMeasure.Name = xUnitOfMeasures.Attribute("Name").Value;
                    unitOfMeasure.ShortName = xUnitOfMeasures.Attribute("ShortName").Value;
                    UnitOfMeasures.Add(unitOfMeasure);
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}

            return true;
        }
    }
}
