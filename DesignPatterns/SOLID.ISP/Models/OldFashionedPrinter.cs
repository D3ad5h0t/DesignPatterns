using SOLID.ISP.Interfaces;

namespace SOLID.ISP.Models;

public class OldFashionedPrinter : IMachine
{
    public void Print(Document document)
    {
        // Ok
    }

    public void Fax(Document document)
    {
        throw new NotImplementedException();
    }

    [Obsolete("Not supported", true)]
    public void Scan(Document document)
    {
        //
    }
}