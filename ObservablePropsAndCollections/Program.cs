using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

BindingList<string> list = new BindingList<string>();
list.Add("Hello");

ObservableCollection<string> collection = new ObservableCollection<string>();


public class Person : INotifyPropertyChanged
{
    private int _age;

    public int Age
    {
        get => _age;
        set
        {
            if (value == _age) return;
            _age = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}