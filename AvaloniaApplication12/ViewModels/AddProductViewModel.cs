﻿using AvaloniaApplication12.Models;
using AvaloniaApplication12.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication12.ViewModels
{
       public class AddProductViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Product?> SaveProduct { get; }
        public Product ProductItem;
        public string mode = "insert";

        public AddProductViewModel()
        {
            SaveProduct = ReactiveCommand.Create(() =>
            {
                ProductItem = new Product();
                ProductItem.Name = Name;
                ProductItem.Category = Category;
                ProductItem.Price = Price;
                ProductItem.Quantity = Quantity;
                return ProductItem;
            });
        }
        

        string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    this.RaiseAndSetIfChanged(ref _name, value);
                }
            }
        }

        string _category = "";
        public string Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    this.RaiseAndSetIfChanged(ref _category, value);
                }
            }
        }

        decimal _price = 0;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    this.RaiseAndSetIfChanged(ref _price, value);
                }
            }
        }

        int _quantity = 0;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    this.RaiseAndSetIfChanged(ref _quantity, value);
                }
            }
        }

    }
    
}
