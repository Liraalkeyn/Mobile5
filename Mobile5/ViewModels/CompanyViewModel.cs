using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mobile5.Context;
using Mobile5.Models;

namespace Mobile5.ViewModels
{
    public class CompanyViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler? PropertyChanged; 

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Company> _Companies;
        public IEnumerable<Company> Companies 
        {
            get { return _Companies; }
            set { _Companies = value; OnPropertyChanged(); } 
        }

        private void GetCompany()
        {
            using (var context = new Rchtest1Context()) { Companies = context.Companies.ToList(); }
        }

        public CompanyViewModel() 
        {
            newCompany = new Company();
            GetCompany();
        }

        private Company _newCompany; 
        public Company newCompany 
        {
            get { return _newCompany; }
            set { _newCompany = value; OnPropertyChanged(); }
        }

        public DelegateCommand AddCompanyCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddCompany();
                });
            }
        }

        private void AddCompany()
        {
            using (var context = new Rchtest1Context())
            {
                context.Companies.Add(newCompany);
                context.SaveChanges();
                Companies = context.Companies.ToList();
            }
            newCompany = new Company();
        }

        private void DeleteCompany(int CompanyId) 
        {
            using (var context = new Rchtest1Context())
            {
                Company CompanyToDelete = context.Companies.Find(CompanyId);
                context.Companies.Remove(CompanyToDelete);
                context.SaveChanges();
                Companies = context.Companies.ToList();
            }
        }

        public DelegateCommand DeleteCompanyCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteCompany((int)o);
                });
            }
        }

        private Company _editCompany;
        public Company editCompany 
        {
            get { return _editCompany; }
            set { _editCompany = value; OnPropertyChanged(); }
        }
        private void UseCompany(int CompanyId)
        {
            using (var context = new Rchtest1Context())
            {
                editCompany = context.Companies.Find(CompanyId);
            }
        }

        public DelegateCommand UseCompanyCommand
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseCompany((int)o);
                });
            }
        }

        private void EditCompany(int CompanyId) 
        {
            using (var context = new Rchtest1Context())
            {
                context.Companies.Update(editCompany);
                context.SaveChanges();
                Companies = context.Companies.ToList();
            }
        }

        public DelegateCommand EditCompanyCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditCompany((int)o);
                });
            }
        }

    }
}
