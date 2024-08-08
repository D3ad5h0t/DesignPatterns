using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;


var product = new Product { Name = "Book" };
var window = new Window { ProductName = "Book" };

using var binding = new BidirectionalBinding(
    product,
    () => product.Name,
    window,
    () => window.ProductName
    );
// product.PropertyChanged += (sender, eventArgs) =>
// {
//     if (eventArgs.PropertyName == "Name")
//     {
//         Console.WriteLine("Name changed in product");
//         window.ProductName = product.Name;
//     }
// };
//
// window.PropertyChanged += (sender, eventArgs) =>
// {
//     if (eventArgs.PropertyName == "ProductName")
//     {
//         Console.WriteLine("Name changed in window");
//         product.Name = window.ProductName;
//     }
// };

product.Name = "Table";
Console.WriteLine(product);
Console.WriteLine(window);


public class BidirectionalBinding : IDisposable
{
    private bool _disposed;

    public BidirectionalBinding(
        INotifyPropertyChanged first,
        Expression<Func<object>> firstProperty,
        INotifyPropertyChanged second,
        Expression<Func<object>> secondProperty)
    {
        if (firstProperty.Body is MemberExpression firstExpr && secondProperty.Body is MemberExpression secondExpr)
        {
            if (firstExpr.Member is PropertyInfo firstProp && secondExpr.Member is PropertyInfo secondProp)
            {
                first.PropertyChanged += (sender, args) =>
                {
                    if (!_disposed)
                    {
                        secondProp.SetValue(second, firstProp.GetValue(first));
                    }
                };

                second.PropertyChanged += (sender, args) =>
                {
                    if (!_disposed)
                    {
                        firstProp.SetValue(first, secondProp.GetValue(second));
                    }
                };
            }
        }
    }
    
    public void Dispose()
    {
        _disposed = true;
    }
}

public class Product : INotifyPropertyChanged
{
    private string _name;
    
    public string Name
    {
        get => _name;
        set
        {
            if (value == _name)
            {
                return;
            }

            _name = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
        return $"Product: {Name}";
    }
}

public class Window : INotifyPropertyChanged
{
    private string _productName;
    
    public string ProductName
    {
        get => _productName;
        set
        {
            if (value == _productName)
            {
                return;
            }

            _productName = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
        return $"Window: {ProductName}";
    }
}