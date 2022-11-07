using SOLID.ISP.Models;

namespace SOLID.ISP.Interfaces;

public interface IMachine
{
    void Print(Document document);

    void Fax(Document document);

    void Scan(Document document);
}