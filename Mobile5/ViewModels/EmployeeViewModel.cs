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
    public class EmployeeViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler? PropertyChanged; 

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IEnumerable<Employee> _Employees; 
        public IEnumerable<Employee> Employees 
        {
            get { return _Employees; }
            set { _Employees = value; OnPropertyChanged(); } 
        }

        
        //Position list
        private List<Position> _positions;
        public List<Position> positions
        {
            get { return _positions; }
            set { _positions = value; OnPropertyChanged();}
        }

        private void GetEmployee() 
        {
            using (var context = new Rchtest1Context()) { Employees = context.Employees.ToList(); }
        }

        public EmployeeViewModel() 
        {
            newEmployee = new Employee();
            using (var context = new Rchtest1Context()) { positions = context.Positions.ToList(); }
            
            GetEmployee();
        }

        private Employee _newEmployee; 
        public Employee newEmployee 
        {
            get { return _newEmployee; }
            set { _newEmployee = value; OnPropertyChanged(); } 
        }

        public DelegateCommand AddEmployeeCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    AddEmployee();
                });
            }
        }

        private void AddEmployee() 
        {
            using (var context = new Rchtest1Context())
            {
                context.Employees.Add(newEmployee);
                context.SaveChanges();
                Employees = context.Employees.ToList();
            }
            newEmployee = new Employee();
        }

        private void DeleteEmployee(int EmployeeId) 
        {
            using (var context = new Rchtest1Context())
            {
                Employee EmployeeToDelete = context.Employees.Find(EmployeeId);
                context.Employees.Remove(EmployeeToDelete);
                context.SaveChanges();
                Employees = context.Employees.ToList();
            }
        }

        public DelegateCommand DeleteEmployeeCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    DeleteEmployee((int)o);
                });
            }
        }

        private Employee _editEmployee; 
        public Employee editEmployee 
        {
            get { return _editEmployee; }
            set { _editEmployee = value; OnPropertyChanged(); } 
        }
        private void UseEmployee(int EmployeeId)
        {
            using (var context = new Rchtest1Context())
            {
                editEmployee = context.Employees.Find(EmployeeId);
            }
        }

        public DelegateCommand UseEmployeeCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    UseEmployee((int)o);
                });
            }
        }

        private void EditEmployee(int EmployeeId) 
        {
            using (var context = new Rchtest1Context())
            {
                context.Employees.Update(editEmployee);
                context.SaveChanges();
                Employees = context.Employees.ToList();
            }
        }

        public DelegateCommand EditEmployeeCommand 
        {
            get
            {
                return new DelegateCommand(o =>
                {
                    EditEmployee((int)o);
                });
            }
        }

    }
}
