using Avalonia.Controls;
using Avalonia.ReactiveUI;
using AvaloniaApplication12.Models;
using AvaloniaApplication12.ViewModels;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace AvaloniaApplication12.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public static String mPath = AppDomain.CurrentDomain.BaseDirectory;
        public static String mDBPath = mPath + "Employee.db";

        private async Task DoShowDialogAsync(InteractionContext<AddPersonViewModel, Person?> interaction)
        {
            var dialog = new AddPerson();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Person?>(this);
            interaction.SetOutput(result);
        }
        private async Task DoShowDialogAsyncP(InteractionContext<AddProductViewModel, Product?> interaction)
        {
            var dialog = new AddProduct();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<Product?>(this);
            interaction.SetOutput(result);
        }
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
            this.WhenActivated(action =>
                action(ViewModel!.ShowDialogp.RegisterHandler(DoShowDialogAsyncP)));
        }

        
    }
}