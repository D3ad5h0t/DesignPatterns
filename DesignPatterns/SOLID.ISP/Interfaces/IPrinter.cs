using SOLID.ISP.Models;

namespace SOLID.ISP.Interfaces;

public interface IPrinter
{
    void Print(Document document);
}