using SOLID.ISP.Interfaces;

namespace SOLID.ISP.Models;

public class MultiFunctionPrinter : IMachine
{
    public void Print(Document document)
    {
        //
    }

    public void Fax(Document document)
    {
        //
    }

    public void Scan(Document document)
    {
        //
    }
}