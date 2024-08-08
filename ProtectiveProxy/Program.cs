ICar car = new CarProxy(new Car(), new Driver(12));
car.Drive();

public interface ICar
{
    void Drive();
}

public class Car : ICar
{
    public void Drive()
    {
        Console.WriteLine("Car being driven");
    }
}

public class Driver
{
    public int Age;

    public Driver(int age)
    {
        Age = age;
    }
}

public class CarProxy : ICar
{
    private Car _car;
    private Driver _driver;

    public CarProxy(Car car, Driver driver)
    {
        _car = car;
        _driver = driver;
    }

    public void Drive()
    {
        if (_driver.Age >= 16)
        {
            _car.Drive();
        }
        else
        {
            Console.WriteLine("Driver too young");
        }
    }
}