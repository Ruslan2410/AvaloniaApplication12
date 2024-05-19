using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaApplication12.Models;
using AvaloniaApplication12.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace AvaloniaApplication12;
    
public partial class AddProduct : ReactiveWindow<AddProductViewModel>
{
    public AddProduct()
    {
        InitializeComponent();
        this.WhenActivated(OnActivated);
        var bCancel = this.FindControl<Button>("bCancel");
        bCancel.Click += BCancel_Click;
    }

    private void OnActivated(CompositeDisposable disposables)
    {
        ViewModel!.SaveProduct.Subscribe(Close).DisposeWith(disposables);
        if (ViewModel!.mode == "edit")
        {
            //ViewModel!.Name = ViewModel!.ProductItem.Name;
        }
    }
    private void BCancel_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close(); // Закрываем текущее окно
    }
}
