
PassCar passcar1 = new PassCar(9.2, 60, 40, 90, 2);  //Расход, Объем бака, Наличие топлива, Скорость,(Необзяательно: Наличие пассажиров).
Console.WriteLine($"Тип транспортного средства: {passcar1.Type};\n\nТекущая скорость: {passcar1.Speed} км/ч;\nРасход: {passcar1.AvgCons} л/100км;\n\n" +
                  $"Объем полного бака: {passcar1.FullTank} литров;\nСейчас в баке: {passcar1.NowTank} литров;\n\nПассажиров: {passcar1.PassCount};");

Console.WriteLine($"Топлива хватит на " + string.Format("{0:0}", passcar1.Rest(passcar1)) + " км;"); 
Console.WriteLine($"Время до цели " + string.Format("{0:0}", passcar1.Distance(passcar1, 100)) + " минут.\n\n");


Truck truck1 = new Truck(28.7, 240, 200, 70, 2000, 1000);  //Расход, Объем бака, Наличие топлива, Скорость, Грузоподъемность,(Необязательно: Текущий груз).
Console.WriteLine($"Тип транспортного средства: {truck1.Type};\n\nТекущая скорость: {truck1.Speed} км/ч;\nРасход: {truck1.AvgCons} л/100км;\n\n" +
                  $"Объем полного бака: {truck1.FullTank} литров;\nСейчас в баке: {truck1.NowTank} литров;\n\nГрузоподъемность: {truck1.Weight} кг;\n" +
                  $"Вес текущего груза: {truck1.CurrentWeight} кг;\n");

Console.WriteLine($"Топлива хватит на " + string.Format("{0:0}", truck1.Rest(truck1)) +" км;");
Console.WriteLine($"Время до цели "+ string.Format("{0:0}", truck1.Distance(truck1, 100)) +" минут.\n\n");


SportCar sportc1 = new SportCar(18.6, 70, 65, 120);  //Расход, Объем бака, Наличие топлива, Скорость, (Необязательно: Наличие пассажиров).
Console.WriteLine($"Тип транспортного средства: {sportc1.Type};\n\nТекущая скорость: {sportc1.Speed} км/ч;\nРасход: {sportc1.AvgCons} л/100км;\n\n" +
                  $"Объем полного бака: {sportc1.FullTank} литров;\nСейчас в баке: {sportc1.NowTank} литров;\n\nПассажиров: {sportc1.PassCount};");

Console.WriteLine($"Топлива хватит на " + string.Format("{0:0}", sportc1.Rest(sportc1)) + " км;");
Console.WriteLine($"Время до цели " + string.Format("{0:0}", sportc1.Distance(sportc1, 100)) + " минут.\n\n");


public abstract class Auto
{
    public string Type { get; set; }      //Тип ТС
    public double AvgCons { get; set; }   //Средний расход
    public double FullTank { get; set; }  //Объем полного бака
    public double NowTank { get; set; }   //Топлива в баке на данный момент
    public double Speed { get; set; }     //Скорость автомобиля (в данном случае текущая) 

    public Auto(string type, double avgCons, double fullTank, double nowTank, double speed)  //конструктор абстр класса
    {
        Type = type;
        AvgCons = avgCons;
        FullTank = fullTank;
        NowTank = nowTank;
        Speed = speed;
    }

    public abstract double Rest(Auto car); //Остаток хода на полном или текущем баке в (км).

    public double Distance(Auto car, double dist)  //Время за которое авто пройдет указанное расстояние(с проверкой)
    {
        if (dist <= car.Rest(this))
        {
            return dist / Speed*60;
        }
        else return 0;
    }
}


public class PassCar : Auto  //Класс легкового (пассажирского) авто
{
    public int PassCount { get; set; }  //Количество пассажиров

    public PassCar(double avgCons, double fullTank, double nowTank, double speed, int passCount=0, string type = "Легковой")
           : base(type, avgCons, fullTank, nowTank, speed)
    {
        PassCount = passCount;
    }
    public override double Rest(Auto car) //Переопределяем метод "Остаток хода в км" для легкового авто ("с" или "без" учета пассажиров)
    {
        if (PassCount >= 0 && PassCount <= 4 && NowTank<=FullTank)
        {
            if (NowTank == FullTank)
            {
                switch (PassCount)
                {
                    case 0: return FullTank * 100 / AvgCons;         //присутствует только водитель          
                    case 1: return FullTank * 100 / AvgCons * 0.94;  //присутствует водитель и 1 пассажир 
                    case 2: return FullTank * 100 / AvgCons * 0.88;
                    case 3: return FullTank * 100 / AvgCons * 0.82;
                    case 4: return FullTank * 100 / AvgCons * 0.76; //присутствует водитель и 4 (максимум) пассажира
                    default: return 0;
                }
            }
            else
            {
                switch (PassCount)
                {
                    case 0: return NowTank * 100 / AvgCons;         //присутствует только водитель
                    case 1: return NowTank * 100 / AvgCons * 0.94;  //присутствует водитель и 1 пассажир 
                    case 2: return NowTank * 100 / AvgCons * 0.88;
                    case 3: return NowTank * 100 / AvgCons * 0.82;
                    case 4: return NowTank * 100 / AvgCons  * 0.76; //присутствует водитель и 4 (максимум) пассажира  
                    default: return 0;
                }
            }
        }
        else return 0;
    }
}

public class Truck : Auto  //Класс грузового авто
{
    public int Weight { get; set; }  // Общая грузоподъемность ТС
    public int CurrentWeight { get; set; } // Вес груза на данный момент

    public Truck(double avgCons, double fullTank, double nowTank, double speed, int weight, int currentWeight = 0, string type = "Грузовой")
           : base(type, avgCons, fullTank, nowTank, speed)
    {
        Weight = weight;
        CurrentWeight = currentWeight;
    }

    public override double Rest(Auto car)  //Переопределяем метод "Остаток хода в км" для грузового авто ("с" или "без" учета груза)
    {

        if (CurrentWeight >= 0 && CurrentWeight <= Weight && NowTank <= FullTank)
        {
            if (NowTank == FullTank)
            {
                double koef = 1 - (CurrentWeight / 200 * 0.04); //вычисляем коэффициент
                return FullTank * 100 / AvgCons * koef;
            }
            else
            {
                double koef = 1 - (CurrentWeight / 200 * 0.04);
                return NowTank * 100 / AvgCons * koef;
            }
        }
        else return 0;
    }
}


public class SportCar : PassCar  //Класс легкового (спортивного) авто (предположим, что это к примеру двухместное купе)
{
    public SportCar(double avgCons, double fullTank, double nowTank, double speed, int passCount = 0, string type = "Спортивный") : base(avgCons, fullTank, nowTank, speed, passCount, type) { }

    public override double Rest(Auto car) //Остаток хода на полном баке в км.
    {
        if (PassCount >= 0 && PassCount <= 1 && NowTank <= FullTank)
        {
            if (NowTank == FullTank)
            {
                switch (PassCount)
                {
                    case 0: return (FullTank * 100 / AvgCons);         //присутствует только водитель
                    case 1: return (FullTank * 100 / AvgCons) * 0.94;  //присутствует водитель и 1 пассажир 
                    default: return 0;
                }
            }
            else
            {
                switch (PassCount)
                {
                    case 0: return NowTank * 100 / AvgCons;         //присутствует только водитель
                    case 1: return NowTank * 100 / AvgCons * 0.94;  //присутствует водитель и 1 пассажир 
                    default: return 0;
                }
            }
        }
        else return 0;
    }
}