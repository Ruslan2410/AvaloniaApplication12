using AvaloniaApplication12.Models;
using AvaloniaApplication12.ViewModels;
using DynamicData;
using ReactiveUI;
using System;
using System.Data.Entity;
using System.Reactive.Linq;
using System.Windows.Input;

namespace AvaloniaApplication12.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public People ThePeople { get; } = new People();
        public Interaction<AddPersonViewModel, Person?> ShowDialog { get; }
        public ICommand AddPersonCommand { get; }
        public ICommand EditPersonCommand { get; }
        public ICommand DeletePersonCommand { get; }
        public ICommand SelectRowCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        
        //public ICommand SelectRow {  get; }
        public Product_Fill TheProduct { get; } = new Product_Fill();
        public Interaction<AddProductViewModel, Product?> ShowDialogp { get; }

        public Log_Fill TheLog { get; } = new Log_Fill();
        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<AddPersonViewModel, Person?>();
            ShowDialogp = new Interaction<AddProductViewModel, Product?>();

            AddPersonCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var person = new AddPersonViewModel();
                person.mode = "insert";
                var result = await ShowDialog.Handle(person);
                if (result != null)
                {
                    string sql = string.Format("Insert into Person (DepartmentNumber, DeskLocation, FirstName, LastName) " +
                            "Values ('{0}', '{1}', '{2}', '{3}');", 
                            result.DepartmentNumber, result.DeskLocation, result.FirstName, result.LastName);
                    string logSql = string.Format("INSERT INTO Logs (Action, Timestamp) VALUES ('Added person: {0} {1}', '{2}');",
                            result.FirstName, result.LastName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    Database.Exec_SQL(sql);
                    Database.Exec_SQL(logSql);
                    ThePeople.FillPeople();
                    TheLog.Fill_Logs();
                }
            });

            AddProductCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var product = new AddProductViewModel();
                product.mode = "insert";
                var result = await ShowDialogp.Handle(product);
                if (result != null)
                {
                    string insertProductSql = string.Format("INSERT INTO Products (Name, Category, Price, Quantity) " +
                        "VALUES ('{0}', '{1}', '{2}', '{3}');",
                        result.Name, result.Category, result.Price, result.Quantity);

                    // Логирование операции добавления продукта
                    string logSql = string.Format("INSERT INTO Logs (Action, Timestamp) VALUES ('Added product: {0}', '{1}');",
                        result.Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    Database.Exec_SQL(insertProductSql);
                    Database.Exec_SQL(logSql);
                    TheProduct.Fill_Product();
                    TheLog.Fill_Logs();
                }
            });


            EditPersonCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var person = new AddPersonViewModel();
                person.mode = "edit";
                person.PersonItem = SelectItem;
                var result = await ShowDialog.Handle(person);
                if (result != null)
                {
                    string sql = string.Format("Update Person SET DepartmentNumber = '{0}', " +
                        "DeskLocation = '{1}', " +
                        "FirstName = '{2}', " +
                        "LastName = '{3}' " +
                        "Where EmployeeNumber = '{4}';",
                        result.DepartmentNumber, result.DeskLocation, result.FirstName, result.LastName, SelectItem.EmployeeNumber);
                    Database.Exec_SQL(sql);
                    ThePeople.FillPeople();
                    TheLog.Fill_Logs();
                }
            });

            EditProductCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var product = new AddProductViewModel();
                product.mode = "edit";
                product.ProductItem = SelectItemp;
                var result = await ShowDialogp.Handle(product);
                if (result != null)
                {
                    string updateProductSql = string.Format("UPDATE Products SET " +
                        "Name = '{0}', " +
                        "Category = '{1}', " +
                        "Price = '{2}', " +
                        "Quantity = '{3}' " +
                        "WHERE ProductID = '{4}';",
                        result.Name, result.Category, result.Price, result.Quantity, SelectItemp.ProductID);

                    // Логирование операции редактирования продукта
                    string logSql = string.Format("INSERT INTO Logs (Action, Timestamp) VALUES ('Edited product: {0}', '{1}');",
                        result.Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    Database.Exec_SQL(updateProductSql);
                    Database.Exec_SQL(logSql);
                    TheProduct.Fill_Product();
                    TheLog.Fill_Logs();
                }
            });

            DeletePersonCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (SelectItem != null)
                {
                    string sql = string.Format("Delete from Person Where EmployeeNumber = '{0}';", SelectItem.EmployeeNumber);
                    Database.Exec_SQL(sql);
                    ThePeople.FillPeople();
                    TheLog.Fill_Logs();
                }
            });

            DeleteProductCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                if (SelectItemp != null)
                {
                    string deleteProductSql = string.Format("DELETE FROM Products WHERE Name = '{0}';", SelectItemp.Name);

                    // Логирование операции удаления продукта
                    string logSql = string.Format("INSERT INTO Logs (Action, Timestamp) VALUES ('Deleted product: {0}', '{1}');",
                        SelectItemp.Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    Database.Exec_SQL(deleteProductSql);
                    Database.Exec_SQL(logSql);
                    TheProduct.Fill_Product();
                    TheLog.Fill_Logs();
                }
            });

            SelectRowCommand = ReactiveCommand.Create<Person>(person =>
            {
                SelectItem = person;
            });

            SelectRowCommand = ReactiveCommand.Create<Product>(product =>
            {
                SelectItemp = product;
            });

            //SelectRow = ReactiveCommand.Create(SelectionChanged);
        }

        Person _selectItem = null;
        Product _selectItemp = null;
        public Person SelectItem
        {
            get { return _selectItem; }
            set
            {
                if (_selectItem != value)
                {
                    this.RaiseAndSetIfChanged(ref _selectItem, value);
                }
            }
        }
        public Product SelectItemp
        {
            get { return _selectItemp; }
            set
            {
                if (_selectItemp != value)
                {
                    this.RaiseAndSetIfChanged(ref _selectItemp, value);
                }
            }
        }

        //private void SelectionChanged()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
